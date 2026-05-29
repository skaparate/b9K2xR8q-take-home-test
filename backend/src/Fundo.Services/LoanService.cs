using Fundo.Core.Entities;
using Fundo.Core.Interfaces;
using Fundo.Core.Dtos;
using Fundo.Core.Extensions;
using Fundo.Services.Exceptions;

namespace Fundo.Services;

public class LoanService(ILoanRepository loanRepository, IAccountHolderRepository accountHolderRepository)
    : ILoanService
{
    public async Task<IEnumerable<LoanDetailsDto>> GetLoans(int accountHolderId)
    {
        var accountHolder = await FindAccountHolder(accountHolderId);

        if (accountHolder == null)
        {
            throw new AccountHolderNotFoundException(accountHolderId);
        }

        return await loanRepository.GetLoans(accountHolder);
    }

    public async Task<IEnumerable<LoanDetailsDto>> GetLoans()
    {
        return await loanRepository.GetLoans();
    }

    public async Task<LoanDetailsDto?> GetLoanDetails(int accountHolderId, int loanId)
    {
        return await loanRepository.GetLoanDetails(accountHolderId, loanId);
    }

    public async Task<LoanDetailsDto?> GetLoanDetails(int loanId)
    {
        return await loanRepository.GetLoanDetails(loanId);
    }

    public async Task<LoanDetailsDto> CreateLoan(CreateLoanDto createLoanDto)
    {
        var accountHolder = await FindAccountHolder(createLoanDto.AccountHolderId);
        var loan = new Loan
        {
            AccountHolder = accountHolder,
            AmountPaid = (decimal)0.0,
            AmountRequested = createLoanDto.AmountRequested,
            Status = LoanStatus.Active
        };

        return await loanRepository.CreateLoan(loan);
    }

    public async Task<LoanDetailsDto> PayLoan(int accountHolderId, int loanId, decimal amount)
    {
        var loan = await FindLoan(accountHolderId, loanId);
        return await PayLoan(loan, amount);
    }

    public async Task<LoanDetailsDto> PayLoan(int loanId, decimal amount)
    {
        var loan = await FindLoan(loanId);
        return await PayLoan(loan, amount);
    }

    private async Task<LoanDetailsDto> PayLoan(Loan loan, decimal amount)
    {
        var tmp = loan.AmountPaid + amount;

        if (tmp > loan.AmountRequested)
        {
            throw new ArgumentException(
                $"The amount to add exceeds the amount requested (paid={amount}, requested={loan.AmountRequested}, paid={loan.AmountPaid})");
        }

        loan.AmountPaid = tmp;
        await loanRepository.UpdateLoan(loan);

        return await Task.FromResult(loan.ToDto());
    }

    private async Task<AccountHolder> FindAccountHolder(int accountHolderId)
    {
        var accountHolder = await accountHolderRepository.GetAccountHolderById(accountHolderId);
        return accountHolder ?? throw new AccountHolderNotFoundException(accountHolderId);
    }

    private async Task<Loan> FindLoan(int accountHolderId, int loanId)
    {
        var loan = await loanRepository.GetLoan(accountHolderId, loanId);
        return loan ?? throw new LoanNotFoundException(loanId);
    }

    private async Task<Loan> FindLoan(int loanId)
    {
        var loan = await loanRepository.GetLoan(loanId);
        return loan ?? throw new LoanNotFoundException(loanId);
    }
}
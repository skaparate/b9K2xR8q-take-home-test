using Fundo.Core.Dtos;
using Fundo.Core.Entities;
using Fundo.Core.Extensions;
using Fundo.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Repositories;

public class LoanRepository(DatabaseContext databaseContext) : ILoanRepository
{
    public async Task<Loan?> GetLoan(int accountHolderId, int loanId)
    {
        return await databaseContext.Loans.Where(l => l.AccountHolder.Id == accountHolderId && l.Id == loanId)
            .SingleOrDefaultAsync();
    }

    public async Task<Loan?> GetLoan(int loanId)
    {
        return await databaseContext.Loans.Where(l => l.Id == loanId)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<LoanDetailsDto>> GetLoans()
    {
        return await databaseContext.Loans
            .Select(l => new LoanDetailsDto(l.Id, l.AmountRequested, l.AmountPaid, l.Status.ToString())).ToListAsync();
    }

    public async Task<IEnumerable<LoanDetailsDto>> GetLoans(AccountHolder accountHolder)
    {
        return await databaseContext.Loans.Where(l => l.AccountHolder == accountHolder)
            .Select(l => l.ToDto())
            .ToListAsync();
    }

    public async Task<LoanDetailsDto> CreateLoan(Loan loan)
    {
        await databaseContext.Loans.AddAsync(loan);
        await databaseContext.SaveChangesAsync();

        return await Task.FromResult(loan.ToDto());
    }

    public async Task<LoanDetailsDto?> GetLoanDetails(int accountHolderId, int loanId)
    {
        return await databaseContext.Loans.Where(i => i.AccountHolder.Id == accountHolderId && i.Id == loanId)
            .Select(i => i.ToDto())
            .FirstOrDefaultAsync();
    }

    public async Task<LoanDetailsDto?> GetLoanDetails(int loanId)
    {
        var loan = await databaseContext.Loans.FindAsync(loanId);
        return loan?.ToDto();
    }

    public async Task<LoanDetailsDto> UpdateLoan(Loan loan)
    {
        databaseContext.Loans.Update(loan);
        await databaseContext.SaveChangesAsync();
        return await Task.FromResult(loan.ToDto());
    }
}
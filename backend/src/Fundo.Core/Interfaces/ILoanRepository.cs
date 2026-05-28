using Fundo.Core.Dtos;
using Fundo.Core.Entities;

namespace Fundo.Core.Interfaces;

public interface ILoanRepository
{
    Task<Loan?> GetLoan(int accountHolderId, int loanId);
    
    Task<Loan?> GetLoan(int loanId);

    Task<IEnumerable<LoanDetailsDto>> GetLoans();

    Task<IEnumerable<LoanDetailsDto>> GetLoans(AccountHolder accountHolder);

    Task<LoanDetailsDto> CreateLoan(Loan loan);

    Task<LoanDetailsDto?> GetLoanDetails(int accountHolderId, int loanId);

    Task<LoanDetailsDto?> GetLoanDetails(int loanId);

    Task<LoanDetailsDto> UpdateLoan(Loan loan);
}
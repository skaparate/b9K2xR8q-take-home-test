using Fundo.Core.Entities;
using Fundo.Core.Dtos;

namespace Fundo.Services;

public interface ILoanService
{
    Task<IEnumerable<LoanDetailsDto>> GetLoans(int accountHolderId);
    Task<IEnumerable<LoanDetailsDto>> GetLoans();
    
    Task<LoanDetailsDto?> GetLoanDetails(int accountHolderId, int loanId);
    Task<LoanDetailsDto?> GetLoanDetails(int loanId);
    
    Task<LoanDetailsDto> CreateLoan(CreateLoanDto createLoanDto);
    
    Task<LoanDetailsDto> PayLoan(int accountHolderId, int loanId, decimal amount);
    Task<LoanDetailsDto> PayLoan(int loanId, decimal amount);
}
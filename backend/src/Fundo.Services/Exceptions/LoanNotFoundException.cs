namespace Fundo.Services.Exceptions;

public class LoanNotFoundException(int loanId) : Exception($"No loan exists with id {loanId}")
{
    
}
namespace Fundo.Services.Exceptions;

public class AccountHolderMismatchException(int accountHolderId, int loanId)
    : Exception($"The accountHolder {accountHolderId} and the loan {loanId} doesn't match.");
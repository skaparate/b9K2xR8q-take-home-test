namespace Fundo.Services.Exceptions;

public class AccountHolderNotFoundException(int accountHolderId)
    : Exception($"Account holder {accountHolderId} not found");
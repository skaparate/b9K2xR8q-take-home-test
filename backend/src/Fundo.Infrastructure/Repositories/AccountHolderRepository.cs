using Fundo.Core.Entities;
using Fundo.Core.Interfaces;

namespace Fundo.Infrastructure.Repositories;

public class AccountHolderRepository(DatabaseContext databaseContext) : IAccountHolderRepository
{
    public Task<IEnumerable<AccountHolder>> GetAccountHolders()
    {
        throw new NotImplementedException();
    }

    public async Task<AccountHolder?> GetAccountHolderById(int accountHolderId)
    {
        return await databaseContext.AccountHolders.FindAsync(accountHolderId);
    }

    public Task<AccountHolder> CreateAccountHolder(AccountHolder accountHolder)
    {
        throw new NotImplementedException();
    }
}
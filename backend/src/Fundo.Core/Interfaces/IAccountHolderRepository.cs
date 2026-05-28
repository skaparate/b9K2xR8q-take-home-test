using Fundo.Core.Entities;

namespace Fundo.Core.Interfaces;

public interface IAccountHolderRepository
{
    public Task<IEnumerable<AccountHolder>> GetAccountHolders();
    
    public Task<AccountHolder?> GetAccountHolderById(int accountHolderId);
    
    public Task<AccountHolder> CreateAccountHolder(AccountHolder accountHolder);
}
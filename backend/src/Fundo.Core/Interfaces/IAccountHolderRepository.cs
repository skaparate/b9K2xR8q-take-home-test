using Fundo.Core.Dtos;
using Fundo.Core.Entities;

namespace Fundo.Core.Interfaces;

public interface IAccountHolderRepository
{
    public Task<IEnumerable<AccountHolder>> GetAccountHolders();

    public Task<AccountHolderDto?> FindByName(string name);

    public Task<IEnumerable<AccountHolderDto>> FindByNameLike(string name);

    public Task<AccountHolder?> GetAccountHolderById(int accountHolderId);

    public Task<AccountHolder> CreateAccountHolder(AccountHolderDto accountHolder);
}
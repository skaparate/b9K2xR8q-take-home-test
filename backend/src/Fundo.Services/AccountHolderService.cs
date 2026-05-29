using Fundo.Core.Dtos;
using Fundo.Core.Interfaces;

namespace Fundo.Services;

public class AccountHolderService(IAccountHolderRepository accountHolderRepository)
{
    public async Task<IEnumerable<AccountHolderDto>> GetAccountHolders(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return await accountHolderRepository.FindByNameLike(name);
        }


        return await accountHolderRepository.GetAccountHolders();
    }
}
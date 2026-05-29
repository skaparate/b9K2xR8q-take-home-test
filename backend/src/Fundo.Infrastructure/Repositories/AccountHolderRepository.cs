using Fundo.Core.Dtos;
using Fundo.Core.Entities;
using Fundo.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Repositories;

public class AccountHolderRepository(DatabaseContext databaseContext) : IAccountHolderRepository
{
    public async Task<IEnumerable<AccountHolder>> GetAccountHolders()
    {
        return await databaseContext.AccountHolders.ToListAsync();
    }

    public async Task<AccountHolderDto?> FindByName(string name)
    {
        var accountHolder = await databaseContext.AccountHolders.Where(ah => ah.Name == name)
            .Select(entity => new AccountHolderDto
            {
                Id = entity.Id,
                Name = entity.Name
            })
            .SingleOrDefaultAsync();
        return accountHolder;
    }

    public async Task<IEnumerable<AccountHolderDto>> FindByNameLike(string name)
    {
        var accountHolder = await databaseContext.AccountHolders.Where(ah => ah.Name.Contains(name))
            .Select(entity => new AccountHolderDto
            {
                Id = entity.Id,
                Name = entity.Name
            })
            .ToListAsync();
        return accountHolder;
    }

    public async Task<AccountHolder?> GetAccountHolderById(int accountHolderId)
    {
        return await databaseContext.AccountHolders.FindAsync(accountHolderId);
    }

    public async Task<AccountHolder> CreateAccountHolder(AccountHolderDto accountHolder)
    {
        var accountHolderEntity = new AccountHolder
        {
            Name = accountHolder.Name
        };

        databaseContext.AccountHolders.Add(accountHolderEntity);
        await databaseContext.SaveChangesAsync();

        return accountHolderEntity;
    }
}
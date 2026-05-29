using System.Collections.Generic;
using System.Threading.Tasks;
using Fundo.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fundo.Applications.WebApi.Controllers;

[Microsoft.AspNetCore.Components.Route("/account-holders")]
public class AccountHolderController
{
   [HttpGet]
   public Task<IEnumerable<AccountHolderDto>> GetAccountHolders()
   {
      
   }
}
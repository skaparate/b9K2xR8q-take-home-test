using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundo.Core.Dtos;
using Fundo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fundo.Applications.WebApi.Controllers;

[Route("/account-holders")]
public class AccountHolderController(AccountHolderService accountHolderService, ILogger<AccountHolderController> logger)
    : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountHolderDto>>> GetAccountHolders([FromQuery] string name)
    {
        try
        {
            logger.LogInformation($"Getting account holders list; filter by name = {name}");
            var accountHolders = await accountHolderService.GetAccountHolders(name);
            logger.LogInformation($"Account holders list retrieved; size: {accountHolders.Count()}");
            return Ok(accountHolders);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting account holders list");
            throw;
        }
    }
}
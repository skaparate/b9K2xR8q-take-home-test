using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fundo.Core.Dtos;
using Fundo.Services;
using Microsoft.Extensions.Logging;

namespace Fundo.Applications.WebApi.Controllers
{
    [Route("/loans")]
    public class LoanManagementController(ILoanService loanService, ILogger<LoanManagementController> logger)
        : Controller
    {
        public record PayLoanRequest(decimal Amount);

        [HttpPost]
        public async Task<ActionResult<LoanDetailsDto>> CreateLoan([FromBody] CreateLoanDto createLoanDto)
        {
            logger.LogInformation(
                $"Creating a new loan {createLoanDto}");

            var loan = await loanService.CreateLoan(createLoanDto);
            logger.LogInformation("Loan successfully created");

            return Ok(loan);
        }

        [HttpGet("{loanId:int}")]
        public async Task<ActionResult<LoanDetailsDto>> Get(int loanId)
        {
            logger.LogInformation($"Getting loan {loanId}");
            var loan = await loanService.GetLoanDetails(loanId);

            if (loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        [HttpPost("{loanId:int}/payment")]
        public async Task<ActionResult<LoanDetailsDto>> PayLoan([FromRoute] int loanId,
            [FromBody] PayLoanRequest payLoanRequest)
        {
            logger.LogInformation($"Paying loan {loanId} for amount {payLoanRequest.Amount}");
            var loan = await loanService.PayLoan(loanId, payLoanRequest.Amount);

            return Ok(loan);
        }

        [HttpGet]
        public Task<ActionResult> GetAllLoans()
        {
            logger.LogInformation("Retrieving every existing loan");
            // This is not the best way to retrieve them, unless there are just a few (<1000) and we know the loan
            // quantity will not increase (though that's unrealistic). Better implement pagination and/or return
            // only what's needed (projecting the entities). For brevity and per requirement, I'm adding it.
            throw new NotImplementedException();
        }
    }
}
using Fundo.Core.Entities;
using Fundo.Core.Dtos;

namespace Fundo.Core.Extensions;

public static class LoanMappingExtensions
{
    public static LoanDetailsDto ToDto(this Loan loan)
    {
        return new LoanDetailsDto(loan.Id, loan.AmountRequested, loan.AmountPaid, loan.Status.ToString().ToLower());
    }
}
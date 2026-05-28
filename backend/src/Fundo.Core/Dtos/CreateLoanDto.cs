namespace Fundo.Core.Dtos;

public class CreateLoanDto
{
    public decimal AmountRequested { get; set; }
    public int AccountHolderId { get; set; }

    public override string ToString()
    {
        return $"CreateLoanDto[AccountHolderId: {AccountHolderId}, AmountRequested: {AmountRequested}]";
    }
}
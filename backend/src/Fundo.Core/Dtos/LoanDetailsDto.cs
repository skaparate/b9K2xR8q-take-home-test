namespace Fundo.Core.Dtos;

public class LoanDetailsDto(int id, decimal amountRequested, decimal amountPaid, string status)
{
    public int Id { get; } = id;

    public decimal AmountRequested { get; } = amountRequested;

    public decimal AmountPaid { get; } = amountPaid;

    public decimal Balance { get; } = amountRequested - amountPaid;

    public string Status { get; } = status;
}
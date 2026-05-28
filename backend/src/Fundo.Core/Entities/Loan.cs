using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundo.Core.Entities;

public class Loan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public decimal AmountRequested { get; set; }
    
    public decimal AmountPaid { get; set; }
    
    public LoanStatus Status { get; set; }
    
    [Required]
    [ForeignKey("AccountHolderId")]
    public AccountHolder AccountHolder { get; set; } = null!;
}
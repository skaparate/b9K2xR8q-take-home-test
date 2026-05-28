using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundo.Core.Entities;

public class AccountHolder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public String Name { get; set; } = null!;
    
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
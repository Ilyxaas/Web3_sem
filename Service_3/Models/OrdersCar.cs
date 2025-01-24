using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service_1.Models;

[Table("Order", Schema = "public")]
public class OrdersCar
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }
    
    [Required]
    public uint CarType { get; set; }
    
    [Required]
    public uint Count { get; set; }

}
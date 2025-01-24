using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service_1.Models
{
   [Table("OutBox", Schema = "public")]
   public class OutBox
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public uint Id { get; set; }
   
      [Required]
      public string Transaction { get; set; }
   
      [Required]
      public TransactionStatus Status { get; set; }
   }
}
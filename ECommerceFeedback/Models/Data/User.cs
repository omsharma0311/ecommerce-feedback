using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceFeedback.Models.Data
{
    [Table("User", Schema = "dbo")]
    public class User
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
      
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string? Name { get; set; }
       
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string? Email { get; set; }
       
        [Required]
        [Column(TypeName = "varchar(400)")]
        public string? ShippingAddress { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(400)")]
        public string? BillingAddress { get; set; }

        [Required]
        [Column(TypeName = "varchar(400)")]
        public string? PaymentDetails { get; set; }
       
        public ICollection<UserCart>? UserCart { get; set; }
    }
}

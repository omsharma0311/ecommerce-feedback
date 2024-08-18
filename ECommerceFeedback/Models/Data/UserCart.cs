using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFeedback.Models.Data
{
    [Table("UserCart", Schema = "dbo")]
    public class UserCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserCartId { get; set; }
    
        public long UserId { get; set; }
       
        public User? User { get; set; }

        public bool ProductPurchased { get; set; }

        public string? OrderId { get; set; }

        public string? OrderStaus { get; set; }
   
        [Column(TypeName = "datetime2(0)")]
        public DateTime AuditDate { get; set; }
       
        [Column(TypeName = "datetime2(0)")]
        public DateTime? PurchasedDate { get; set; }

       
        public ICollection<ShoppingCartProducts>? ShoppingCartProducts { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFeedback.Models.Data
{
    [Table("ShoppingCart", Schema = "dbo")]
    public class ShoppingCartProducts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CartId { get; set; }
   
        public int? Quantity { get; set; }
      
        [ForeignKey("Products")]
        public long ProductId { get; set; }
       
        public Product? Products { get; set; }

     
        [ForeignKey("UserCart")]
        public long UserCartId { get; set; }
        
        public UserCart? UserCart { get; set; }
    }
}
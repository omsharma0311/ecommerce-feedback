using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceFeedback.Models.Data
{
    [Table("Products", Schema = "dbo")]
    public class Product
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string? Name { get; set; }
       
        [Column(TypeName = "varchar(100)")]
        public string? Description { get; set; }
      
        [Required]
        public long Price { get; set; }
       
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string? Category { get; set; }
       
        public bool ActiveIndicator { get; set; }
      
        [Column(TypeName = "datetime2(0)")]
        public DateTime AuditDate { get; set; }
      
        [Column(TypeName = "datetime2(0)")]
        public DateTime ExpiryDate { get; set; }
      
        public ICollection<ShoppingCartProducts>? ShoppingCartProducts { get; set; }
    }
}


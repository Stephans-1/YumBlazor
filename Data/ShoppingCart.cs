using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Data
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser user { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Golf.Product.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(18)]
        [Required]
        public string Sku { get; set; }

        public string Description { get; set; }

        public virtual Family Family { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

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

        public Gender Gender { get; set; }

        public Hand Hand { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Golf.Product.Model
{
    public class Category
    {
        [Key]
        public short CategoryId { get; set; }

        [StringLength(150)]
        [Required]
        public string Description { get; set; }

        public ICollection<Family> Families { get; set; }
    }

    public class Family
    {
        [Key]
        public int FamilyId { get; set; }

        [StringLength(150)]
        [Required]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }

        public virtual Category Category { get; set; }
    }

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

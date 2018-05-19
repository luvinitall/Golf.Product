using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Golf.Product.Model
{
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
}
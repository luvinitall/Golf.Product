using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Golf.Product.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        //TODO: Add test to validate the regex
        [Required]
        [StringLength(18)]
        [Index("uidx_Product_Sku",IsUnique = true)]
        [RegularExpression("^([a-zA-Z0-9]+)$")]
        public string Sku { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }

        public int FamilyId { get; set; }
        public virtual Family Family { get; set; }

        public Gender Gender { get; set; }
  

        public Hand Hand { get; set; }
    }
}
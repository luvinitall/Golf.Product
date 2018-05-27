using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Golf.Product.Model
{
    public class Catalog
    {
        [Key]
        public short CatalogId { get; set; }

        [Required]
        [StringLength(250)]
        [Index("uidx_Catalog_Description",IsUnique = true)]
        public string Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; }


    }



}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Golf.Product.Model
{
    public class Category
    {
        [Key]
        public short CategoryId { get; set; }

        [Required]
        [StringLength(250)]
        [Index("uidx_Category_Description",IsUnique = true)]
        public string Description { get; set; }

        //public virtual ICollection<Asset> Assets { get; set; }

        public virtual ICollection<Family> Families { get; set; } = new List<Family>();

        public virtual ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>();
    }
}

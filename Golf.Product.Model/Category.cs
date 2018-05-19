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
}

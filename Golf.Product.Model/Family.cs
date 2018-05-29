using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml;

namespace Golf.Product.Model
{
    public class Family:IValidatableObject
    {
        [Key]
        public int FamilyId { get; set; }

        [Required]
        [StringLength(250)]
        [Index("uidx_Family_Description", IsUnique = true)]
        public string Description { get; set; }

        public short CategoryId { get; set; }

        public virtual Category Category { get; set; }
        
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        
        //public virtual ICollection<CustomOption> CustomOptions { get; set; }

        //Outside of a demo, I might just trim the values for the user, but I wanted to try out a 'complex' validation rule
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Description.Trim() != Description)
                return new List<ValidationResult>()
                {
                    new ValidationResult("Family names may not start or end with whitespace.",new []{"Description"})
                };

            return Enumerable.Empty<ValidationResult>();
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Golf.Product.Model
{

    public  class CustomOptionType
    {
        [Key] public short CustomOptionTypeId { get; set; }

        [Required]
        [StringLength(250)]
        [Index("uidx_CustomOptionType_Description", IsUnique = true)]
        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public virtual ICollection<CustomOption> CustomOptions { get; set; }
    }

    //public class ValueCustomOptionType : CustomOptionType
    //{
    //    [Required] public int MaxLength { get; set; }
    //    [StringLength(2000)] public string RegExValidationRule { get; set; }

    //}
}

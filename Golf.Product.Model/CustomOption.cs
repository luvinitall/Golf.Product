using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Golf.Product.Model;

namespace Golf.Product.Model
{

    public class CustomOption
    {
        [Key]
        public int CustomOptionId { get; set; }


        public virtual CustomOptionType CustomOptionType { get; set; }

        //TODO: Test the unique index on two columns
        [Index("uidx_CustomOption_CustomOptionType_SapReference")]
        public short CustomOptionTypeId { get; set; }

        [Required]
        [StringLength(18)]
        [Index("uidx_CustomOption_CustomOptionType_SapReference")]
        [Index("uidx_CustomOption_CustomOptionType_SapReference", 2, IsUnique = true)]
        public string SapReference { get; set; }


        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        //public virtual ComponentCustomOptionBase ClubComponent { get; set; }
        public virtual ICollection<Family> Families { get; set; }

        //public virtual ShaftComponentCustomOption ShaftComponentCustomOption { get; set; }

    }

    public abstract class ComponentCustomOptionBase : CustomOption
    {
        
        public bool IsConstrained { get; set; }
  

        public virtual ICollection<Asset> Assets { get; set; }
    }


    public class GripComponentCustomOption : ComponentCustomOptionBase
    {
        
        public string GripMaterialNumber => SapReference;
    }
        

    public class ShaftComponentCustomOption : ComponentCustomOptionBase
    {
        [Required]
        [StringLength(250)]
        public string ShaftModelCode => SapReference;
 
        //public ICollection<CustomOption> Flexes { get; set; }
    }
}
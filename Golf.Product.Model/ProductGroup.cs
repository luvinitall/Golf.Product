using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Golf.Product.Model
{
    //This is an open type to allow extended properties based on the product (For example like Custom, Right Handed, Gender, etc...)
    public class ProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public int SortOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        //public IDictionary<string, object> Properties;
    }

    //public class DynamicPropertyHelper<T>
    //{
    //    [Key]
    //    [Column(Order = 1)]
    //    public string Key { get; set; }
    //    public string SerializedValue { get; set; }
        
    //    public object Value
    //    {
    //        get { return JsonConvert.DeserializeObject(SerializedValue); }
    //        set { SerializedValue = JsonConvert.SerializeObject(value); }
    //    }

    //    [Key]
    //    [Column(Order = 2)]
    //    public virtual T Parent { get; set; }
    //}
}
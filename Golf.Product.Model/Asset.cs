using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Golf.Product.Model
{
    public enum AssetType:byte
    {
        Marketing= 0, Reporting = 1, InternalUse=2
    }; 
       
    //An asset represents an image which is really a collection of images all representing the same thing, but in different sizes and/or resolutions
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }
        public AssetType AssetType { get; set; }
        public string Description { get; set; }

        public ICollection<ImageUrl> ImageUrls { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<ComponentCustomOptionBase> ClubComponents { get; set; }
    }



    public struct Resolution
    {
        [Range(0, Int32.MaxValue)]
        public int HeightInPixels { get; set; }
        [Range(0, Int32.MaxValue)]
        public int WidthInPixels { get; set; }
    }
    //This will be on OpenType class to allow for various properties to be persisted that might be used by the client to determine the best image to select
    public class ImageUrl
    {
        [Key]
        public int ImageUrlId { get; set; }

        [Index("idx_ImageUrl_AssetId", IsUnique=false)]
        public int AssetId { get; set; }
        public virtual Asset Asset { get; set; }

        [Required]
        [StringLength(850)]
        [Index("uidx_ImageUrl_Url",IsUnique=true)]
        [Url] //Pretty Cool, easy Url validation!!  Could also use Regex.
        public string Url { get; set; }


        public int WidthInPixels { get; set; }
        public int HeightInPixels { get; set; }

        // public IDictionary<string, string> Properties { get; set; }
    }
}


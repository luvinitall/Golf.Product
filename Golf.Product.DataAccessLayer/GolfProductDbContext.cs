using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Golf.Product.Model;

namespace Golf.Product.DataAccessLayer
{
    public class GolfProductDbContext:DbContext
    {
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Model.Product> Products { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        
        public virtual DbSet<CustomOption> CustomOptions { get; set; }
        public virtual DbSet<CustomOptionType> CustomOptionTypes { get; set; }

        public virtual DbSet<ImageUrl> ImageUrls { get; set; }

        public virtual DbSet<ProductGroup> ProductGroups { get; set; }


        public GolfProductDbContext()
        {
            Database.SetInitializer(new GolfProductDbInitializer());

            // disable lazy loading
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Asset>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Assets)
                .Map(m => m.ToTable("Category_Asset").MapLeftKey("CategoryId").MapRightKey("AssetId"));

            modelBuilder.Entity<Asset>()
                .HasMany(e => e.ClubComponents)
                .WithMany(e => e.Assets)
                .Map(m => m.ToTable("ClubComponent_Asset").MapLeftKey("CustomOptionId").MapRightKey("AssetId"));

            

            modelBuilder.Entity<Catalog>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Catalogs)
                .Map(m => m.ToTable("Catalog_Category").MapLeftKey("CatalogId").MapRightKey("CategoryId"));


            modelBuilder.Entity<Category>()
                .HasMany(e => e.Families)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            


            modelBuilder.Entity<CustomOption>()
                .HasMany(e => e.Families)
                .WithMany(e => e.CustomOptions)
                .Map(m => m.ToTable("Family_CustomOption").MapLeftKey("FamilyId").MapRightKey("CustomOptionId"));


            modelBuilder.Entity<ComponentCustomOptionBase>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("ClubComponent");
                });

            modelBuilder.Entity<GripComponentCustomOption>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("ClubComponent");
                });

            modelBuilder.Entity<ShaftComponentCustomOption>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("ClubComponent");
                });

           


            //modelBuilder.Entity<CustomOption>()
            //    .HasOptional(x => x.ShaftComponentCustomOption)
            //    .WithMany(x => x.Flexes)
            //    .HasForeignKey(x=>x.CustomOptionId).WillCascadeOnDelete(true);
            //    //.Map(m => m.ToTable("ClubComponent_ChildOption"));






            modelBuilder.Entity<ComponentCustomOptionBase>()
                .Property(e => e.SapReference)
                .IsUnicode(false);

            modelBuilder.Entity<CustomOptionType>()
                .HasMany(e => e.CustomOptions)
                .WithRequired(e => e.CustomOptionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Family>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Family)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model.Product>()
                .Property(e => e.Sku)
                .IsUnicode(false);

            modelBuilder.Entity<ProductGroup>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model.Product>().HasRequired(p => p.Family);
        }
    }
}
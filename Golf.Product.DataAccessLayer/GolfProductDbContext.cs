using System.Data.Entity;
using Golf.Product.Model;

namespace Golf.Product.DataAccessLayer
{
    public class GolfProductDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Model.Product> Products { get; set; }


        public GolfProductDbContext()
        {
            Database.SetInitializer(new GolfProductDbInitializer());
            // disable lazy loading
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasMany(p => p.Families)
                .WithRequired(r => r.Category).WillCascadeOnDelete(true);

            modelBuilder.Entity<Family>().HasMany(p => p.Products)
                .WithRequired(r => r.Family).WillCascadeOnDelete(true);

            modelBuilder.Entity<Model.Product>().HasRequired(p => p.Family);
        }
    }
}
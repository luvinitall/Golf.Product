using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Golf.Product.Model;

namespace Golf.Product.DataAccessLayer
{
    public class GolfProductDbInitializer: DropCreateDatabaseAlways<GolfProductDbContext>
    {
        protected override void Seed(GolfProductDbContext context)
        {

            var usCatalog = new Catalog(){CatalogId = 1, Description = "US Catalog"};
            var japanCatalog = new Catalog() { CatalogId = 2, Description = "JP Catalog" };

            var categoryWoods = new Category()
            {
               
                Description = "Woods", Catalogs = new List<Catalog>() { usCatalog, japanCatalog}
            };

            var categoryIrons = new Category()
            {
                Description = "Irons", Catalogs = new List<Catalog>() { usCatalog}
            };

            var emptyCategory = new Category()
            {
                Description = "Empty"
            };



            var familyEpicDriver = new Family()
            {
                Description = "Epic Drivers",
                Category = categoryWoods
            };

            var familyRogueDriver = new Family()
            {
                Description = "Rogue Drivers",
                Category = categoryWoods
            };

            var familyEpicIron = new Family()
            {
                Description = "Epic Irons",
                Category = categoryIrons
            };

            var familyRogueIron = new Family()
            {
                Description = "Rogue Irons",
                Category = categoryIrons
            };
            
            var familyIronNoProducts = new Family()
            {
                Description = "Empty Family",
                Category = categoryIrons
            };

            var rogueIronRHMensProductGroup = new ProductGroup(){Description = "Rogue Irons RH Mens"};
            var rogueIronLHMensProductGroup = new ProductGroup() { Description = "Rogue Irons LH Mens" };

            var rogueIronRHWomensProductGroup = new ProductGroup() { Description = "Rogue Irons RH Womens" };
            var rogueIronLHWomensProductGroup = new ProductGroup() { Description = "Rogue Irons LH Womens" };

            familyRogueIron.Products = new List<Model.Product>();
            var productRogueIron3 =
                new Model.Product() {Description = "Rogue 3 Iron", Family = familyRogueIron, Sku = "Rogue3IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIron4 =
                new Model.Product() { Description = "Rogue 4 Iron", Family = familyRogueIron, Sku = "Rogue4IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIron5 =
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIron6 =
                new Model.Product() { Description = "Rogue 6 Iron", Family = familyRogueIron, Sku = "Rogue6IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIron7 =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIron8 =
                new Model.Product() { Description = "Rogue 8 Iron", Family = familyRogueIron, Sku = "Rogue8IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIron9 =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIronSet1 =
                new Model.Product() { Description = "Rogue 3-P Iron", Family = familyRogueIron, Sku = "Rogue3PIronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            var productRogueIronSet2 =
                new Model.Product() { Description = "Rogue 5-P Iron", Family = familyRogueIron, Sku = "Rogue5PIronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };

            familyRogueIron.Products.Add(productRogueIron3);
            familyRogueIron.Products.Add(productRogueIron4);
            familyRogueIron.Products.Add(productRogueIron5);
            familyRogueIron.Products.Add(productRogueIron6);
            familyRogueIron.Products.Add(productRogueIron7);
            familyRogueIron.Products.Add(productRogueIron8);
            familyRogueIron.Products.Add(productRogueIron9);
            familyRogueIron.Products.Add(productRogueIronSet1);
            familyRogueIron.Products.Add(productRogueIronSet2);
     

            var productRogueIron5F =
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5IronWR", Gender = Gender.Female, Hand = Hand.Right, ProductGroup = rogueIronRHWomensProductGroup };
            var productRogueIron6F =
                new Model.Product() { Description = "Rogue 6 Iron", Family = familyRogueIron, Sku = "Rogue6IronWR", Gender = Gender.Female, Hand = Hand.Right, ProductGroup = rogueIronRHWomensProductGroup };
            var productRogueIron7F =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7IronWR", Gender = Gender.Female, Hand = Hand.Right, ProductGroup = rogueIronRHWomensProductGroup };
            var productRogueIron8F =
                new Model.Product() { Description = "Rogue 8 Iron", Family = familyRogueIron, Sku = "Rogue8IronWR", Gender = Gender.Female, Hand = Hand.Right, ProductGroup = rogueIronRHWomensProductGroup };
            var productRogueIron9F =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9IronWR", Gender = Gender.Female, Hand = Hand.Right, ProductGroup = rogueIronRHWomensProductGroup };
            
            var productRogueIronSetF =
                new Model.Product() { Description = "Rogue 5-P Iron", Family = familyRogueIron, Sku = "Rogue5PIronWR", Gender = Gender.Female, Hand = Hand.Right, ProductGroup = rogueIronRHWomensProductGroup };


            familyRogueIron.Products.Add(productRogueIron5F);
            familyRogueIron.Products.Add(productRogueIron6F);
            familyRogueIron.Products.Add(productRogueIron7F);
            familyRogueIron.Products.Add(productRogueIron8F);
            familyRogueIron.Products.Add(productRogueIron9F);
            familyRogueIron.Products.Add(productRogueIronSetF);
   



            var productRogueIron5LH =
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5IronLM", Gender = Gender.Male, Hand = Hand.Left, ProductGroup = rogueIronLHMensProductGroup };
            var productRogueIron6LH =
                new Model.Product() { Description = "Rogue 6 Iron", Family = familyRogueIron, Sku = "Rogue6IronLM", Gender = Gender.Male, Hand = Hand.Left, ProductGroup = rogueIronLHMensProductGroup };
            var productRogueIron7LH =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7IronLM", Gender = Gender.Male, Hand = Hand.Left, ProductGroup = rogueIronLHMensProductGroup };
            var productRogueIron8LH =
                new Model.Product() { Description = "Rogue 8 Iron", Family = familyRogueIron, Sku = "Rogue8IronLM", Gender = Gender.Male, Hand = Hand.Left, ProductGroup = rogueIronLHMensProductGroup };
            var productRogueIron9LH =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9IronLM", Gender = Gender.Male, Hand = Hand.Left, ProductGroup = rogueIronLHMensProductGroup };
            
            var productRogueIronSetLH =
                new Model.Product() { Description = "Rogue 5-P Iron", Family = familyRogueIron, Sku = "Rogue5PIronLM", Gender = Gender.Male, Hand = Hand.Left, ProductGroup = rogueIronLHMensProductGroup };


            familyRogueIron.Products.Add(productRogueIron5LH);
            familyRogueIron.Products.Add(productRogueIron6LH);
            familyRogueIron.Products.Add(productRogueIron7LH);
            familyRogueIron.Products.Add(productRogueIron8LH);
            familyRogueIron.Products.Add(productRogueIron9LH);
            familyRogueIron.Products.Add(productRogueIronSetLH);

            var productRogueIron5LHF =
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5IronLW", Gender = Gender.Female, Hand = Hand.Left, ProductGroup = rogueIronLHWomensProductGroup };
            
            var productRogueIron7LHF =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7IronLW", Gender = Gender.Female, Hand = Hand.Left, ProductGroup = rogueIronLHWomensProductGroup };
            
            var productRogueIron9LHF =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9IronLW", Gender = Gender.Female, Hand = Hand.Left, ProductGroup = rogueIronLHWomensProductGroup };

            familyRogueIron.Products.Add(productRogueIron5LHF);
            familyRogueIron.Products.Add(productRogueIron7LHF);
            familyRogueIron.Products.Add(productRogueIron9LHF);
            

            categoryIrons.Families = new List<Family>(){familyRogueIron, familyEpicIron ,familyIronNoProducts};
            categoryWoods.Families = new List<Family>() { familyEpicDriver, familyRogueDriver };

            context.Catalogs.Add(usCatalog);
            context.Catalogs.Add(japanCatalog);

            context.Categories.Add(categoryWoods);
            context.Categories.Add(categoryIrons);
            context.Categories.Add(emptyCategory);

            context.Families.Add(familyEpicDriver);
            context.Families.Add(familyRogueDriver);
            context.Families.Add(familyEpicIron);
            context.Families.Add(familyRogueIron);
            context.Families.Add(familyIronNoProducts);

            context.Products.Add(productRogueIron3);
            context.Products.Add(productRogueIron4);
            context.Products.Add(productRogueIron5);
            context.Products.Add(productRogueIron6);
            context.Products.Add(productRogueIron7);
            context.Products.Add(productRogueIron8);
            context.Products.Add(productRogueIron9);
            context.Products.Add(productRogueIronSet1);
            context.Products.Add(productRogueIronSet2);

            context.Products.Add(productRogueIron5F);
            context.Products.Add(productRogueIron6F);
            context.Products.Add(productRogueIron7F);
            context.Products.Add(productRogueIron8F);
            context.Products.Add(productRogueIron9F);
            context.Products.Add(productRogueIronSetF);

            context.Products.Add(productRogueIron5LH);
            context.Products.Add(productRogueIron6LH);
            context.Products.Add(productRogueIron7LH);
            context.Products.Add(productRogueIron8LH);
            context.Products.Add(productRogueIron9LH);
            context.Products.Add(productRogueIronSetLH);

            context.Products.Add(productRogueIron5LHF);
            context.Products.Add(productRogueIron7LHF);
            context.Products.Add(productRogueIron9LHF);


            base.Seed(context);
        }
    }
}

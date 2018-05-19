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
            short categoryId = 0;
            int familyId = 0;
            int productId = 0;

            var categoryWoods = new Category()
            {
                CategoryId = categoryId++,
                Description = "Woods"
            };

            var categoryIrons = new Category()
            {
                CategoryId = categoryId++,
                Description = "Irons"
            };

            var emptyCategory = new Category()
            {
                CategoryId = categoryId++,
                Description = "Empty"
            };



            var familyEpicDriver = new Family()
            {
                FamilyId = familyId++,
                Description = "Epic Drivers",
                Category = categoryWoods
            };

            var familyRogueDriver = new Family()
            {
                FamilyId = familyId++,
                Description = "Rogue Drivers",
                Category = categoryWoods
            };

            var familyEpicIron = new Family()
            {
                FamilyId = familyId++,
                Description = "Epic Irons",
                Category = categoryIrons
            };

            var familyRogueIron = new Family()
            {
                FamilyId = familyId++,
                Description = "Rogue Irons",
                Category = categoryIrons
            };
            familyRogueIron.Products = new List<Model.Product>();
            var productRogueIron3 =
                new Model.Product() {Description = "Rogue 3 Iron", Family = familyRogueIron, Sku = "Rogue3Iron", Gender = Gender.Male, Hand = Hand.Right};
            var productRogueIron4 =
                new Model.Product() { Description = "Rogue 4 Iron", Family = familyRogueIron, Sku = "Rogue4Iron", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron5 =
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5Iron", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron6 =
                new Model.Product() { Description = "Rogue 6 Iron", Family = familyRogueIron, Sku = "Rogue6Iron", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron7 =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7Iron", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron8 =
                new Model.Product() { Description = "Rogue 8 Iron", Family = familyRogueIron, Sku = "Rogue8Iron", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron9 =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9Iron", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIronSet1 =
                new Model.Product() { Description = "Rogue 3-P Iron", Family = familyRogueIron, Sku = "Rogue3PIron", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIronSet2 =
                new Model.Product() { Description = "Rogue 5-P Iron", Family = familyRogueIron, Sku = "Rogue5PIron", Gender = Gender.Male, Hand = Hand.Right };

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
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5Iron", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron6F =
                new Model.Product() { Description = "Rogue 6 Iron", Family = familyRogueIron, Sku = "Rogue6Iron", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron7F =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7Iron", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron8F =
                new Model.Product() { Description = "Rogue 8 Iron", Family = familyRogueIron, Sku = "Rogue8Iron", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron9F =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9Iron", Gender = Gender.Female, Hand = Hand.Right };
            
            var productRogueIronSetF =
                new Model.Product() { Description = "Rogue 5-P Iron", Family = familyRogueIron, Sku = "Rogue5PIron", Gender = Gender.Female, Hand = Hand.Right };


            familyRogueIron.Products.Add(productRogueIron5F);
            familyRogueIron.Products.Add(productRogueIron6F);
            familyRogueIron.Products.Add(productRogueIron7F);
            familyRogueIron.Products.Add(productRogueIron8F);
            familyRogueIron.Products.Add(productRogueIron9F);
            familyRogueIron.Products.Add(productRogueIronSetF);
   



            var productRogueIron5LH =
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5Iron", Gender = Gender.Male, Hand = Hand.Left };
            var productRogueIron6LH =
                new Model.Product() { Description = "Rogue 6 Iron", Family = familyRogueIron, Sku = "Rogue6Iron", Gender = Gender.Male, Hand = Hand.Left };
            var productRogueIron7LH =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7Iron", Gender = Gender.Male, Hand = Hand.Left };
            var productRogueIron8LH =
                new Model.Product() { Description = "Rogue 8 Iron", Family = familyRogueIron, Sku = "Rogue8Iron", Gender = Gender.Male, Hand = Hand.Left };
            var productRogueIron9LH =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9Iron", Gender = Gender.Male, Hand = Hand.Left };
            
            var productRogueIronSetLH =
                new Model.Product() { Description = "Rogue 5-P Iron", Family = familyRogueIron, Sku = "Rogue5PIron", Gender = Gender.Male, Hand = Hand.Left };


            familyRogueIron.Products.Add(productRogueIron5LH);
            familyRogueIron.Products.Add(productRogueIron6LH);
            familyRogueIron.Products.Add(productRogueIron7LH);
            familyRogueIron.Products.Add(productRogueIron8LH);
            familyRogueIron.Products.Add(productRogueIron9LH);
            familyRogueIron.Products.Add(productRogueIronSetLH);

            var productRogueIron5LHF =
                new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5Iron", Gender = Gender.Female, Hand = Hand.Left };
            
            var productRogueIron7LHF =
                new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7Iron", Gender = Gender.Female, Hand = Hand.Left };
            
            var productRogueIron9LHF =
                new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9Iron", Gender = Gender.Female, Hand = Hand.Left };

            familyRogueIron.Products.Add(productRogueIron5LHF);
            familyRogueIron.Products.Add(productRogueIron7LHF);
            familyRogueIron.Products.Add(productRogueIron9LHF);

            categoryIrons.Families = new List<Family>(){familyRogueIron, familyEpicIron};
            categoryWoods.Families = new List<Family>() { familyEpicDriver, familyRogueDriver };

            context.Categories.Add(categoryWoods);
            context.Categories.Add(categoryIrons);
            context.Categories.Add(emptyCategory);

            context.Families.Add(familyEpicDriver);
            context.Families.Add(familyRogueDriver);
            context.Families.Add(familyEpicIron);
            context.Families.Add(familyRogueIron);

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

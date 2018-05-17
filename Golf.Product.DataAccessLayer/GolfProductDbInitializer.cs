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

            categoryIrons.Families = new List<Family>(){familyRogueIron, familyEpicIron};
            categoryWoods.Families = new List<Family>() { familyEpicDriver, familyRogueDriver };

            context.Categories.Add(categoryWoods);
            context.Categories.Add(categoryIrons);

            context.Families.Add(familyEpicDriver);
            context.Families.Add(familyRogueDriver);
            context.Families.Add(familyEpicIron);
            context.Families.Add(familyRogueIron);


            base.Seed(context);
        }
    }
}

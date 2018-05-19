using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Golf.Product.DataAccessLayer;
using Golf.Product.Helpers;
using Golf.Product.Model;

namespace Golf.Product.Controllers
{
    public class CategoriesController : ODataController
    {
        GolfProductDbContext _ctx = new GolfProductDbContext();

        public IHttpActionResult Get()
        {
            return Ok(_ctx.Categories.Include("Families"));

        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            var category = _ctx.Categories.FirstOrDefault(c => c.CategoryId == key);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [ODataRoute("Categories({key})/Description")]
        public IHttpActionResult GetCategoryProperty([FromODataUri] int key)
        {
            var category = _ctx.Categories.FirstOrDefault(c => c.CategoryId == key);

            if (category == null)
                return NotFound();

            var propertyToGet = Url.Request.RequestUri.Segments.Last();

            if (!category.HasProperty(propertyToGet))
                return NotFound();

            var propertyValue = category.GetValue(propertyToGet);

            if (propertyValue == null)
                return StatusCode(System.Net.HttpStatusCode.NoContent);


            return this.CreateOKHttpActionResult(propertyValue);
        }

        [HttpGet]
        [ODataRoute("Categories({key})/Families")]
        public IHttpActionResult GetCategoryCollectionProperty([FromODataUri] int key)
        {
            var propertyToGet = Url.Request.RequestUri.Segments.Last();

            var category = _ctx.Categories.Include(propertyToGet).FirstOrDefault(c => c.CategoryId == key);

            if (category == null)
                return NotFound();


            var propertyValue = category.GetValue(propertyToGet);



            return this.CreateOKHttpActionResult(propertyValue);
        }

        [HttpGet]
        [ODataRoute("Categories({key})/Description/$value")]
        public IHttpActionResult GetCategoryPropertyRawValue([FromODataUri] int key)
        {
            var category = _ctx.Categories.FirstOrDefault(c => c.CategoryId == key);

            if (category == null)
                return NotFound();

            var propertyToGet = Url.Request.RequestUri.Segments[Url.Request.RequestUri.Segments.Length - 2].TrimEnd('/');

            if (!category.HasProperty(propertyToGet))
                return NotFound();

            var propertyValue = category.GetValue(propertyToGet);

            if (propertyValue == null)
                return StatusCode(System.Net.HttpStatusCode.NoContent);


            return this.CreateOKHttpActionResult(propertyValue.ToString());
        }

        public IHttpActionResult Post(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _ctx.Categories.Add(category);
            _ctx.SaveChanges();

            return Created(category);
        }

        public IHttpActionResult Put([FromODataUri] int key, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentCategory = _ctx.Categories.FirstOrDefault(c => c.CategoryId == key);
            if (currentCategory == null)
                return NotFound();

            category.CategoryId = currentCategory.CategoryId;


            _ctx.Entry(currentCategory).CurrentValues.SetValues(category);

            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Patch([FromODataUri] int key, Delta<Category> patch)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var currentCategory = _ctx.Categories.FirstOrDefault(c => c.CategoryId == key);
            if (currentCategory == null)
                return NotFound();

            patch.Patch(currentCategory);
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var currentCategory = _ctx.Categories.Include("Families").FirstOrDefault(c => c.CategoryId == key);
            if (currentCategory == null)
                return NotFound();

            //Can not delete if the category is linked to any families;
            if (currentCategory.Families.Any())
                return Content(HttpStatusCode.Conflict,"This category is being referenced by other families.");

            _ctx.Categories.Remove(currentCategory);
            _ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ODataRoute("Categories({key})/Families/$ref")]
        public IHttpActionResult CreateLinkToFamily([FromODataUri] int key, [FromBody] Uri link)
        {
            var currentCategory = _ctx.Categories.Include("Families").FirstOrDefault(c => c.CategoryId == key);
            if (currentCategory == null)
                return NotFound();

            int keyOfFamilyToAdd = Request.GetKeyValue<int>(link);

            if (currentCategory.Families.Any(i => i.FamilyId == keyOfFamilyToAdd))
                return BadRequest($"The family with id {keyOfFamilyToAdd} is already linked to this category");


            var familyLinkToAdd = _ctx.Families.Include("Category").FirstOrDefault(f => f.FamilyId == keyOfFamilyToAdd);

            if (familyLinkToAdd == null)
                return NotFound();

 currentCategory.Families.Add(familyLinkToAdd);
            familyLinkToAdd.Category = currentCategory;
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [ODataRoute("Categories({key})/Families({relatedKey})/$ref")]
        public IHttpActionResult UpdateLinkToFamily([FromODataUri] int key, [FromODataUri] int relatedKey, [FromBody] Uri link)
        {
            var currentCategory = _ctx.Categories.Include("Families").FirstOrDefault(c => c.CategoryId == key);
            if (currentCategory == null)
                return NotFound();

            var familyToRemove = currentCategory.Families.FirstOrDefault(f => f.FamilyId == relatedKey);

            if (familyToRemove == null)
                return NotFound();

            int keyOfFamilyToAdd = Request.GetKeyValue<int>(link);

            if (currentCategory.Families.Any(i => i.FamilyId == keyOfFamilyToAdd))
                return BadRequest($"The family with id {keyOfFamilyToAdd} is already linked to this category");


            var familyLinkToAdd = _ctx.Families.Include("Category").FirstOrDefault(f => f.FamilyId == keyOfFamilyToAdd);

            if (familyLinkToAdd == null)
                return NotFound();


            currentCategory.Families.Remove(familyToRemove);
            _ctx.Families.Remove(familyToRemove);


            currentCategory.Families.Add(familyLinkToAdd);
            familyLinkToAdd.Category = currentCategory;
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [ODataRoute("Categories({key})/Families({relatedKey})/$ref")]
        public IHttpActionResult DeleteLinkToFamily([FromODataUri] int key, [FromODataUri] int relatedKey)
        {
            var currentCategory = _ctx.Categories.Include("Families").FirstOrDefault(c => c.CategoryId == key);
            if (currentCategory == null)
                return NotFound();

            var familyToRemove = currentCategory.Families.FirstOrDefault(f => f.FamilyId == relatedKey);

            if (familyToRemove == null)
                return NotFound();

            familyToRemove.Category = null;
            currentCategory.Families.Remove(familyToRemove);
            _ctx.Families.Remove(familyToRemove);

            _ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);

        }

        protected override void Dispose(bool disposing)
        {
            _ctx?.Dispose();
            base.Dispose(disposing);
        }
    }
}
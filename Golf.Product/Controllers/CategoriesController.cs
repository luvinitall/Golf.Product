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
            return Ok(_ctx.Categories);

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
        public IHttpActionResult GetPersonCollectionProperty([FromODataUri] int key)
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
        public IHttpActionResult GetPersonPropertyRawValue([FromODataUri] int key)
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
                return StatusCode(HttpStatusCode.Conflict);

            _ctx.Categories.Remove(currentCategory);
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
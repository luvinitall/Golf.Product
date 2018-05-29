using System;
using System.Data.Entity;
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
    public class CatalogsController:ODataController
    {
        private GolfProductDbContext _ctx;

        public CatalogsController(GolfProductDbContext context)
        {
            _ctx = context;
        }

        public CatalogsController()
        {
            _ctx = new GolfProductDbContext();
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public IHttpActionResult Get()
        {
            return Ok(_ctx.Catalogs);

        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            var catalog = _ctx.Catalogs.Where(c => c.CatalogId == key);

            if (!catalog.Any())
                return NotFound();

            return Ok(SingleResult.Create(catalog));
        }

        [HttpGet]
        [ODataRoute("Catalogs({key})/Description")]
        public virtual IHttpActionResult GetCatalogProperty([FromODataUri] int key)
        {
            var catalog = _ctx.Catalogs.FirstOrDefault(c => c.CatalogId == key);

            if (catalog == null)
                return NotFound();

            var propertyToGet = Request.RequestUri.Segments.Last();

            if (!catalog.HasProperty(propertyToGet))
                return NotFound();

            var propertyValue = catalog.GetValue(propertyToGet);

            if (propertyValue == null)
                return StatusCode(System.Net.HttpStatusCode.NoContent);


            return this.CreateOKHttpActionResult(propertyValue);
        }

        [HttpGet]
        [ODataRoute("Catalogs({key})/Categories")]
        [EnableQuery]
        public IHttpActionResult GetCategoryCollectionProperty([FromODataUri] int key)
        {
            var catalog = _ctx.Catalogs.Where(c => c.CatalogId == key);

            if (!catalog.Any())
                return NotFound();


            return Ok(_ctx.Categories.Where(f => f.Catalogs.Any(c=>c.CatalogId == key)));


        }

        [HttpGet]
        [ODataRoute("Catalogs({key})/Description/$value")]
        public IHttpActionResult GetCategoryPropertyRawValue([FromODataUri] int key)
        {
            var catalog = _ctx.Catalogs.FirstOrDefault(c => c.CatalogId == key);

            if (catalog == null)
                return NotFound();

            var propertyToGet = Request.RequestUri.Segments[Request.RequestUri.Segments.Length - 2].TrimEnd('/');

            if (!catalog.HasProperty(propertyToGet))
                return NotFound();

            var propertyValue = catalog.GetValue(propertyToGet);

            if (propertyValue == null)
                return StatusCode(System.Net.HttpStatusCode.NoContent);


            return this.CreateOKHttpActionResult(propertyValue.ToString());
        }

        public IHttpActionResult Post(Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _ctx.Catalogs.Add(catalog);
            _ctx.SaveChanges();

            return Created(catalog);
        }

        public IHttpActionResult Put([FromODataUri] int key, Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentCatalog = _ctx.Catalogs.FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            catalog.CatalogId = currentCatalog.CatalogId;


            _ctx.Entry(currentCatalog).CurrentValues.SetValues(catalog);

            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Patch([FromODataUri] int key, Delta<Catalog> patch)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var currentCatalog = _ctx.Catalogs.FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            patch.Patch(currentCatalog);
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var currentCatalog = _ctx.Catalogs.Include("Categories").FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            //Can not delete if the category is linked to any families;
            if (currentCatalog.Categories.Any())
                return Content(HttpStatusCode.Conflict, "This catalog is being referenced by other categories.");

            _ctx.Catalogs.Remove(currentCatalog);
            _ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ODataRoute("Catalogs({key})/Categories/$ref")]
        public IHttpActionResult CreateLinkToCategory([FromODataUri] int key, [FromBody] Uri link)
        {
            var currentCatalog = _ctx.Catalogs.Include("Categories").FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            int keyOfCategoryToAdd = Request.GetKeyValue<int>(link);

            if (currentCatalog.Categories.Any(i => i.CategoryId == keyOfCategoryToAdd))
                return BadRequest($"The category with id {keyOfCategoryToAdd} is already linked to this catelog");


            var categoryLinkToAdd = _ctx.Categories.Include("Catalog").FirstOrDefault(f => f.CategoryId == keyOfCategoryToAdd);

            if (categoryLinkToAdd == null)
                return NotFound();

            currentCatalog.Categories.Add(categoryLinkToAdd);
            categoryLinkToAdd.Catalogs.Add(currentCatalog);
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [ODataRoute("Catalogs({key})/Categories({relatedKey})/$ref")]
        public IHttpActionResult UpdateLinkToCatalog([FromODataUri] int key, [FromODataUri] int relatedKey, [FromBody] Uri link)
        {
            var currentCatalog = _ctx.Catalogs.Include("Categories").FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            var categoryToRemove = currentCatalog.Categories.FirstOrDefault(f => f.CategoryId == relatedKey);

            if (categoryToRemove == null)
                return NotFound();

            int keyOfCategoryToAdd = Request.GetKeyValue<int>(link);

            if (currentCatalog.Categories.Any(i => i.CategoryId == keyOfCategoryToAdd))
                return BadRequest($"The category with id {keyOfCategoryToAdd} is already linked to this catalog");


            var categoryLinkToAdd = _ctx.Categories.Include("Catalog").FirstOrDefault(f => f.CategoryId == keyOfCategoryToAdd);

            if (categoryLinkToAdd == null)
                return NotFound();


            currentCatalog.Categories.Remove(categoryToRemove);
            _ctx.Categories.Remove(categoryToRemove);


            currentCatalog.Categories.Add(categoryLinkToAdd);
            categoryLinkToAdd.Catalogs.Add(currentCatalog);
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [ODataRoute("Catalogs({key})/Categories({relatedKey})/$ref")]
        public IHttpActionResult DeleteLinkToCategory([FromODataUri] int key, [FromODataUri] int relatedKey)
        {
            var currentCatalog = _ctx.Catalogs.Include("Categories").FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            var categoryToRemove = currentCatalog.Categories.FirstOrDefault(f => f.CategoryId == relatedKey);

            if (categoryToRemove == null)
                return NotFound();

            categoryToRemove.Catalogs = null;
            currentCatalog.Categories.Remove(categoryToRemove);
            _ctx.Categories.Remove(categoryToRemove);

            _ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);

        }

        private const short US_CATALOG_ID = 1;
        [HttpGet]
        [ODataRoute("US Catalog")]
        [EnableQuery]
        public IHttpActionResult GetSingletonUSCatalog()
        {
            var catalog = _ctx.Catalogs.Where(c => c.CatalogId == US_CATALOG_ID);

            if (!catalog.Any())
                return NotFound();

            return Ok(SingleResult.Create(catalog));
        }

        protected override void Dispose(bool disposing)
        {
            _ctx?.Dispose();
            base.Dispose(disposing);
        }
    }
}
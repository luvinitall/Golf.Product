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
    public class ProductsController : ODataController
    {
        GolfProductDbContext _ctx = new GolfProductDbContext();

        [EnableQuery]
        public IHttpActionResult Get()
        {
            return Ok(_ctx.Products);

        }
        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            var product = _ctx.Products.Where(p => p.ProductId == key);

            if (!product.Any())
                return NotFound();

            return Ok(SingleResult.Create(product));

        }

        [HttpGet]
        [ODataRoute("Products/Golf.Product.Functions.GetSets")]
        public IHttpActionResult GetSets()
        {
            return this.CreateOKHttpActionResult(_ctx.Products.Where(p =>  p.Description.IndexOf("-") > 0)); 
        }


        [HttpPost]
        [ODataRoute("Products/Golf.Product.Actions.SetAllProductsAsRightHanded")]
        public IHttpActionResult SetAllProductsAsRightHanded()
        {
            foreach (var product in _ctx.Products)
            {
                product.Hand = Hand.Right;
            }

            if (_ctx.SaveChanges()  > -1)
                return Ok();

            return InternalServerError();

          
        }

        //[HttpGet]
        //[ODataRoute("Products({key})/Description")]
        //[ODataRoute("Products({key})/Sku")]
        //[ODataRoute("Products({key})/Hand")]
        //[ODataRoute("Products({key})/Gender")]
        //public IHttpActionResult GetProductProperty([FromODataUri] int key)
        //{
        //    var product = _ctx.Products.FirstOrDefault(p => p.ProductId == key);

        //    if (product == null)
        //        return NotFound();

        //    var propertyToGet = Request.RequestUri.Segments.Last();

        //    if (!product.HasProperty(propertyToGet))
        //        return NotFound();

        //    var propertyValue = product.GetValue(propertyToGet);

        //    if (propertyValue == null)
        //        return StatusCode(System.Net.HttpStatusCode.NoContent);


        //    return this.CreateOKHttpActionResult(propertyValue);
        //}



        //[HttpGet]
        //[ODataRoute("Products({key})/Description/$value")]
        //[ODataRoute("Products({key})/Sku/$value")]
        //[ODataRoute("Products({key})/Hand/$value")]
        //[ODataRoute("Products({key})/Gender/$value")]
        //public IHttpActionResult GetPersonPropertyRawValue([FromODataUri] int key)
        //{
        //    var product = _ctx.Products.FirstOrDefault(p => p.ProductId == key);

        //    if (product == null)
        //        return NotFound();

        //    var propertyToGet = Request.RequestUri.Segments[Request.RequestUri.Segments.Length - 2].TrimEnd('/');

        //    if (!product.HasProperty(propertyToGet))
        //        return NotFound();

        //    var propertyValue = product.GetValue(propertyToGet);

        //    if (propertyValue == null)
        //        return StatusCode(System.Net.HttpStatusCode.NoContent);


        //    return this.CreateOKHttpActionResult(propertyValue.ToString());
        //}

        //public IHttpActionResult Post(Model.Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _ctx.Products.Add(product);
        //    _ctx.SaveChanges();

        //    return Created(product);
        //}

        //public IHttpActionResult Put([FromODataUri] int key, Model.Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var currentproduct = _ctx.Products.FirstOrDefault(p => p.ProductId == key);
        //    if (currentproduct == null)
        //        return NotFound();

        //    product.ProductId = currentproduct.ProductId;


        //    _ctx.Entry(currentproduct).CurrentValues.SetValues(product);

        //    _ctx.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //public IHttpActionResult Patch([FromODataUri] int key, Delta<Model.Product> patch)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);


        //    var currentproduct = _ctx.Products.FirstOrDefault(p => p.ProductId == key);
        //    if (currentproduct == null)
        //        return NotFound();

        //    patch.Patch(currentproduct);
        //    _ctx.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //public IHttpActionResult Delete([FromODataUri] int key)
        //{
        //    var currentproduct = _ctx.Products.FirstOrDefault(p => p.ProductId == key);
        //    if (currentproduct == null)
        //        return NotFound();

        //    _ctx.Products.Remove(currentproduct);
        //    _ctx.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        protected override void Dispose(bool disposing)
        {
            _ctx?.Dispose();
            base.Dispose(disposing);
        }
    }
}
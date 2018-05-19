using System.Linq;
using System.Web.Http;
using System.Web.OData;
using Golf.Product.DataAccessLayer;

namespace Golf.Product.Controllers
{
    public class ProductsController : ODataController
    {
        GolfProductDbContext _ctx = new GolfProductDbContext();

        public IHttpActionResult Get()
        {
            return Ok(_ctx.Products);

        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            var product = _ctx.Products.FirstOrDefault(p => p.ProductId == key);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx?.Dispose();
            base.Dispose(disposing);
        }
    }
}
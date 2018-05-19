using System.Web.Http;
using System.Web.OData;
using Golf.Product.DataAccessLayer;

namespace Golf.Product.Controllers
{
    public class ProductController : ODataController
    {
        GolfProductDbContext _ctx = new GolfProductDbContext();

        public IHttpActionResult Get()
        {
            return Ok(_ctx.Products);

        }

        protected override void Dispose(bool disposing)
        {
            _ctx?.Dispose();
            base.Dispose(disposing);
        }
    }
}
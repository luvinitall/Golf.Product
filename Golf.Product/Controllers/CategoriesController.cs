using System.Web.Http;
using System.Web.OData;
using Golf.Product.DataAccessLayer;

namespace Golf.Product.Controllers
{
    public class CategoriesController : ODataController
    {
        GolfProductDbContext _ctx = new GolfProductDbContext();

        public IHttpActionResult Get()
        {
            return Ok(_ctx.Categories);

        }

        protected override void Dispose(bool disposing)
        {
            _ctx?.Dispose();
            base.Dispose(disposing);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Golf.Product.DataAccessLayer;
using Golf.Product.Helpers;
using Golf.Product.Model;

namespace Golf.Product.Controllers
{
    public class FamiliesController : ODataController
    {
        GolfProductDbContext _ctx = new GolfProductDbContext();

        public IHttpActionResult Get()
        {
            return Ok(_ctx.Families);

        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            var family = _ctx.Families.FirstOrDefault(f => f.FamilyId == key);

            if (family == null)
                return NotFound();

            return Ok(family);
        }

        [HttpGet]
        [ODataRoute("Families({key})/Description")]
        public IHttpActionResult GetFamilyProperty([FromODataUri] int key)
        {
            var family = _ctx.Families.FirstOrDefault(c => c.FamilyId == key);

            if (family == null)
                return NotFound();

            var propertyToGet = Url.Request.RequestUri.Segments.Last();

            if (!family.HasProperty(propertyToGet))
                return NotFound();

            var propertyValue = family.GetValue(propertyToGet);

            if (propertyValue == null)
                return StatusCode(System.Net.HttpStatusCode.NoContent);


            return this.CreateOKHttpActionResult(propertyValue);
        }

        [HttpGet]
        [ODataRoute("Families({key})/Products")]
        public IHttpActionResult GetFamilyCollectionProperty([FromODataUri] int key)
        {
            var propertyToGet = Url.Request.RequestUri.Segments.Last();

            var family = _ctx.Families.Include(propertyToGet).FirstOrDefault(c => c.FamilyId == key);

            if (family == null)
                return NotFound();


            var propertyValue = family.GetValue(propertyToGet);



            return this.CreateOKHttpActionResult(propertyValue);
        }

        [HttpGet]
        [ODataRoute("Families({key})/Description/$value")]
        public IHttpActionResult GetFamilyPropertyRawValue([FromODataUri] int key)
        {
            var family = _ctx.Families.FirstOrDefault(c => c.FamilyId == key);

            if (family == null)
                return NotFound();

            var propertyToGet = Url.Request.RequestUri.Segments[Url.Request.RequestUri.Segments.Length - 2].TrimEnd('/');

            if (!family.HasProperty(propertyToGet))
                return NotFound();

            var propertyValue = family.GetValue(propertyToGet);

            if (propertyValue == null)
                return StatusCode(System.Net.HttpStatusCode.NoContent);


            return this.CreateOKHttpActionResult(propertyValue.ToString());
        }

        public IHttpActionResult Post(Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _ctx.Families.Add(family);
            _ctx.SaveChanges();

            return Created(family);
        }

        public IHttpActionResult Put([FromODataUri] int key, Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentFamily = _ctx.Families.FirstOrDefault(c => c.FamilyId == key);
            if (currentFamily == null)
                return NotFound();

            family.FamilyId = currentFamily.FamilyId;


            _ctx.Entry(currentFamily).CurrentValues.SetValues(family);

            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Patch([FromODataUri] int key, Delta<Family> patch)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var currentFamily = _ctx.Families.FirstOrDefault(c => c.FamilyId == key);
            if (currentFamily == null)
                return NotFound();

            patch.Patch(currentFamily);
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var currentFamily = _ctx.Families.Include("Products").FirstOrDefault(c => c.FamilyId == key);
            if (currentFamily == null)
                return NotFound();

            //Can not delete if the family is linked to any families;
            if (currentFamily.Products.Any())
                return Content(HttpStatusCode.Conflict, "This family is being referenced by other products.");

            _ctx.Families.Remove(currentFamily);
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
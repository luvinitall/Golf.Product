﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using Golf.Product.DataAccessLayer;

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

        protected override void Dispose(bool disposing)
        {
            _ctx?.Dispose();
            base.Dispose(disposing);
        }
    }
}
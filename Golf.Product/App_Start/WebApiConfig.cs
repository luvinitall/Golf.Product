﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Query;
using Golf.Product.Model;
using Microsoft.OData.Edm;


namespace Golf.Product
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapODataServiceRoute("ODataRoute", "odata", GetEdmModel());
            //config.EnsureInitialized();
            config.Select().Expand().Filter().OrderBy().MaxTop(10).Count();
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "Golf.Product";
            builder.ContainerName = "Golf.ProductContainer";
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Family>("Families");
            builder.EntitySet<Model.Product>("Products");
            return builder.GetEdmModel();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Formatter;
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

            //var odataFormatters = ODataMediaTypeFormatters.Create();
            //config.Formatters.InsertRange(0, odataFormatters);



            //config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");
            //config.Formatters.XmlFormatter.AddQueryStringMapping("$format", "xml", "application/xml");




        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "Golf.Product";
            builder.ContainerName = "Golf.ProductContainer";
            builder.EntitySet<Catalog>("Catalogs");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Family>("Families");
            builder.EntitySet<Model.Product>("Products");

            var areBestSellingProducts =
                builder.EntityType<Model.Product>().Collection.Function("GetSets");
            areBestSellingProducts.ReturnsCollectionFromEntitySet<Model.Product>("Products");
            areBestSellingProducts.Namespace = "Golf.Product.Functions";

            var updateAllClubsToRightHanded = builder.EntityType<Model.Product>().Collection.Action("SetAllProductsAsRightHanded");
            updateAllClubsToRightHanded.Namespace = "Golf.Product.Actions";

            builder.Singleton<Catalog>("US Catalog");

            return builder.GetEdmModel();
        }
    }
}

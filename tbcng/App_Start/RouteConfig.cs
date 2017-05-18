using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace tbcng
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               "gioi thieu",
               "gioi-thieu",
               new { controller = "Home", action = "About" }
             );
            routes.MapRoute(
               "dich vu",
               "dich-vu",
               new { controller = "Home", action = "Service" }
             );
            //routes.MapRoute(
            //   "san pham",
            //   "san-pham",
            //   new { controller = "Products", action = "Grid" }
            // );
            routes.MapRoute(
               "Admin",
               "admin",
               new { controller = "Admin", action = "Index" }
             );

            routes.MapRoute(
              "LoginAccount",
              "admin/login",
              new { controller = "Account", action = "Login" }
            );

            //AdminAddCat 
            routes.MapRoute(
             "AdminAddCat",
             "admin/cat/add",
             new { controller = "Cats", action = "Add" }
           );

            routes.MapRoute(
            "AdminEditCat",
            "admin/cat/edit/{id}",
            new { controller = "Cats", action = "Edit", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "AdminDeleteCat",
            "admin/cat/delete/{id}",
            new { controller = "Cats", action = "Delete", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "AdminListCat",
            "admin/cat/list",
            new { controller = "Cats", action = "List" }
          );

            //AdminAddProduct
            routes.MapRoute(
             "AdminAddProduct",
             "admin/product/add",
             new { controller = "Products", action = "Add" }
           );

            routes.MapRoute(
            "AdminEditProduct",
            "admin/product/edit/{id}",
            new { controller = "Products", action = "Edit", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "AdminDeleteProduct",
            "admin/product/delete/{id}",
            new { controller = "Products", action = "Delete", id = UrlParameter.Optional }
          );

            //AdminRestoreOffice
            routes.MapRoute(
           "AdminRestoreProduct",
           "admin/product/restore/{id}",
           new { controller = "Products", action = "Restore", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            "AdminListProduct",
            "admin/product/list",
            new { controller = "Products", action = "List" }
          );

            #region 404 Notfound
            // 404 not found
            routes.MapRoute(
                "NotFound",
                "{url}",
                new { controller = "Home", action = "NotFoundPage" }
            );
            #endregion

            //routes.MapRoute(
            //    "chitietsanpham",
            //    "san-pham/{danhmuc}/{tensanpham}-{id}",
            //    new { controller = "Home", action = "ProductDetail", danhmuc = UrlParameter.Optional, tensanpham = UrlParameter.Optional, id = UrlParameter.Optional }
            //);

            // Chi tiết sản phẩm
            routes.Add("chitietsanpham", new SeoFriendlyRoute("san-pham/{danhmuc}/{tensanpham}-{id}",
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "ProductDetail",
                        danhmuc = UrlParameter.Optional,
                        tensanpham = UrlParameter.Optional,
                        id = UrlParameter.Optional
                    }),
                new MvcRouteHandler()));

            // danh mục sản phẩm
            routes.Add("danhmucsanpham", new SeoFriendlyRoute("danh-muc/{url}-{id}",
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "ProductCat",
                        url = UrlParameter.Optional,
                        id = UrlParameter.Optional
                    }),
                new MvcRouteHandler()));
            //routes.MapRoute(
            //    "danhmucsanpham",
            //    "danh-muc/{url}-{id}",
            //    new { controller = "Home", action = "ProductCat", id = UrlParameter.Optional, url = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }

    public class SeoFriendlyRoute : Route
    {
        public SeoFriendlyRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);

            if (routeData != null)
            {
                if (routeData.Values.ContainsKey("id"))
                    routeData.Values["id"] = GetIdValue(routeData.Values["id"]);
            }

            return routeData;
        }

        private object GetIdValue(object id)
        {
            if (id != null)
            {
                string idValue = id.ToString();

                var regex = new Regex(@"^(?<id>\d+).*$");
                var match = regex.Match(idValue);

                if (match.Success)
                {
                    return match.Groups["id"].Value;
                }
            }

            return id;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tbcng.Models;

namespace tbcng.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (thietbicnEntities db = new thietbicnEntities())
            {
                ViewBag.SoDanhMuc = db.cats.Count();
                ViewBag.SoSanPham = db.products.Count();
            }
            return View();
        }
    }
}
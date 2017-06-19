using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tbcng.Models;
using PagedList;
using PagedList.Mvc;

namespace tbcng.Controllers
{
    public class HomeController : Controller
    {
        private thietbicnEntities db = new thietbicnEntities();

        public ActionResult Index()
        {
            try
            {
                ViewBag.products = (from q in db.products select q).OrderByDescending(o => o.loads).ThenByDescending(o => o.product_id).Take(15).ToList();//db.products.OrderByDescending(o => o.loads).ThenByDescending(o=>o.product_id).Take(15);
            }
            catch { 
            }
            return View();
        }
        public ActionResult DanhMucSanPhamPartial()
        {
            var data = db.cats.Where(x => x.cat_parent_id == 44).ToList();

            return PartialView("DanhMucSanPhamPartial", data);
        }
        public ActionResult ProductWithCatelog(int? cat_id)
        {
            var data = db.products.Where(x => x.cat_id == cat_id || x.cat_id_2 == cat_id || x.cat_id_3 == cat_id).Take(10).ToList();
            return PartialView("_ProductWithCatelog", data);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Service()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public class itemContact
        {
            public string fullname3 { get; set; }
            public string email3 { get; set; }
            public string subject3 { get; set; }
            public string comment3 { get; set; }

        }
        public ActionResult SubmitContact(itemContact item)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NotFoundPage(string aspxerrorpath)
        {
            if (!string.IsNullOrEmpty(aspxerrorpath))
            {
                return RedirectToRoute("NotFound");
            }
            return View();
        }

        public ActionResult LoadProductNewHot()
        {
            //var model = (from o in db.products where o.status == true && o.product_new_type == 2 && o.product_photo2 != null orderby o.updated_date descending select o).ToList().Take(10).ToList();
            var model = (from s in db.products where s.status == true orderby s.updated_date descending select s).ToList().Take(10).ToList();
            
            return PartialView("_SectionProductHot", model);
        }

        public ActionResult ProductDetail(long? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("NotFound");
            }

            var _product = (from o in db.products where o.status == true && o.product_id == id select o).FirstOrDefault();
            if (_product == null)
            {
                return RedirectToRoute("NotFound");
            }

            return View(_product);
        }

        public ActionResult LoadProductInvolve(long? id)
        {
            var model = (from s in db.products where s.product_id != id && s.status == true orderby s.updated_date descending select s).ToList().Take(10).ToList();
            return PartialView("_LoadProductInvolve", model);
        }

        public ActionResult LoadProductNew()
        {
            var model = (from s in db.products where s.status == true orderby s.updated_date descending select s).ToList().Take(10).ToList();
            return PartialView("_LoadProductNew", model);
        }

        //public ActionResult ProductCat(int? id, int? pg)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return RedirectToRoute("NotFound");
        //    }

        //    var _cat = (from o in db.cats where o.cat_id == id select o).FirstOrDefault();
        //    if (_cat == null)
        //    {
        //        return RedirectToRoute("NotFound");
        //    }

        //    ViewBag.TenDanhMuc = _cat.cat_name;
        //    ViewBag.URLDanhMuc = _cat.cat_url;
        //    ViewBag.IdDanhMuc = id;

        //    int pageSize = 10;
        //    if (pg == null) pg = 1;
        //    int pageNumber = (pg ?? 1);
        //    ViewBag.pg = pg;
        //    var data = (from q in db.products where q.cat_id == id && q.status == true select q);
        //    if (data == null)
        //    {
        //        return View(data);
        //    }

        //    return View(data.ToList().ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult ProductCat(int? pg, int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("NotFound");
            }
            var _cat = db.cats.Where(x => x.cat_id == id).FirstOrDefault();
            if (_cat == null)
            {
                return RedirectToRoute("NotFound");
            }

            ViewBag.TenDanhMuc = _cat.cat_name;
            ViewBag.URLDanhMuc = _cat.cat_url;
            ViewBag.IdDanhMuc = id;

            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;
            var data = GetArticleOfCat(id);

            if (data.Count() == 0)
            {
                return View();
            }

            data = data.Where(x => x.status == true).ToList().OrderByDescending(x=>x.updated_date);
            return View(data.ToPagedList(pageNumber, pageSize));
        }

        public void SetArticles(ICollection<cat> ic, List<product> _articles)
        {
            foreach (var c1 in ic)
            {
                if (c1.products.Count > 0)
                {
                    _articles.AddRange(c1.products);
                }
                if (c1.cats1.Count > 0)
                {
                    SetArticles(c1.cats1, _articles);
                }
            }
        }


        public IEnumerable<product> GetArticleOfCat(int? id)
        {
            var _cat = db.cats.Where(x => x.cat_id == id).FirstOrDefault();
            List<product> _articles = new List<product>();
            if (_cat != null)
            {
                ViewBag.category = _cat.cat_name;
                if (_cat.cats1.Count > 0)
                {
                    SetArticles(_cat.cats1, _articles);
                }
                else
                {
                    _articles.AddRange(_cat.products);
                }
            }
            else
            {
                _articles = null;
            }
            return _articles;
        }



        public ActionResult LoadMenu()
        {
            var menu = (from c in db.cats where c.cat_parent_id != null select c);
            string _menu = "";
            foreach (var item in menu)
            {
                var li = string.Format("<li><a href='/danh-muc/{0}-{1}'>{2}</a>", item.cat_url, item.cat_id, item.cat_name);
                _menu += li + "</li>";
            }
            return PartialView("_MenuPartial", _menu);
        }



        public ActionResult LoadMultiProductCat()
        {
            var model = from c in db.cats where c.cat_parent_id == 1 select c;
            return PartialView("_LoadProductMulCat", model.ToList());
        }


        public PartialViewResult _MenuLeftPartial()
        {
            List<DanhMuc> data =  db.cats.Select(x => new DanhMuc()
            {
                CatId = x.cat_id,
                CatName = x.cat_name,
                ParentId = x.cat_parent_id,
                PositionIndex = x.cat_pos,
                CatUrl = x.cat_url
            }).OrderBy(o => o.PositionIndex).ToList();

            var presidents = data.Where(x => x.ParentId == null).FirstOrDefault();
            SetChildren(presidents, data);

            return PartialView("_MenuLeftPartial", presidents);
        }

        private void SetChildren(DanhMuc model, List<DanhMuc> danhmuc)
        {
            var childs = danhmuc.Where(x => x.ParentId == model.CatId).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildren(child, danhmuc);
                    model.DanhMucs.Add(child);
                }
            }
        }

        public ActionResult LoadProductInCat(int cat_id)
        {
            var model = GetArticleOfCat(cat_id).OrderByDescending(x=>x.product_id).Take(10).ToList();
            return PartialView("_LoadProductInCat", model);
        }
        

    }
}
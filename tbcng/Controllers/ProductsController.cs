using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tbcng.Models;
using tbcng.Helpers;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Configuration;
using tbcng.Helpers;
namespace tbcng.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private thietbicnEntities db = new thietbicnEntities();
        // GET: Products
        public ActionResult List(int? pg, string search)
        {
            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;
            var data = db.products.Select(x=>x);
            if (data == null)
            {
                return View(data);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                data = data.Where(x => x.product_name.ToLower().Contains(search));
                ViewBag.search = search;
            }

            data = data.OrderBy(x => x.updated_date);
            return View(data.ToList().ToPagedList(pageNumber, pageSize));
        }
        // GET: Products
        public ActionResult Grid(int? pf,int? pt,int? pg, string search,int? order,int? cat_id)
        {
            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;
            var data = db.products.Select(x => x);
            if (data == null)
            {
                return View(data);
            }
            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim();
                data = data.Where(x => x.product_name.ToLower().Contains(search));
                ViewBag.search = search;
            }
            else search = "all";
            ViewBag.cat_id = -1;
            if (cat_id != null && cat_id != -1 && cat_id != 0)
            {
                data = data.Where(x => x.cat_id==cat_id);
                ViewBag.cat_id = cat_id;
                ViewBag.catname = configs.getcatname(cat_id);
            }
            ViewBag.pf = 0;
            if (pf != null && pf != -1 && pf != 0)
            {
                data = data.Where(x => x.product_price_public >= pf);
                ViewBag.pf = pf;
            }
            ViewBag.pt = 0;
            if (pt != null && pt != -1 && pt != 0)
            {
                data = data.Where(x => x.product_price_public <= pt);
                ViewBag.pt = pt;
            }
            if (order == null) order = 4;
            if (order == 1) data = data.OrderBy(x => x.product_price_public);
            else if (order == 2) data = data.OrderByDescending(x => x.product_price_public);
            else if (order == 3) data = data.OrderBy(x => x.product_id);
            else if (order == 4) data = data.OrderByDescending(x => x.product_id);

            ViewBag.search = search;
            ViewBag.order = order;
            return View(data.ToList().ToPagedList(pageNumber, pageSize));
        }
        // GET: Cats
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errored"] = "Vui lòng kiểm tra lại các trường.";
                return RedirectToRoute("AdminAddProduct");
            }

            try
            {
                long? idproduct = 0;
                product _new = new product();
                _new.cat_id = model.cat_id ?? null;
                _new.product_name = model.product_name ?? null;
                _new.product_content = model.product_content ?? null;
                _new.product_photo = model.product_photo ?? null;
                _new.product_photo2 = model.product_photo2 ?? null;
                _new.product_price_public = model.product_price_public ?? null;
                //_new.product_type = model.product_type ?? null;
                _new.product_new_type = model.product_new_type ?? null;
                _new.status = model.status;
                _new.updated_date = DateTime.Now;
                _new.product_des = model.product_des ?? null;
                _new.g=model.g;
                _new.h=model.h;
                _new.l=model.l;
                _new.w = model.w;
                _new.lang = model.lang;
                db.products.Add(_new);
                
                await db.SaveChangesAsync();
                idproduct = _new.product_id;

                TempData["Updated"] = "Thêm sản phẩm thành công";
                return RedirectToRoute("AdminEditProduct", new { id = idproduct });
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm mới");
                configs.SaveTolog(ex.ToString());
                return View(model);
            }

        }

        //updateanh1
        [HttpPost]
        public ActionResult updateanh1(long? id, string anh_url)
        {
            try
            {
                var sql = "update products set product_photo = '" + anh_url + "' where product_id = " + id;
                var update = db.Database.ExecuteSqlCommand(sql);
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult updateanh2(long? id, string anh_url)
        {
            try
            {
                var sql = "update products set product_photo2 = '" + anh_url + "' where product_id = " + id;
                var update = db.Database.ExecuteSqlCommand(sql);
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return RedirectToRoute("Admin");
            }
            product _model = await db.products.FindAsync(id);
            if (_model == null)
            {
                return View(_model);
            }
            var getPro = new ProductVM()
            {
                cat_id = _model.cat_id,
                product_content = _model.product_content,
                product_id = _model.product_id,
                product_name = _model.product_name,
                product_new_type = _model.product_new_type,
                product_photo = _model.product_photo,
                product_photo2 = _model.product_photo2,
                product_price_public = _model.product_price_public,
                //product_type = _model.product_type,
                status = (bool)_model.status,
                product_des = _model.product_des,
                g=_model.g,
                h=_model.h,
                l=_model.l,
                w=_model.w,
                lang=_model.lang,
            };

            ViewBag.TenCat = _model.product_name;
            return View(getPro);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errored"] = "Vui lòng kiểm tra lại các trường.";
                return RedirectToRoute("AdminEditProduct", new { id = model.product_id });
            }
            try
            {
                var _model = await db.products.FindAsync(model.product_id);
                if (_model != null)
                {
                    _model.cat_id = model.cat_id ?? null;
                    _model.product_name = model.product_name ?? null;
                    _model.product_content = model.product_content ?? null;
                    //_model.product_photo = model.product_photo ?? null;
                    //_model.product_photo2 = model.product_photo2 ?? null;
                    _model.product_price_public = model.product_price_public ?? null;
                    //_model.product_type = model.product_type ?? null;
                    _model.product_new_type = model.product_new_type ?? null;
                    _model.status = model.status;
                    _model.updated_date = DateTime.Now;
                    _model.product_des = model.product_des;
                    _model.g = model.g;
                    _model.h = model.h;
                    _model.l = model.l;
                    _model.w = model.w;
                    _model.lang = model.lang;
                    db.Entry(_model).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Updated"] = "Cập nhật sản phẩm thành công";
                }
            }
            catch (Exception ex)
            {
                TempData["Errored"] = "Có lỗi xảy ra khi cập nhật danh mục.";
                configs.SaveTolog(ex.ToString());
                return RedirectToRoute("AdminEditProduct", new { id = model.product_id });
            }
            return RedirectToRoute("AdminListProduct");

        }
        public ActionResult Detail(long? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Grid");
            }
            product _model = db.products.Find(id);
            if (_model == null)
            {
                return View();
            }
            ViewBag.cat = db.cats.Find(_model.cat_id).cat_name;
            return View(_model);
        }
        public ActionResult Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("Admin");
            }
            product _model = db.products.Find(id);
            if (_model == null)
            {
                return View();
            }
            return View(_model);
        }

        //tailennhieuanh
        public ActionResult tailennhieuanh(long? product_id)
        {
            bool isSaved = true;
            int fName = 0;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images\\photos", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var _fileName = Guid.NewGuid().ToString("N") + ".jpg";

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, _fileName);
                        //System.Drawing.Image bm = System.Drawing.Image.FromStream(file.InputStream);
                        // Thay đổi kích thước ảnh
                        //bm = ResizeBitmap((Bitmap)bm, 400, 310); /// new width, height
                        // Giảm dung lượng ảnh trước khi lưu
                        //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        //ImageCodecInfo ici = null;
                        //foreach (ImageCodecInfo codec in codecs)
                        //{
                        //    if (codec.MimeType == "image/jpeg")
                        //        ici = codec;
                        //}
                        //EncoderParameters ep = new EncoderParameters();
                        //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                        //bm.Save(path, ici, ep);
                        //bm.Save(path);
                        file.SaveAs(path);
                        string file_url = "/images/photos/" + _fileName;
                        var update_img_product = db.Database.ExecuteSqlCommand("INSERT INTO product_img(img_url,product_id) VALUES('" + file_url + "'," + product_id + ")");
                        
                        fName = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                Helpers.configs.SaveTolog(ex.ToString());
            }
            if (isSaved)
            {
                return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Có lỗi khi lưu tệp tin" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            product _model = await db.products.FindAsync(id);
            if (_model == null)
            {
                return View();
            }
            if (_model.product_img.Count() > 0)
            {
                TempData["Error"] = "Bạn không thể xóa sản phẩm này. <br />";
                return RedirectToRoute("AdminDeleteProduct", new { id = _model.product_id });
            }
            try
            {
                db.products.Remove(_model);
                await db.SaveChangesAsync();
                TempData["Deleted"] = "Sản phẩm đã được xóa khỏi danh sách.";
            }
            catch (Exception ex)
            {
                configs.SaveTolog(ex.ToString());
            }

            return RedirectToRoute("AdminListProduct");
        }

        public PartialViewResult _lstOptionCatPartial()
        {
            List<LstCat> data = db.cats.Select(x => new LstCat()
            {
                CatId = x.cat_id,
                CatName = x.cat_name,
                ParentCatId = x.cat_parent_id,
                CatPos = x.cat_pos,
                CatURL = x.cat_url
            }).OrderBy(x => x.CatPos).ToList();

            var presidents = data.Where(x => x.ParentCatId == null).FirstOrDefault();
            SetChildrenCat(presidents, data);
            return PartialView("_lstOptionCatPartial", presidents);
        }

        private void SetChildrenCat(LstCat model, List<LstCat> danhmuc)
        {
            var childs = danhmuc.Where(x => x.ParentCatId == model.CatId).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildrenCat(child, danhmuc);
                    model.LstCats.Add(child);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SaveImage()
        {
            bool isSaved = true;
            var fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images\\photos", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var _fileName = Guid.NewGuid().ToString("N") + ".jpg";

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, _fileName);
                        System.Drawing.Image bm = System.Drawing.Image.FromStream(file.InputStream);
                        // Thay đổi kích thước ảnh
                        bm = ResizeBitmap((Bitmap)bm, 400, 310); /// new width, height
                        // Giảm dung lượng ảnh trước khi lưu
                        //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        //ImageCodecInfo ici = null;
                        //foreach (ImageCodecInfo codec in codecs)
                        //{
                        //    if (codec.MimeType == "image/jpeg")
                        //        ici = codec;
                        //}
                        //EncoderParameters ep = new EncoderParameters();
                        //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                        //bm.Save(path, ici, ep);
                        bm.Save(path);
                        //file.SaveAs(path);
                        fName = "/images/photos/" + _fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                Helpers.configs.SaveTolog(ex.ToString());
            }
            if (isSaved)
            {
                return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Có lỗi khi lưu tệp tin" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveImageBig()
        {
            bool isSaved = true;
            var fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images\\photos", Server.MapPath(@"\")));
                        //string strDay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString();
                        //string strDay = DateTime.Now.ToString("yyyyMM");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var _fileName = Guid.NewGuid().ToString("N") + ".jpg";

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, _fileName);
                       // System.Drawing.Image bm = System.Drawing.Image.FromStream(file.InputStream);
                        // Thay đổi kích cỡ
                        //bm = ResizeBitmap((Bitmap)bm, 1920, 790); /// new width, height
                        //// Giảm dung lượng ảnh trước khi lưu
                        //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        //ImageCodecInfo ici = null;
                        //foreach (ImageCodecInfo codec in codecs)
                        //{
                        //    if (codec.MimeType == "image/jpeg")
                        //        ici = codec;
                        //}
                        //EncoderParameters ep = new EncoderParameters();
                        //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                        //bm.Save(path, ici, ep);
                        //bm.Save(path);
                        file.SaveAs(path);
                        fName = "/images/photos/" + _fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                Helpers.configs.SaveTolog(ex.ToString());
            }
            if (isSaved)
            {
                return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Có lỗi khi lưu tệp tin" }, JsonRequestBehavior.AllowGet);
            }
        }

        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }


        public ActionResult LoadPhotoProduct(long? id)
        {
            var model = db.products.Find(id).product_img.ToList();
            return PartialView("_LoadPhotoProduct", model);
        }
        public ActionResult LoadPhotoProduct2(long? id)
        {
            var model = db.products.Find(id).product_img.ToList();
            return PartialView("_LoadPhotoProduct2", model);
        }
        public ActionResult LoadRelateProduct(int? cat_id,long? product_id)
        {
            var model = (from q in db.products where q.cat_id==cat_id && q.product_id!=product_id select q).OrderByDescending(o=>o.product_id).ToList();
            return PartialView("LoadRelateProduct", model);
        }
        public ActionResult LoadCart()
        {
            try {
                string session = Helpers.configs.getCookie("session");
                var model = (from q in db.product_order where q.session == session select q).OrderBy(o => o.id).ToList();//Đoạn này cần dùng query sql
                ViewBag.count = model.Count;
                return PartialView("LoadCart", model);
            }
            catch
            {
                ViewBag.count = 0;
                return PartialView("LoadCart", null);
            }
        }
        public string addToCart(long? product_id)
        {
            try
            {
                string session = Helpers.configs.getCookie("session");
                if (session == "")
                {
                    session=Guid.NewGuid().ToString();
                    Helpers.configs.setCookie("session",session);
                }
                product pr=db.products.Find(product_id);
                product_order po = new product_order();
                po.customer_id = null;
                po.date_time = DateTime.Now;
                po.product_id = product_id;
                po.product_name = pr.product_name;
                po.product_photos = pr.product_photo;
                po.product_price = pr.product_price_public;
                po.product_total = pr.product_price_public*1;
                po.quantity = 1;
                po.session = session;
                po.status = 0;
                db.product_order.Add(po);
                db.SaveChanges();
                string rs = "{\"product_photos\":\"" + pr.product_photo + "\", \"product_name\":\"" + pr.product_name + "\", \"quantity\":\"" + 1 + "\", \"product_price\":\"" + pr.product_price_public + "\"}";
                return rs;//JsonConvert.SerializeObject(rs);
            }
            catch
            {
                return "Lỗi";
            }
        }
        public string removeCartItem(long? id){
            try{
                db.Database.ExecuteSqlCommand("delete from product_order where id=" + id);
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        [HttpPost]
        public string removeCartProduct(long? product_id)
        {
            try
            {
                string session = Helpers.configs.getCookie("session");
                if (session == "")
                {
                    session = Guid.NewGuid().ToString();
                    Helpers.configs.setCookie("session", session);
                }
                db.Database.ExecuteSqlCommand("delete from product_order where product_id=" + product_id + " and session=N'" + session + "'");
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        public ActionResult Cart(string address,double? lon, double? lat)
        {
            string session = Helpers.configs.getCookie("session");
            if (session == "")
            {
                session = Guid.NewGuid().ToString();
                Helpers.configs.setCookie("session", session);
            }
            ViewBag.list = null;
            ViewBag.lon = 105.8194541;
            ViewBag.lat = 21.0227431;
            ViewBag.address = "";
            try {
                string query = "select product_id,product_name,product_photos,product_price,sum(quantity) as quantity from ";
                   query+="(";
                   query += " select product_id,product_name,product_photos,product_price,quantity from [phutunghoangia].[dbo].[product_order] where session='" + session + "' and status=0 ";
                   query += ") as A group by product_id,product_name,product_photos,product_price";
                   var list = db.Database.SqlQuery<itemCart>(query).ToList();
                   ViewBag.list = list;
            }
            catch
            {
                ViewBag.list = null;
            }
            if (lon!=null) ViewBag.lon = lon;
            if (lat != null) ViewBag.lat = lat;
            if (address != null) ViewBag.address = address;
            ViewBag.session = session;
            return View();
        }
        public ActionResult CartStep2(string session)
        {
            try
            {
                string query = "select product_id,product_name,product_photos,product_price,sum(quantity) as quantity from ";
                query += "(";
                query += " select product_id,product_name,product_photos,product_price,quantity from [phutunghoangia].[dbo].[product_order] where session='" + session + "' and status=1 ";
                query += ") as A group by product_id,product_name,product_photos,product_price";
                var list = db.Database.SqlQuery<itemCart>(query).ToList();
                ViewBag.list = list;
                var p = db.product_customer_order.Where(o => o.session == session).FirstOrDefault();
                ViewBag.ordercode = p.id;
                ViewBag.session = session;
                ViewBag.ship_fee = p.ship_fee;
                ViewBag.total = p.total;
            }
            catch
            {
                ViewBag.list = null;
            }
            return View();
        }
        //Class for "distance" and "duration" which has the "text" and "value" properties.
        public class CepElementNode
        {
            public string text { get; set; }

            public string value { get; set; }
        }

        //Class for "distance", "duration" and "status" nodes of "elements" node 
        public class CepDataElement
        {
            public CepElementNode distance { get; set; }

            public CepElementNode duration { get; set; }

            public string status { get; set; }
        }

        //Class for "elements" node
        public class CepDataRow
        {
            public List<CepDataElement> elements { get; set; }
        }

        //Class which wrap the json response
        public class RequestCepViewModel
        {
            public List<string> destination_addresses { get; set; }

            public List<string> origin_addresses { get; set; }

            public List<CepDataRow> rows { get; set; }

            public string status { get; set; }
        }
        public class itemallcart
        {
            public long product_id { get; set; }
            public int? quantity { get; set; }
        }
        public class itemallcartall
        {
            public long product_id { get; set; }
            public string product_photos { get; set; }
            public string product_name { get; set; }
            public float? product_price { get; set; }
            public int quantity { get; set; }
            public float product_total { get; set; }
        }
        public string getPriceShip(double? lon1, double? lat1, double? lon2, double? lat2, int type, string data)
        {
            try
            {
                string address = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" + lat1 + "," + lon1 + "&destinations=" + lat2 + "," + lon2 + "&mode=driving&key=AIzaSyDLPSKQ4QV4xGiQjnZDUecx-UEr3D0QePY";
                string result = new System.Net.WebClient().DownloadString(address);
                var viewModel = new JavaScriptSerializer().Deserialize<RequestCepViewModel>(result);
                var dt = viewModel.rows[0].elements[0].distance.value;
                int km = int.Parse(dt) / 1000;
                dynamic thelist = JsonConvert.DeserializeObject<List<itemallcart>>(data);
                double total_kg = 0;
                foreach (var detail in thelist)
                {
                    long product_id = detail.product_id;
                    int quantity = detail.quantity;
                    if (product_id != -1)
                    {
                        var pr = db.products.Find(product_id);
                        if (pr != null)
                        {
                            double tempkg = (double)((pr.l * pr.w * pr.h) / 6000);
                            tempkg=tempkg*1000;// Đổi sang g
                            tempkg=(double)(tempkg>pr.g?tempkg:pr.g);
                            total_kg += tempkg * quantity;
                        }
                    }
                }
                if (type == 3)
                {
                    //Tìm cước sài gòn
                    if (total_kg > 2000)
                    {
                        var q = db.ships.Where(o => o.g_from >= 2000 && o.g_to <= 1000000000).FirstOrDefault().hn_hcm;
                        double bonus =(double)((total_kg - 2000) / 500 * q);
                        var q2 = db.ships.Where(o => o.g_from >= 1500 && o.g_to <= 2000).FirstOrDefault().hn_hcm;
                        double rs=(double)(q2 + bonus);
                        return total_kg+"_"+rs;
                    }
                    else
                    {
                        var q = db.ships.Where(o => o.g_from <= (int)total_kg && o.g_to >= (int)total_kg).FirstOrDefault().hn_hcm;
                        double rs=(double)(q);
                        return total_kg+"_"+rs;
                    }

                }
                else
                {
                    //Nếu là gửi đi Đà Nẵng
                    if (type == 2)
                    {
                        if (total_kg > 2000)
                        {
                            var q = db.ships.Where(o => o.g_from >= 2000 && o.g_to <= 1000000000).FirstOrDefault().hn_dn;
                            double bonus =(double)((total_kg - 2000) / 500 * q);
                            var q2 = db.ships.Where(o => o.g_from >= 1500 && o.g_to <= 2000).FirstOrDefault().hn_dn;
                            double rs=(double)(q2 + bonus);
                            return total_kg+"_"+rs;
                        }
                        else
                        {
                            var q = db.ships.Where(o => o.g_from <= (int)total_kg && o.g_to >= (int)total_kg).FirstOrDefault().hn_dn;
                            double rs=(double)(q);
                            return total_kg+"_"+rs;
                        }
                    }
                    else
                        if (type == 1)
                        {
                            if (total_kg > 2000)
                            {
                                var q = db.ships.Where(o => o.g_from >= 2000 && o.g_to <= 1000000000).FirstOrDefault().in_city;
                                double bonus =(double)((total_kg - 2000) / 500 * q);
                                var q2 = db.ships.Where(o => o.g_from >= 1500 && o.g_to <= 2000).FirstOrDefault().in_city;
                                double rs=(double)(q2 + bonus);
                                return total_kg+"_"+rs;
                            }
                            else
                            {
                                var q = db.ships.Where(o => o.g_from <= (int)total_kg && o.g_to >= (int)total_kg).FirstOrDefault().in_city;
                                double rs=(double)(q);
                                return total_kg+"_"+rs;
                            }
                        }
                        else
                        {
                            int? price=0;
                            if (km<=100){
                                if (total_kg > 2000)
                                {
                                    var q = db.ships.Where(o => o.g_from >= 2000 && o.g_to <= 1000000000).FirstOrDefault().e_100;
                                    double bonus =(double)((total_kg - 2000) / 500 * q);
                                    var q2 = db.ships.Where(o => o.g_from >= 1500 && o.g_to <= 2000).FirstOrDefault().e_100;
                                    double rs=(double)(q2 + bonus);
                                    return total_kg+"_"+rs;
                                }
                                else
                                {
                                    var q = db.ships.Where(o => o.g_from <= (int)total_kg && o.g_to >= (int)total_kg).FirstOrDefault().e_100;
                                    double rs=(double)(q);
                                    return total_kg+"_"+rs;
                                }
                            }else{
                                if (km<=300){
                                    if (total_kg > 2000)
                                    {
                                        var q = db.ships.Where(o => o.g_from >= 2000 && o.g_to <= 1000000000).FirstOrDefault().e_300;
                                        double bonus =(double)((total_kg - 2000) / 500 * q);
                                        var q2 = db.ships.Where(o => o.g_from >= 1500 && o.g_to <= 2000).FirstOrDefault().e_300;
                                        double rs=(double)(q2 + bonus);
                                        return total_kg+"_"+rs;
                                    }
                                    else
                                    {
                                        var q = db.ships.Where(o => o.g_from <= (int)total_kg && o.g_to >= (int)total_kg).FirstOrDefault().e_300;
                                        double rs=(double)(q);
                                        return total_kg+"_"+rs;
                                    }
                                }else{
                                    if (total_kg > 2000)
                                    {
                                        var q = db.ships.Where(o => o.g_from >= 2000 && o.g_to <= 1000000000).FirstOrDefault().m_300;
                                        double bonus =(double)((total_kg - 2000) / 500 * q);
                                        var q2 = db.ships.Where(o => o.g_from >= 1500 && o.g_to <= 2000).FirstOrDefault().m_300;
                                        double rs=(double)(q2 + bonus);
                                        return total_kg+"_"+rs;
                                    }
                                    else
                                    {
                                        var q = db.ships.Where(o => o.g_from <= (int)total_kg && o.g_to >= (int)total_kg).FirstOrDefault().m_300;
                                        double rs=(double)(q);
                                        return total_kg+"_"+rs;
                                    }
                                }
                            }
                        }
                }
                return "0_0";
            }catch(Exception ex){
                return "0_0";
            }
        }
        [HttpPost]
        public string submitOrder(string data, string session, float? g, long? ship_fee, long? total_fee, string customer_address, string customer_name, string customer_email, string customer_phone, double? lon, double? lat)
        {
             try
             {
                 //customer ctm = new customer();
                 //ctm.customer_address = customer_address;
                 //ctm.customer_email = customer_email;
                 //ctm.customer_name = customer_name;
                 //ctm.customer_phone = customer_phone;
                 //ctm.lat = lat;
                 //ctm.lon = lon;
                 //db.customers.Add(ctm);
                 //db.SaveChanges();
                 //int customer_id=ctm.id;
                 //product_customer_order pco = new product_customer_order();
                 //pco.customer_id = customer_id;
                 //pco.g = g;
                 //pco.session = session;
                 //pco.ship_fee = ship_fee;
                 //pco.total_fee = total_fee;
                 //pco.total = ship_fee + total_fee;
                 //db.product_customer_order.Add(pco);
                 //db.SaveChanges();

                 string result ="";
                 result += "<table style=\"margin: 0 auto;width: 700px;border: 1px solid #cbcbcb;background: rgba(193, 193, 193, 0.08);\">"
                 + "<tr><td colspan=\"5\">Đơn Đặt Hàng</td></tr>"
                 + "<tr><td colspan=\"5\">Khách Hàng: " + customer_name + ", điện thoại:" + customer_phone + ", địa chỉ:" + customer_address + " </td></tr>"
                 + "<tr><td colspan=\"5\">Chi Tiết Đơn Hàng</td></tr>"
                 + "<tr><th>Ảnh</th><th>Sản phẩm</th><th>Giá</th><th>Số lượng</th><th>Tổng</th></tr>";
                 List<itemallcartall> thelist = JsonConvert.DeserializeObject<List<itemallcartall>>(data);
                 //double total_kg = 0;
                 int total_quantity = 0;
                 long? total = ship_fee + total_fee;
                 if (thelist.Count > 1) { 

                     foreach (var detail in thelist)
                     {
                         long product_id = detail.product_id;
                         if (product_id == -1) continue;                         
                         string product_name = detail.product_name;
                         string product_photos = detail.product_photos;
                         int quantity = detail.quantity;
                         float? product_price = detail.product_price;
                         float product_total = detail.product_total;
                         total_quantity += quantity;
                         //product_order po = new product_order();
                         //po.customer_id = customer_id;
                         //po.date_time = DateTime.Now;
                         //po.product_id = product_id;
                         //po.product_name = product_name;
                         //po.product_photos = product_photos;
                         //po.product_price = product_price;
                         //po.product_total = product_total;
                         //po.quantity = quantity;
                         //po.session = session;
                         //po.status = 1;
                         //db.product_order.Add(po);
                         //db.SaveChanges();
                         result += "<tr><td><img src=\"" + product_photos + "\"  style=\"height:50px;width:50px;\"></td><td>" + product_name + "</td><td>" + String.Format("{0:n0}", product_price) + "</td><td>" + quantity + "</td><td>" + String.Format("{0:n0}", product_total) + "</td></tr>";
                     }
                     result += "<tr><td colspan=\"2\">Tổng</td><td>Tổng số lượng " + total_quantity + "</td><td>Phí ship " + String.Format("{0:n0}", ship_fee) + "</td><td>Tổng giá trị hàng" + String.Format("{0:n0}", total_fee) + "</td></tr>";
                     result += "<tr><td colspan=\"4\">Tổng</td><td>Tổng tiền: " + String.Format("{0:n0}", total) + "</td></tr>";
                     var sendmail =configs.Sendmail(WebConfigurationManager.AppSettings["emailroot"], WebConfigurationManager.AppSettings["passroot"], "thuexevn.com@gmail.com", customer_phone + "-Khách đặt hàng", result);
                     return session;
                 }
                 
                 return "0";
             }
             catch (Exception ex)
             {
                 return "0";
                 //configs.SaveTolog(ex.ToString());
             }
        }
        //public ActionResult upanhsanpham(long? product_id, string img_url, string img_title, string img_alt)
        //{
        //    try
        //    {
        //        product_img _anhmoi = new product_img();
        //        _anhmoi.product_id = product_id ?? null;
        //        _anhmoi.img_url = img_url ?? null;
        //        _anhmoi.img_title = img_title ?? null;
        //        _anhmoi.img_alt = img_alt ?? null;
        //        db.product_img.Add(_anhmoi);
        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        configs.SaveTolog(ex.ToString());
        //    }
        //    return RedirectToRoute("AdminEditProduct", new { id = product_id });
        //}



        public ActionResult xoa_anh(long? id)
        {
            long? idproduct = 0;
            try
            {
                var photo = db.product_img.Find(id);
                if (photo != null)
                {
                    idproduct = photo.product_id;
                    db.product_img.Remove(photo);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                configs.SaveTolog(ex.ToString());
            }
            return RedirectToRoute("AdminEditProduct", new { id = idproduct });
        }
        

        //RestoreOffice       
        public async Task<ActionResult> Restore(long? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("Admin");
            }
            try
            {
                var _product = await db.products.FindAsync(id);
                if (_product != null)
                {
                    if (_product.status == false)
                    {
                        _product.status = true;
                        db.Entry(_product).State = System.Data.Entity.EntityState.Modified;
                        await db.SaveChangesAsync();

                        TempData["Updated"] = "Sản phẩm đã được khôi phục.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Errored"] = "Có lỗi xảy ra khi khôi phục.";
                configs.SaveTolog(ex.ToString());
               
            }
            return RedirectToRoute("AdminEditProduct", new { id = id });
        }

    }
}
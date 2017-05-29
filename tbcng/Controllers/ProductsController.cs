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
        public ActionResult Grid(int? pg, string search,int? order)
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
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                data = data.Where(x => x.product_name.ToLower().Contains(search));
                ViewBag.search = search;
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
        
        public ActionResult Cart()
        {
            string session = Helpers.configs.getCookie("session");
            if (session == "")
            {
                session = Guid.NewGuid().ToString();
                Helpers.configs.setCookie("session", session);
            }
            string query="select product_name,product_photos,product_price,sum(quantity) as quantity from ";
                   query+="(";
                   query+=" select product_name,product_photos,product_price,quantity from [phutunghoangia].[dbo].[product_order] where session='"+session+"' ";
                   query += ") as A group by product_name,product_photos,product_price";
                   var list = db.Database.SqlQuery<itemCart>(query).ToList();
                   ViewBag.list = list;
            return View();
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
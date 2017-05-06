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

namespace tbcng.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CatsController : Controller
    {
        private thietbicnEntities db = new thietbicnEntities();

        public ActionResult List(int? pg, string search)
        {
            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;
            var data = db.cats.Where(x => x.cat_parent_id != null);
            if (data == null)
            {
                return View(data);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                data = data.Where(x => x.cat_name.ToLower().Contains(search));
                ViewBag.search = search;
            }

            data = data.OrderBy(x => x.cat_pos);
            return View(data.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Cats
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CatVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errored"] = "Vui lòng kiểm tra lại các trường.";
                return RedirectToRoute("AdminAddCat");
            }

            try
            {
                cat _new = new cat();
                _new.cat_name = model.cat_name ?? null;
                _new.cat_parent_id = model.cat_parent_id ?? null;
                _new.cat_url = _new.cat_name != null ? configs.unicodeToNoMark(_new.cat_name) : null;
                _new.cat_pos = model.cat_pos ?? null;
                db.cats.Add(_new);
                await db.SaveChangesAsync();

                TempData["Updated"] = "Thêm danh mục thành công";
                return RedirectToRoute("AdminAddCat");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm danh mục mới");
                configs.SaveTolog(ex.ToString());
                return View(model);
            }

        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null || id == 0 || id == 1)
            {
                return RedirectToRoute("Admin");
            }
            cat _cat = await db.cats.FindAsync(id);
            if (_cat == null)
            {
                return View(_cat);
            }
            var getCat = new CatVM()
            {
                cat_id = _cat.cat_id,
                cat_name = _cat.cat_name,
                cat_parent_id = _cat.cat_parent_id,
                cat_pos = _cat.cat_pos
            };

            ViewBag.TenCat = _cat.cat_name;
            return View(getCat);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CatVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errored"] = "Vui lòng kiểm tra lại các trường.";
                return RedirectToRoute("AdminEditCat", new { id = model.cat_id });
            }
            try
            {
                var _cat = await db.cats.FindAsync(model.cat_id);
                if (_cat != null)
                {
                    _cat.cat_name = model.cat_name ?? null;
                    _cat.cat_parent_id = model.cat_parent_id ?? null;
                    _cat.cat_pos = model.cat_pos ?? null;
                    _cat.cat_url = _cat.cat_name != null ? configs.unicodeToNoMark(_cat.cat_name) : null;
                    db.Entry(_cat).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Updated"] = "Cập nhật danh mục thành công";
                }
                return RedirectToRoute("AdminEditCat", new { id = model.cat_id });
            }
            catch (Exception ex)
            {
                TempData["Errored"] = "Có lỗi xảy ra khi cập nhật danh mục.";
                configs.SaveTolog(ex.ToString());
                return RedirectToRoute("AdminEditCat", new { id = model.cat_id });
            }

        }

        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0 || id == 1)
            {
                return RedirectToRoute("Admin");
            }
            cat _cat = db.cats.Find(id);
            if (_cat == null)
            {
                return View();
            }
            return View(_cat);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            cat _cat = await db.cats.FindAsync(id);
            if (_cat == null)
            {
                return View();
            }
            if (_cat.cats1.Count() > 0)
            {
                TempData["Error"] = "Bạn không thể xóa danh mục này. <br /> Danh mục này chứa danh mục con khác. Vui lòng xóa danh mục con trước.";
                return RedirectToRoute("AdminDeleteCat", new { id = _cat.cat_id });
            }
            try
            {
                db.cats.Remove(_cat);
                await db.SaveChangesAsync();
                TempData["Deleted"] = "Danh mục đã được xóa khỏi danh sách.";
            }
            catch (Exception ex)
            {
                configs.SaveTolog(ex.ToString());
            }

            return RedirectToRoute("AdminListCat");
        }

        public PartialViewResult lstCatPartial()
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
            return PartialView("_lstCatPartial", presidents);
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

    }
}
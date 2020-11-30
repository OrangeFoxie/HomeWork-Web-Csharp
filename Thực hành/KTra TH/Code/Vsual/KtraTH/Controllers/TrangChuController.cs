using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KtraTH.Models;

namespace KtraTH.Controllers
{
    public class TrangChuController : Controller
    {
        dbQLKhachHangDataContext db = new dbQLKhachHangDataContext();
        // GET: TrangChu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DSKH()
        {
            return View(db.Khachhangs.ToList());
        }

        [HttpGet]
        public ActionResult ThemmoiKH(Khachhang khach)
        {
            ViewBag.MaKV = new SelectList(db.Khuvucs.ToList().OrderBy(n => n.TenKV), "MaKV", "TenKV");

            db.Khachhangs.InsertOnSubmit(khach);
            //db.SubmitChanges();

            //return RedirectToAction("khach");
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectronicStore.Areas.Administrator.Controllers.CustomAttributes;
using ElectronicStore.Models;
using PagedList;
using PagedList.Mvc;
using ElectronicStore.Areas.Administrator.Models;
namespace ElectronicStore.Areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        electronicDataContext db = new electronicDataContext();
        // GET: Adminstrator/Home
        public ActionResult Index()
        {
            return View();
        }
        //------------------------------------San Pham--------------------------------------
        [AdminAuthorize(Order = 2)]
        public ActionResult SanPham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.SPs.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }

        [AdminAuthorize(Order = 2)]
        public ActionResult ThemMoiSP()
        {
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSP(SP sanpham, HttpPostedFileBase fileUpLoad)
        {
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");

            if (fileUpLoad == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpLoad.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpLoad.SaveAs(path);
                    }
                    sanpham.HinhSP = fileName;
                    db.SPs.InsertOnSubmit(sanpham);
                    db.SubmitChanges();
                }
            }
            return RedirectToAction("SanPham", "Home");
        }
        [AdminAuthorize]
        public ActionResult Details(int? id)
        {
            SP sanpham = db.SPs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        [AdminAuthorize(Order = 2)]
        public ActionResult XoaSP(int? id)
        {
            SP sanpham = db.SPs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("XoaSP")]
        public ActionResult XacNhanXoa(int? id)
        {
            SP sanpham = db.SPs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SPs.DeleteOnSubmit(sanpham);
            db.SubmitChanges();
            return RedirectToAction("SanPham");
        }
        [AdminAuthorize(Order = 2)]
        public ActionResult SuaSP(int? id)
        {
            SP sp = db.SPs.SingleOrDefault(p => p.MaSP == id);
            ViewBag.MaSP = sp.MaSP;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH", sp.MATH);
            return View(sp);
        }
        [AdminAuthorize(Order = 2)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSP(SP sp, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    sp.HinhSP = fileName;
                }

                var obj = db.SPs.SingleOrDefault(p => p.MaSP == sp.MaSP);
                obj.TenSP = sp.TenSP;
                obj.GiaBan = sp.GiaBan;
                obj.MoTa = sp.MoTa;
                obj.NgayCapNhat = sp.NgayCapNhat;
                obj.HinhSP = sp.HinhSP;
                obj.SLTon = sp.SLTon;
                obj.MATH = sp.MATH;
                db.SubmitChanges();
                return RedirectToAction("SanPham");
            }
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
            return View(sp);
        }
        //-----------------------------------------------Nhan Vien--------------------------------------------
        [AdminAuthorize(Order = 4)]
        public ActionResult NhanVien(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.NhanViens.ToList().OrderBy(n => n.MaNV).ToPagedList(pageNumber, pageSize));
        }
        //[AdminAuthorize(Order = 4)]
        public ActionResult DSNV(int? id)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == id);
            return View(nhanvien);
        }
        [AdminAuthorize(Order = 1)]
        public ActionResult ThemMoiNV()
        {
            ViewBag.MaCV = new SelectList(db.NhanViens.ToList().OrderBy(n => n.HoTen), "MaCV", "HoTen");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiNV([Bind(Include = "MaNV, Username, Password, Email, HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, MaCV")] NhanVien nhanVien)
        {
            ViewBag.MaCV = new SelectList(db.NhanViens.ToList().OrderBy(n => n.HoTen), "MaCV", "HoTen");
            if (ModelState.IsValid)
            {
                db.NhanViens.InsertOnSubmit(nhanVien);
                db.SubmitChanges();
            }
            return RedirectToAction("NhanVien", "Home");
        }
        [AdminAuthorize(Order = 4)]
        public ActionResult ChiTietNV(int? id)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == id);

            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanvien);
        }
        [AdminAuthorize(Order = 4)]
        public ActionResult XoaNV(int? id)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == id);
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanvien);
        }
        [HttpPost, ActionName("XoaNV")]
        public ActionResult XacNhanXoaNV(int? id)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == id);
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhanViens.DeleteOnSubmit(nhanvien);
            db.SubmitChanges();
            return RedirectToAction("NhanVien");
        }
        [AdminAuthorize(Order = 4)]
        public ActionResult SuaNV(int? id)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == id);
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCV = new SelectList(db.NhanViens.ToList().OrderBy(n => n.MaCV), "MaCV", "HoTen");
            return View(nhanvien);
        }
        [AdminAuthorize(Order = 4)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaNV([Bind(Include = "MaNV, Username, Password, Email, HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, MaCV")] NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                var obj = db.NhanViens.SingleOrDefault(p => p.MaNV == nhanvien.MaNV);
                obj.Username = nhanvien.Username;
                obj.Password = nhanvien.Password;
                obj.Email = nhanvien.Email;
                obj.HoTen = nhanvien.HoTen;
                obj.NgaySinh = nhanvien.NgaySinh;
                obj.GioiTinh = nhanvien.GioiTinh;
                obj.DiaChi = nhanvien.DiaChi;
                obj.DienThoai = nhanvien.DienThoai;
                obj.MaCV = nhanvien.MaCV;
                db.SubmitChanges();
                return RedirectToAction("NhanVien");
            }
            ViewBag.MaCV = new SelectList(db.NhanViens.ToList().OrderBy(n => n.MaCV), "MaCV", "HoTen");
            return View(nhanvien);
        }
        //----------------------------------------------Thương Hiệu-----------------------------------------
        [AdminAuthorize(Order = 4)]
        public ActionResult ThuongHieu(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.THUONGHIEUs.ToList().OrderBy(n => n.MATH).ToPagedList(pageNumber, pageSize));
        }

        [AdminAuthorize(Order = 4)]
        public ActionResult ThemMoiTH()
        {
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiTH(THUONGHIEU thuonghieu, HttpPostedFileBase fileUpLoad)
        {
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
            if (ModelState.IsValid)
            {
                db.THUONGHIEUs.InsertOnSubmit(thuonghieu);
                db.SubmitChanges();
            }
            return RedirectToAction("ThuongHieu", "Home");
        }
        [AdminAuthorize(Order = 4)]
        public ActionResult XoaTH(int? id)
        {
            THUONGHIEU thuonghieu = db.THUONGHIEUs.SingleOrDefault(n => n.MATH == id);
            ViewBag.MATH = thuonghieu.MATH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thuonghieu);
        }
        [AdminAuthorize(Order = 4)]
        [HttpPost, ActionName("XoaTH")]
        public ActionResult XacNhanXoaTH(int? id)
        {
            THUONGHIEU thuonghieu = db.THUONGHIEUs.SingleOrDefault(n => n.MATH == id);
            ViewBag.MATH = thuonghieu.MATH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.THUONGHIEUs.DeleteOnSubmit(thuonghieu);
            db.SubmitChanges();
            return RedirectToAction("ThuongHieu");
        }
        [AdminAuthorize(Order = 4)]
        public ActionResult SuaTH(int? id)
        {
            THUONGHIEU thuonghieu = db.THUONGHIEUs.SingleOrDefault(n => n.MATH == id);
            ViewBag.MATH = thuonghieu.MATH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH", thuonghieu.MATH);
            return View(thuonghieu);
        }
        [AdminAuthorize(Order = 4)]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTH(THUONGHIEU thuonghieu)
        {
            if (ModelState.IsValid)
            {

                var obj = db.THUONGHIEUs.SingleOrDefault(p => p.MATH == thuonghieu.MATH);
                obj.TENTH = thuonghieu.TENTH;
                db.SubmitChanges();
                return RedirectToAction("ThuongHieu");
            }
            ViewBag.MATH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
            return View(thuonghieu);
        }
    }
}
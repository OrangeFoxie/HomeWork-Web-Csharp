using ElectronicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        electronicDataContext db = new electronicDataContext();
        [HttpGet]
        public ActionResult Dangky()

        {
            return View();
        }
        [HttpPost]

        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = string.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["loi1"] = "Không được bỏ trống họ tên ";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi2"] = "Không được trùng tên đăng nhập.";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "Không được bỏ trống mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["loi4"] = "Phải nhập lại mật khẩu trùng khớp";

            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["loi5"] = "Không được bỏ trống email";

            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["loi6"] = "Không được bỏ trống số điện thoại";
            }
            else
            {
                kh.HoTen = hoten;
                kh.TaiKhoan = tendn;
                kh.MatKhau = matkhau;               
                kh.Email = email;
                kh.DiaChiKH = diachi;
                kh.DienThoaiKH = dienthoai;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");

            }
            return Dangky();
        }
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {

            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Vui lòng nhập tên đăng nhập!";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Mật khẩu không được bỏ trống!";

            }
            else
            {

                KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(n => n.TaiKhoan.CompareTo(tendn) == 0 && n.MatKhau.CompareTo(matkhau) == 0);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công!";
                    Session["Taikhoan"] = kh;

                }
                else
                    ViewBag.Thongbao = " Tên đăng nhập hoặc mật khẩu không đúng! ";
            }
            return RedirectToAction("GioHang","GioHang");

        }
        public PartialViewResult ID()
        {
            if (Session["Taikhoan"] != null)
            {
                KHACHHANG kh = (KHACHHANG) Session["Taikhoan"];
                ViewBag.ThongBao = kh.TaiKhoan;
            }
            return PartialView();
        }
    }
}
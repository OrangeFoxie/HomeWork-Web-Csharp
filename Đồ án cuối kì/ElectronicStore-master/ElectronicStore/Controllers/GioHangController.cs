using ElectronicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Controllers
{
    public class GioHangController : Controller
    {
        electronicDataContext data = new electronicDataContext();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.iMaSP == iMaSP);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSP);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "ElectronicStore");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult CapnhatGiohang(int iMaSanPham, FormCollection f)
        {
            int sl = int.Parse(f["txtSoLuong"].ToString());
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSanPham);
            if (sanpham != null)
            {
                sanpham.iSoLuong = sl;
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int iMaSanPham)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSanPham);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSanPham);
                return RedirectToAction("GioHang");

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "ElectronicStore");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "ElectronicStore");
        }
        public ActionResult DatHang()
        {
            if(Session["Taikhoan"]==null || Session["Taikhoan"].ToString()=="")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("Index", "ElectronicStore");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang (FormCollection collection)
        {
            string ten="";
            int gia = 0;
            int sl = 0;
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MAKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            DateTime ngaygiao = DateTime.Parse(collection["Ngaygiao"].ToString());
            ddh.NgayGiao = ngaygiao;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            foreach(var item in gh)
            {
                CTHD cthd = new CTHD();
                cthd.MaDH = item.iMaSP;
                cthd.MaSP = ddh.MADH;
                cthd.SoLuong = item.iSoLuong;
                cthd.DonGia = (decimal)item.dDonGia;
                ten += item.sTenSP + "  ";
                gia += (int)(cthd.DonGia*item.iSoLuong);
                data.CTHDs.InsertOnSubmit(cthd);             
            }
            data.SubmitChanges();
            string url = "https://www.baokim.vn/payment/product/version11?business=phamvungochoi@gmail.com&id=&order_description=ABC" + "&product_name=" + ten + "&product_price=" + gia + "&product_quantity=" + sl + "&total_amount=" + gia + "&url_cancel=&url_detail=" + "&url_success=" + Url.Action("XacNhanDonHang", "GioHang");
            Session["GioHang"] = null;
            return Redirect(url);           
        }
        public ActionResult DatHangKhiNhan()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "ElectronicStore");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHangKhiNhan(FormCollection collection)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MAKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            DateTime ngaygiao = DateTime.Parse(collection["Ngaygiao"].ToString());
            ddh.NgayGiao = ngaygiao;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                CTHD cthd = new CTHD();
                cthd.MaDH = item.iMaSP;
                cthd.MaSP = ddh.MADH;
                cthd.SoLuong = item.iSoLuong;
                cthd.DonGia = (decimal)item.dDonGia;              
                data.CTHDs.InsertOnSubmit(cthd);
            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ElectronicStore.Models
{
    public class GioHang
    {
        electronicDataContext data = new electronicDataContext();
        public int iMaSP { set; get; }
        public string sTenSP { set; get; }
        public string sHinhSP { set; get; }
        public Double dDonGia { set; get; }
        public int iSoLuong { set; get; }
        public Double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public GioHang(int MaSP)
        {
            iMaSP = MaSP;
            SP sanpham = data.SPs.Single(n => n.MaSP == iMaSP);
            sTenSP = sanpham.TenSP;
            sHinhSP = sanpham.HinhSP;
            dDonGia = double.Parse(sanpham.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
    
    
}
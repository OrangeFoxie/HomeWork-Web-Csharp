using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectronicStore.Models;
using PagedList;
namespace ElectronicStore.Controllers
{
    public class ElectronicStoreController : Controller
    {
        electronicDataContext data = new electronicDataContext();
        private List<SP> LaySP(int count)
        {
            return data.SPs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        // GET: ElectronicStore
        public ActionResult Index()
        {
            var spmoi = LaySP(7);
            return View(spmoi);
        }
        public ActionResult ThuongHieu()
        {
            var thuonghieu = from th in data.THUONGHIEUs select th;
            return PartialView(thuonghieu);
        }
        public ActionResult SpTheoThuongHieu(int id)
        {
            var sanpham = from sp in data.SPs where sp.MATH == id select sp ;
            return View(sanpham);
        }
        public ActionResult Details (int id)
        {
            var sanpham = from sp in data.SPs where sp.MaSP == id select sp;
            return View(sanpham.Single());
        }
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection f, int? page)
        {
            string chuoi = f["timkiem"].ToString();           
            int pageSize = 24;
            int pageNum = (page ?? 1);
            var lst = from s in data.SPs where s.TenSP.Contains(chuoi.Trim()) select s;
            return View(lst.ToPagedList(pageNum, pageSize));
        }
    }
}
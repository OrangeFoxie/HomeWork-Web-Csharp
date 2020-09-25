using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab02.Models;

namespace Lab02.Controllers
{
    public class TheloaitinController : Controller
    {
        // GET: Theloaitin
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Index()
        {
            var All_Loaitin = from tt in data.Theloaitins select tt;
            return View(All_Loaitin);
        }

        // Hàm Details truyền dữ liệu sang Details.aspx
        public ActionResult Details(int id)
        {
            var Details_tin = data.Theloaitins.Where(m => m.IDLoai == id).First();
            return View(Details_tin);
        }
        //Hàm create tạo khung cho ng dùng nhập liệu
        public ActionResult Create()
        {
            return View();
        }
        // hàm create(Post) xử lí data dc truyền từ trang Create.aspx và trả về kết quả
        [HttpPost]
        public ActionResult Create(FormCollection collection, Theloaitin ltin)
        {
            var CB_Loaitin = collection["Tentheloai"];
            if (string.IsNullOrEmpty(CB_Loaitin))
            {
                ViewData["Loi"] = "Thể loại tin ko được bỏ trống";
            }
            else
            {
                ltin.Tentheloai = CB_Loaitin;
                data.Theloaitins.InsertOnSubmit(ltin);
                //Thực hiện tạo mới
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        //GET:Hàm edit(get)
        public ActionResult Edit(int id)
        {
            var EB_tin = data.Theloaitins.First(m => m.IDLoai == id);
            return View(EB_tin);
        }
        //POST:
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var Ltin = data.Theloaitins.First(m => m.IDLoai == id);
            var E_Loaitin = collection["Tentheloai"];
            Ltin.IDLoai = id;

            if (string.IsNullOrEmpty(E_Loaitin))
            {
                ViewData["Loi"] = "The loai tin không duoc empty";
            }
            else
            {
                Ltin.Tentheloai = E_Loaitin;
                UpdateModel(Ltin);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        // GET:
        public ActionResult Delete(int id)
        {
            var D_tin = data.Theloaitins.First(m => m.IDLoai == id);
            return View(D_tin);
        }
        //POST:
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_tin = data.Theloaitins.Where(m => m.IDLoai == id).First();
            //Xoá
            data.Theloaitins.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}// xong câu 6
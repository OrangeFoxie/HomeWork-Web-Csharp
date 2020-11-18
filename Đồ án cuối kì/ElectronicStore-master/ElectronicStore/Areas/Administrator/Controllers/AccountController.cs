using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectronicStore.Areas.Administrator.Models;
using ElectronicStore.Models;
namespace ElectronicStore.Areas.Administrator.Controllers
{
    public class AccountController : Controller
    {
        private electronicDataContext db = new electronicDataContext();
        // GET: Administrator/Login
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost, ActionName("SignIn")]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(NhanVienViewModel account, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var isValid = db.NhanViens.SingleOrDefault(p => p.Username == account.Username);
                if (isValid == null)
                {
                    ViewBag.Message = "Tài khoản không tồn tại.";
                    return View(account);
                }
                else if (isValid.Password != account.Password)
                {
                    ViewBag.Message = "Mật khẩu bị sai.";
                    return View(account);
                }
                HttpCookie userCookie = new HttpCookie("AdminAccount", account.Username);
                userCookie["ID"] = isValid.MaNV.ToString();
                userCookie["Username"] = account.Username;
                userCookie["Password"] = account.Password;
                userCookie["MaCV"] = isValid.MaCV.ToString();
                userCookie.Expires = DateTime.Now.AddDays(1);
                Response.SetCookie(userCookie);
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(account);
        }
        public ActionResult SignOut()
        {
            HttpCookie userCookie = new HttpCookie("AdminAccount");
            userCookie.Expires = DateTime.Now.AddDays(-1);
            Response.SetCookie(userCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}
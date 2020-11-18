using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectronicStore.Models;
namespace ElectronicStore.Areas.Administrator.Controllers.CustomAttributes
{
    public class AdminAuthorize : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isFailed = false;
            string message = "";
            HttpCookie account = filterContext.HttpContext.Request.Cookies["AdminAccount"];
            if (account == null)
            {
                message = "Đăng nhập để tiếp tục!";
                isFailed = true;
            }
            else
            {
                using (var dataContext = new electronicDataContext())
                {
                    var id = int.Parse(account["ID"]);
                    var model = dataContext.NhanViens.Single(a => a.MaNV == id);
                    if (model.MaCV == null)
                    {
                        message = "Tài khoản của bạn đã bị khóa";
                        isFailed = true;
                    }
                    else if (model.Password != account["Password"])
                    {
                        message = "Mật khẩu của bạn đã thay đổi, vui lòng đăng nhập lại!";
                        isFailed = true;
                    }
                }

            }
            if (isFailed)
            {
                HttpCookie userCookie = new HttpCookie("AdminAccount");
                userCookie.Expires = DateTime.Now.AddDays(-1);
                filterContext.HttpContext.Response.SetCookie(userCookie);
                filterContext.Controller.TempData["Message"] = message;
                filterContext.Result = new RedirectResult("/Administrator/Account/SignIn");
            }
            else if (Order != -1 && !Check(Order, int.Parse(account["MaCV"] ?? "0")))
            {
                filterContext.Controller.TempData["Message"] = "Bạn bị giới hạn quyền truy cập!";
                filterContext.Result = new RedirectResult("/Administrator/Account/SignIn");
            }
        }
        private static bool Check(int Order, int MaCV)
        {
            switch (Order)
            {
                //Toàn quyền
                //CRUD toàn bộ
                case 1:
                    switch (MaCV)
                    {
                        case 1:
                            return true;
                        default:
                            return false;
                    }
                // Nhập kho
                // CRUD Hàng hóa, Loại, Hãng, Menu
                case 2:
                    switch (MaCV)
                    {
                        case 1:
                        case 2:
                            return true;
                        default:
                            return false;
                    }
                // Kiểm đơn
                // Đánh dấu tình trạng đơn hàng
                case 3:
                    switch (MaCV)
                    {
                        case 1:
                        case 3:
                            return true;
                        default:
                            return false;
                    }
                // Quản lý
                // Xem báo cáo thống kê
                case 4:
                    switch (MaCV)
                    {
                        case 1: 
                        case 4:
                            return true;
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LAB_01.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult index()
        {
            return View();
        }

        // GET: HelloWorld
        //public String Index()
        //{
        //    return "This is my <b> default <b> action...";
        //}

        //// Get:/HelloWorld/Wellcome
        //// Get:/HelloWorld/Wellcome?name=Scott&numtimes=4
        ////public String Wellcome(String name, int numtimes)
        ////{
        ////    //return "this is the wellcome to action method ...";
        ////    return HttpUtility.HtmlEncode("Hello " + name + " numtimes " + numtimes);   //link nhanh: https://localhost:44312/HelloWorld/Wellcome?name=Scott&numtimes=4
        ////}

        //// GET: /HelloWorld/Wellcome
        //public String Wellcome(String name, int id = 1)
        //{
        //    return HttpUtility.HtmlEncode("Hello " + name + ", ID: " + id);
        //}
    }
}
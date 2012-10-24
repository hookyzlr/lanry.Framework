using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lanry.Model;
using Lanry.Facade;
using Lanry.Utility;

namespace Lanry.Framework
{
    public class HomeController : Controller
    {
        [NonAction]
        public ActionResult Index()
        {

            ViewBag.Message = "欢迎使用 ASP.NET MVC!";
            return View();
        }

        public ActionResult Index(int id)
        {
            TB1 entity = new TB1();
            entity.Name = "2";
            entity.ParentID = 2;
            entity.ID = 7;

            HM_CatergoryFacade.Update(entity);
            DataList<TB1> list = HM_CatergoryFacade.GetTestData();

            ViewBag.Message = "欢迎使用 ASP.NET MVC!";
            return View(list);
        }

        public ActionResult About(string id,string name)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}

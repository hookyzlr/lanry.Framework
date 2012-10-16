using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lanry.Utility;

namespace Lanry.Framework.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /List/
        public ActionResult GetList(int id = 0,string name = "")
        {
            return View();
        }

        public ActionResult NameList([Lanry.Utility.Attributes.Parameter(0)]string id, [Lanry.Utility.Attributes.Parameter("100", @"^\d{4}-\d{2}-\d{2}$")]string name)
        {
            return View();
        }
    }
}

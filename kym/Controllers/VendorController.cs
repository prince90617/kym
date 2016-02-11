using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kym.Utility.Mvc;

namespace kym.Controllers
{
    [ApiAuthorize]
    public class VendorController : Controller
    {
        //
        // GET: /Vendor/

        public ActionResult Index()
        {
            var apiToken = ((ApiIdentity)User.Identity).AccessToken;
            return View();
        }
        public ActionResult Catalogue()
        {
            return View();
        }
        public ActionResult Sale()
        {
            return View();
        }
        public ActionResult Purchase()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}

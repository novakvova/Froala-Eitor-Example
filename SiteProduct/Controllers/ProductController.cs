using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteProduct.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UploadImageDecription(HttpPostedFileBase file)
        {

            return Json(new { link = "/Images/Product/Description/default.jpg" });
        }
    }
}
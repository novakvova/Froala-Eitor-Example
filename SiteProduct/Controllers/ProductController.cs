
using SiteProduct.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            string link = string.Empty;
            string filename= Guid.NewGuid().ToString() + ".jpg";
            string image = Server.MapPath(Constants.ProductDescriptionPath) +
                filename;
            try
            {
                using (Bitmap bmp = new Bitmap(file.InputStream))
                {
                    var saveImage = ImageWorker.CreateImage(bmp, 450, 450);
                    if(saveImage!=null)
                    {
                        saveImage.Save(image, ImageFormat.Jpeg);
                        link = Url.Content(Constants.ProductDescriptionPath) + 
                            filename;
                    }
                }
                string path = Url.Content(Constants.ProductDescriptionPath);
            }
            catch (Exception)
            {
                if(System.IO.File.Exists(image))
                {
                    System.IO.File.Delete(image);
                }
            }
            return Json(new { link });
        }

        [HttpPost]
        public JsonResult DeleteImageDecription(string src)
        {
            string link = string.Empty;
            string filename = Path.GetFileName(src);
            string image = Server.MapPath(Constants.ProductDescriptionPath) +
                filename;

            if (System.IO.File.Exists(image))
            {
                System.IO.File.Delete(image);
            }
            return Json(new { link });
        }
        
    }
}
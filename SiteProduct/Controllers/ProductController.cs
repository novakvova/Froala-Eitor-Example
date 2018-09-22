
using SiteProduct.Core;
using SiteProduct.DAL.Entities;
using SiteProduct.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SiteProduct.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ProductAddViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {

                        scope.Complete();
                    }
                }
                catch
                {

                }
            }
            return View(model);
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
                        var pdImage = new ProductDescriptionImage
                        {
                            Name = filename
                        };
                        _context.ProductDescriptionImages.Add(pdImage);
                        _context.SaveChanges();

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
            return Json(new { link, filename });
        }

        [HttpPost]
        public JsonResult DeleteImageDecription(string src)
        {
            string link = string.Empty;
            string filename = Path.GetFileName(src);
            string image = Server.MapPath(Constants.ProductDescriptionPath) +
                filename;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var pdImage = _context
                        .ProductDescriptionImages
                        .SingleOrDefault(p => p.Name == filename);
                    if(pdImage!=null)
                    {
                        _context.ProductDescriptionImages.Remove(pdImage);
                        _context.SaveChanges();
                    }
                    //throw new Exception("Галяк");
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }
                    scope.Complete();
                }
            }
            catch
            {
                filename = string.Empty;
            }
            
            return Json(new { filename });
        }
        
    }
}
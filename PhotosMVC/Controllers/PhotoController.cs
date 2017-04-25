using PhotosMVC.Filters;
using PhotosMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotosMVC.Controllers
{
    [ValueReporter]
    public class PhotoController : Controller
    {
        // declaracion del contexto de db
        // -----------------------------------

        private Models.PhotoSharingDB context = new Models.PhotoSharingDB();

        // metodos action
        // -----------------------------------

        //
        // GET: /Photo/
        public ActionResult Index()
        {
            // en vez de retornar la vista por defecto, usa la sobrecarga con
            // dos parametros: el primero indica la "vista" correspondiente,
            // y el segundo es el "modelo" que se le pasa a esa vista.
            return View("Index", context.Photos.ToList());
        }

        //
        // GET: /Photo/1
        public ActionResult Display(int id)
        {
            Models.Photo photo = context.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Models.Photo newPhoto = new Models.Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }

        [HttpPost]
        public ActionResult Create(Models.Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View("Create", photo);
            }
            else
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);


                }

                context.Photos.Add(photo);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        
        }

        public ActionResult Delete(int id)
        {
            Models.Photo photo = context.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Delete", photo);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Photo photo = context.Photos.Find(id);
            context.Photos.Remove(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            Models.Photo photo = context.Photos.Find(id);
            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}
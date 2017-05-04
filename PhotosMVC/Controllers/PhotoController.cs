using PhotosMVC.Filters;
using PhotosMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotosMVC.Controllers
{
    [ValueReporter]
    [HandleError(View = "Error")]
    public class PhotoController : Controller
    {
        // declaracion del Repositorio
        // -----------------------------------

        private IPhotoSharingContext context;

        public PhotoController()
        {
            context = new PhotoSharingContext();
        }

        public PhotoController(IPhotoSharingContext context)
        {
            this.context = context;
        }

        // metodos action
        // -----------------------------------

        //
        // GET: /Photo/
        public ActionResult Index()
        {
            // en vez de retornar la vista por defecto, usa la sobrecarga con
            // dos parametros: el primero indica la "vista" correspondiente,
            // y el segundo es el "modelo" que se le pasa a esa vista.
            return View("Index");
        }

        //
        // GET: /Photo/1
        public ActionResult Display(int id)
        {
            Models.Photo photo = context.FindPhotoById(id);
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

                context.Add<Photo>(photo);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        
        }

        public ActionResult Delete(int id)
        {
            Models.Photo photo = context.FindPhotoById(id);
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
            Models.Photo photo = context.FindPhotoById(id);
            context.Delete<Photo>(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            Models.Photo photo = context.FindPhotoById(id);
            if (photo != null && photo.PhotoFile != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Edit(int id)
        {
            Models.Photo photo = context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Edit", photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }

                Photo old = context.FindPhotoById(photo.PhotoID);
                old.Comments = photo.Comments;
                old.CreatedDate = photo.CreatedDate;
                old.Description = photo.Description;
                old.ImageMimeType = photo.ImageMimeType;
                old.PhotoFile = photo.PhotoFile;
                old.Title = photo.Title;
                old.UserName = photo.UserName;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", photo);
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos;
            if (number == 0)
            {
                photos = context.Photos.ToList();
            }
            else
            {
                photos = context.Photos.OrderByDescending(x => x.CreatedDate)
                                        .Take(number)
                                        .ToList();
            }
            return PartialView("_PhotoGallery", photos);

        }

        public ActionResult SlideShow()
        {
            throw new NotImplementedException("The SlideShow action is not yet ready");
        }

        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = context.FindPhotoByTitle(title);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

    }
}
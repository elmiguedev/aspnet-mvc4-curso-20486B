using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotosMVC.Models
{
    public class PhotoSharingContext : DbContext, IPhotoSharingContext
    {
        public PhotoSharingContext()
        : base("PhotoSharingConnectionString") {
            
            //Database.SetInitializer(new PhotoSharingInitializer());
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        IQueryable<Photo> IPhotoSharingContext.Photos {
            get { return Photos; }
        }

        IQueryable<Comment> IPhotoSharingContext.Comments {
            get { return Comments; }
        }

        public T Add<T>(T entity) where T : class
        {
            return Set<T>().Add(entity);
        }

        public T Delete<T>(T entity) where T : class
        {
            return Set<T>().Remove(entity);
        }

        public Comment FindCommentById(int ID)
        {
            return Set<Comment>().Find(ID);
        }

        public Photo FindPhotoById(int ID)
        {
            return Set<Photo>().Find(ID);
        }

        public Photo FindPhotoByTitle(string title)
        {
            return Set<Photo>().Where(x => x.Title == title).FirstOrDefault();
        }
    }
}
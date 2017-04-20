using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotosMVC.Models
{
    public class PhotoSharingDB : DbContext
    {
        public PhotoSharingDB()
            : base("PhotoSharingConnectionString")
        {

        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotosMVC.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        
        public string Title { get; set; }
        
        public byte[] PhotoFile { get; set; }
        
        public string ImageMimeType { get; set; }
        
        public string Descripton { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        
        public string UserName { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
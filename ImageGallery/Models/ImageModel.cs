using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RozichMurals.Web.Models
{
    public class Image
    {
        public int id { get; set; } 
        public string Path { get; set; }
        public string Description { get; set; }
        public int AlbumId { get; set; }
        public int? OrderNumber { get; set; }

        public virtual Album Album { get; set; }
    }
    public class Album
    {
        public Album()
        {
            Images = new List<Image>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ThumbImage { get; set; }
        public int CategoryId { get; set; }
        public int? OrderNumber { get; set; }

        public virtual Category Category { get; set; } 
        public virtual List<Image> Images { get; set; }

        public string Description { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual List<Album> Albums { get; set; } 
    }
   
}
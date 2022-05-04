using System;
namespace siemens_pedigree_api.Models
{
	public class TreeImage
	{
		public int? Id { get; set; }
	    public string Image { get; set; } //string to store the image data in base 64     
		public DateTime? Date { get; set; }
		public String IdTree { get; set; }

        public TreeImage()
        {
            this.Image = "";
            this.IdTree = "";
        }

        // public TreeImage()
        // {
        // 	this.ImagePath = null;
        // 	this.IdTree = "";
        // }

        // public TreeImage(String idTree, string content)
        // {
        // 	this.ImagePath = content;
        // 	this.IdTree = idTree;
        // }

        // public TreeImage(int id, String idTree, string content)
        // {
        // 	this.ImagePath = content;
        // 	this.Id = id;
        // 	this.IdTree = idTree;
        // }

        public TreeImage(int id, DateTime date, String idTree, string image)
        {
            this.Image = image;
            this.IdTree = idTree;
            this.Date = date;
            this.Id = id;
        }

        // public TreeImage(DateTime date, String idTree, string content)
        // {
        // 	this.ImagePath = content;
        // 	this.IdTree = idTree;
        // 	this.Date = date;
        // }

    }
}


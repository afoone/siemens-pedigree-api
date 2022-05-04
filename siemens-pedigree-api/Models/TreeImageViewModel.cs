using System;
namespace siemens_pedigree_api.Models
{
	public class TreeImageViewModel
	{
		public int? Id { get; set; }
		public IFormFile image { get; set; }

		
		public TreeImageViewModel(IFormFile image)
        {
			this.image = image;
        }

		public TreeImageViewModel(int id, IFormFile image)
        {
			this.image = image;
			this.Id = id;
        }

    }
}


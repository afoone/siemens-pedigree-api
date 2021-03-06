using System;
namespace siemens_pedigree_api.Models
{
	public class JSONStruct
	{
		public int? Id { get; set; }
		public String Content { get; set; }
		public DateTime? Date { get; set; }
		public String IdTree { get; set; }
		
		public JSONStruct()
		{
			this.Content = "";
			this.IdTree = "";
		}

		public JSONStruct(String idTree, String content)
        {
			this.Content = content;
			this.IdTree = idTree;
        }

		public JSONStruct(int id, String idTree, String content)
        {
			this.Content = content;
			this.Id = id;
			this.IdTree = idTree;
        }

		public JSONStruct(int id, DateTime date, String idTree, String content)
        {
			this.Content = content;
			this.IdTree = idTree;
			this.Date = date;
			this.Id = id;
        }

		public JSONStruct(DateTime date, String idTree, String content)
		{
			this.Content = content;
			this.IdTree = idTree;
			this.Date = date;
		}

        public static implicit operator JSONStruct?(JSONStructContext? v)
        {
            throw new NotImplementedException();
        }
    }
}


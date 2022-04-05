using System;
namespace siemens_pedigree_api.Models
{
	public class JSONEntry
	{
		public int? Id { get; set; }
		public String Content { get; set; }
		public DateTime? Date { get; set; }
		public String IdTree { get; set; }
		
		public JSONEntry()
		{
			this.Content = "";
			this.IdTree = "";
		}

		public JSONEntry(String idTree, String content)
        {
			this.Content = content;
			this.IdTree = idTree;
        }

		public JSONEntry(int id, String idTree, String content)
        {
			this.Content = content;
			this.Id = id;
			this.IdTree = idTree;
        }

		public JSONEntry(int id, DateTime date, String idTree, String content)
        {
			this.Content = content;
			this.IdTree = idTree;
			this.Date = date;
			this.Id = id;
        }

		public JSONEntry(DateTime date, String idTree, String content)
		{
			this.Content = content;
			this.IdTree = idTree;
			this.Date = date;
		}

        public static implicit operator JSONEntry?(JSONEntryContext? v)
        {
            throw new NotImplementedException();
        }
    }
}


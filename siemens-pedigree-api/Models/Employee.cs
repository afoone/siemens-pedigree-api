using System;
namespace siemens_pedigree_api.Models
{
	public class Employee
	{
		public long Id { get; set; }
		public string? name { get; set; }
		public bool isComplete { get; set; }


		public Employee()
		{
			
		}

		public Employee(long id, string name, bool isComplete)
        {
			this.Id = id;
			this.name = name;
			this.isComplete = isComplete;
        }
	}
}


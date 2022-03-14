using System;
using Microsoft.EntityFrameworkCore;

namespace siemens_pedigree_api.Models
{
    public class EmployeeContext : DbContext
	{
		public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
		{
		}

		public DbSet<Employee> employees { get; set; } = null!;
	}
}


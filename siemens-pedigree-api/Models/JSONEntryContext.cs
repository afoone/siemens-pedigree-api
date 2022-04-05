using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace siemens_pedigree_api.Models
{
    public class JSONEntryContext : DbContext
	{
		public JSONEntryContext(DbContextOptions<JSONEntryContext> options): base(options)
		{

		}


		public DbSet<JSONEntryContext> JSONEntries { get; set; } = null!;
	}
}


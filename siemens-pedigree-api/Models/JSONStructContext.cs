using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace siemens_pedigree_api.Models
{
    public class JSONStructContext : DbContext
	{
		public JSONStructContext(DbContextOptions<JSONStructContext> options): base(options)
		{

		}


		public DbSet<JSONStructContext> JSONEntries { get; set; } = null!;
	}
}


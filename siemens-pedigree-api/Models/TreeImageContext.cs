using System;
using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace siemens_pedigree_api.Models
{
	public class TreeImageContext:DbContext
	{
		public TreeImageContext(DbContextOptions<TreeImageContext> options) : base(options)
    {
    }

    public DbSet<TreeImageViewModel> Images { get; set; }

    }
}


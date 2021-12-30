using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data
{
    public class TenisKubContext : DbContext
    {
        public TenisKubContext (DbContextOptions<TenisKubContext> options)
            : base(options)
        {
        }

        public DbSet<web.Models.Igralec> Igralec { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Models
{
    public class PerpustakaanDBContext : DbContext
    {
        public PerpustakaanDBContext(DbContextOptions<PerpustakaanDBContext> options) : base(options)
        {

        }

        public DbSet<Anggota> Anggotas { get; set; }
    }
}

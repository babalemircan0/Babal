using Babal.Models;
using BabalSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Babal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<DiaryEntry> DiaryEntries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Agenda> Agendas { get; set; }

        // Chat tablosunu buraya ekledik
        
    }
}
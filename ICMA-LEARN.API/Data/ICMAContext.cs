using ICMA_LEARN.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ICMA_LEARN.API.Data
{
    public class ICMAContext : DbContext
    {
        public ICMAContext(DbContextOptions<ICMAContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CourseContent> CourseContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Additional configurations if needed
        }
    }

}

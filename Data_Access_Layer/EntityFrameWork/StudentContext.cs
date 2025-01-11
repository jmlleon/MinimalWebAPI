namespace Data_Access_Layer.EntityFrameWork
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using Domain_Layer.Models;

    public class StudentContext : DbContext
    {
        /* protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseInMemoryDatabase(databaseName: "IDGSampleDb");
         }*/


        public StudentContext(DbContextOptions<StudentContext> options)
        : base(options) { }

        public DbSet<Student> Students => base.Set<Student>();
    }
}

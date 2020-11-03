using Microsoft.EntityFrameworkCore;

using Students.Infrastructure.Entities;


namespace Students.Infrastructure
{
    public class CoreContext : DbContext
    {
        public DbSet<Absenteeism> Absenteeism { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Student> Students { get; set; }


        public CoreContext(ICoreContextSettings settings) :
            base(new DbContextOptionsBuilder<CoreContext>().UseSqlServer(settings.ConnectionString).Options)
        { }
    }
}
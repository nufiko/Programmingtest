using Microsoft.EntityFrameworkCore;
using ProgrammingTest.DAL.Model;

namespace ProgrammingTest.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<ProgrammingTask> ProgrammingTasks { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Submission> Submissions { get; set; }
    }
}

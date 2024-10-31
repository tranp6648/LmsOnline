using Microsoft.EntityFrameworkCore;

namespace LMSOnline.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public virtual DbSet<Account>Accounts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace EMS.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<Registration> Register {  get; set; }

        public virtual DbSet<EmpData> Emp {  get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace GraduationProject
{
	public class DatabaseContext:DbContext
	{
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
            
        }
    }
}

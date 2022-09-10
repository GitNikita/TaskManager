using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Contexts
{
    public class UserTaskManagerContext : DbContext
    {
        public DbSet<ExecutionStatusTask> ExecutionStatuses { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        public UserTaskManagerContext(DbContextOptions<UserTaskManagerContext> options) : base(options)
        {
        }
    }
}

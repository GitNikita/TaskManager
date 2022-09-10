using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    [Table("tExecutionStatusesTask")]
    public class ExecutionStatusTask
    {
        public int Id { get; private set; }
        public string? ExecutionStatusName { get; set; }
        public List<UserTask>? UserTasks { get; private set; }
    }
}

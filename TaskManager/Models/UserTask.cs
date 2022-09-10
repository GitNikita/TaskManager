using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    [Table("tUserTasks")]
    public class UserTask
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int ExecutionStatusTaskId { get; set; }
        public List<UserFile>? UserFiles { get; private set; }
    }
}

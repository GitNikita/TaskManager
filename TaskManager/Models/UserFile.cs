using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    [Table("tUserFiles")]
    public class UserFile
    {
        public Guid Id { get; private set; }
        public string FileName { get; set; }
        public byte[]? FileData { get; set; }
        public int UserTaskId { get; set; }
    }
}

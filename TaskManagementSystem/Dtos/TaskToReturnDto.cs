using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.api.Dtos
{
    public class TaskToReturnDto 
    {

        public string User_Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Status { get; set; } 
       // public string? AssignUserId { get; set; }
    }
}

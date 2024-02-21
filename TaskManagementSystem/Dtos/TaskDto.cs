namespace TaskManagementSystem.api.Dtos
{
    public class TaskDto
    {
        public string User_Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime DeadLine { get; set; }
        public int CategoryId { get; set; }     
    }
}

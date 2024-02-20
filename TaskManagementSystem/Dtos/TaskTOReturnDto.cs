using System;

namespace TaskManagementSystem.api.Dtos
{
    public class TaskTOReturnDto
    {
        public string User_Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public DateTime DeadLine { get; set; }
        public int CategoryId { get; set; }
        public int CommentId { get; set; }
        public string Status { get; set; }
    }
}

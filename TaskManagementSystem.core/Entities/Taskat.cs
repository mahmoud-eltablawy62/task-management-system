using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.core.Entities
{
    public class Taskat : BaseClass   
    {

        public string User_Email { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; } 
        public DateTime DeadLine { get; set; }
        public DateTimeOffset PublishDate {get; set;} = DateTimeOffset.UtcNow;
        public int CategoryId { get; set; }
        public Category Category { get; set; }       
        public int CommentId { get; set; }    
        public Comment Comment { get; set; }       
        public Status Status { get; set; } = Status.InProgress;

        
    }
}

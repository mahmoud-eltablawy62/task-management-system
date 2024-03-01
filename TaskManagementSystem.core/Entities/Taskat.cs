using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Entities.Identity;

namespace TaskManagementSystem.core.Entities
{
    public class Taskat : BaseClass   
    {
        public Taskat(){}


        public Taskat(string user_email, string title , string description
               ,DateTime deadLine , int categoryid ) 
        {
            User_Email = user_email;
            Title = title;
            Description = description;
            DeadLine = deadLine;
            CategoryId = categoryid;
        }

      

        public string User_Email { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; } 
        public DateTime DeadLine { get; set; }
        public DateTimeOffset PublishDate {get; set;} = DateTimeOffset.UtcNow;
        public int CategoryId { get; set; }
        public Category Category { get; set; }                  
        public Status Status { get; set; } = Status.InProgress;   
        public string ? AssignUserId { get; set; }  
      
    }
}

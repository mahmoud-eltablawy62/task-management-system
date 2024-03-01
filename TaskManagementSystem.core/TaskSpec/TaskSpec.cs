using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Spacifications;

namespace TaskManagementSystem.Core.TaskSpec
{
    public class TaskSpec : Spacification<Taskat>
    {

        public TaskSpec(string AssignUserId) : base(
           O => O.AssignUserId == AssignUserId)
        {
            Includes.Add(O => O.Category);         
            
        }

        public TaskSpec(int id, string User_Email) : base(
            O => O.User_Email == User_Email && O.Id == id)
        {
            Includes.Add(O => O.Category);
        }

    }
}

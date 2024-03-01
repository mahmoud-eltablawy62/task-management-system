using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Spacifications;

namespace TaskManagementSystem.Core.Services.Contract
{
    public interface ITaskService
    {
        Task<Taskat?> CreateTaskAsync(int Cat_Id ,string user_email,string title,string des,DateTime deadline);
        Task<IReadOnlyList<Taskat>> GetAllTasksAsync();
        Task<Taskat?> GetOrderById(int Id);
        void Delete(Taskat task);
        Taskat Update(Taskat taskat);

        void GetTaskByDeadLineAndDeleteAsync(DateTime deadline); 
        
        Task<IReadOnlyList<Taskat>> OrderAsync(string AssignUserId);
        ////////////////////////////// spec ///////////////////
        Task<IReadOnlyList<Taskat>> GetAllTasksAsync(TaskParams Param);
        Task<int> GetCount(TaskParams Param);


    }
}

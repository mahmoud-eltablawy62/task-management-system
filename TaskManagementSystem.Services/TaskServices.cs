using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Talabat.Repository.Data;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Services.Contract;
using TaskManagementSystem.Core.Spacifications;
using TaskManagementSystem.Core.Spacifications.TaskSpacificarion;
using TaskManagementSystem.Core.TaskSpec;
using TaskManagementSystem.Repository;

namespace TaskManagementSystem.Services
{
    public class TaskServices : ITaskService
    {
        private readonly IUnitOfWork _Unit;

        private readonly TasksContext _TasksContext;    
        public TaskServices(IUnitOfWork Unit , TasksContext TasksContext)
        {
            _Unit = Unit;
            _TasksContext = TasksContext;
        }

        public async Task<IReadOnlyList<Taskat>> GetAllTasksAsync() =>
            await _Unit.Repo<Taskat>().GetAllAsync();
       

        public async Task<Taskat?> GetOrderById(int Id) => 
            await _Unit.Repo<Taskat>().GetAsync(Id);
        

       public async Task<Taskat?> CreateTaskAsync(int Cat_Id, string user_email, string title, string des, DateTime deadline)
        {
           var task = new Taskat(user_email , title , des , deadline , Cat_Id);
            await _Unit.Repo<Taskat>().Add(task);
            await _Unit.CompleteAsync();
            return task;
        }

        public void Delete(Taskat task)
        {
            _Unit.Repo<Taskat>().Delete(task);
            _Unit.CompleteAsync();
        }
        public Taskat Update(Taskat taskat)
        {
            _Unit.Repo<Taskat>().Update(taskat);
            _Unit.CompleteAsync();
            return taskat;
        }

        public async Task<IReadOnlyList<Taskat>> GetAllTasksAsync(TaskParams Param)
        {
            var spec = new TaskWithCategory(Param);
            var tasks = await _Unit.Repo<Taskat>().GetAllWithSpesAsync(spec);
            return tasks;
        }

        public Task<int> GetCount(TaskParams Param)
        {         
                var countspec = new TaskAfterFilteration(Param);
                var Count = _Unit.Repo<Taskat>().GetCountAsync(countspec);
                return Count;        
        }

        public async Task<IReadOnlyList<Taskat>> OrderAsync(string User_AssignID)
        {
            var task_repo = _Unit.Repo<Taskat>();
            var spec = new TaskSpec(User_AssignID);
            var Tasks = await task_repo.GetAllWithSpesAsync(spec);
            return Tasks;
        }

        public  void GetTaskByDeadLineAndDeleteAsync(DateTime deadline)
        {
            var tasks = _TasksContext.Tasks.Where(t => t.DeadLine == deadline).ToList();
            foreach (var task in tasks)
            {
                _TasksContext.Tasks.Remove(task);
            }
           _Unit.CompleteAsync();
        }
    }
}

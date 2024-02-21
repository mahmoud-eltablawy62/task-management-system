using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Services.Contract;

namespace TaskManagementSystem.Services
{
    public class TaskServices : ITaskService
    {
        private readonly IUnitOfWork _Unit;
        public TaskServices(IUnitOfWork Unit)
        {
            _Unit = Unit;
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

        public async Task<Category?> AddCategory(string name, string description)
        {
            var category = new Category(name , description);
            await _Unit.Repo<Category>().Add(category); 
            await _Unit.CompleteAsync();    
            return category;    
        }
    }
}

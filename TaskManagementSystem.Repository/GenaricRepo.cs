using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Repository.Data;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Repository.Repo.Contract;

namespace TaskManagementSystem.Repository
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseClass
    {
        private readonly TasksContext _TasksContext;
        public GenaricRepo(TasksContext Context)
        {
            _TasksContext = Context;    
        }
        public async Task Add(T entity) => 
            await _TasksContext.AddAsync(entity);
        public void Delete(T entity) =>
             _TasksContext.Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await _TasksContext.Set<T>().ToListAsync();

        public async Task<T?> GetAsync(int id) =>
            await _TasksContext.Set<T>().FindAsync(id);

        public void Update(T entity) =>
            _TasksContext.Update(entity);
         
    }
        
}


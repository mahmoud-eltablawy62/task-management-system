using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Repository.Repo.Contract
{
    public interface IGenaricRepo <T> where T : BaseClass
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    } 
   
}

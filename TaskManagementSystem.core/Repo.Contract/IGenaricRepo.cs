using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Spacifications;

namespace TaskManagementSystem.Repository.Repo.Contract
{
    public interface IGenaricRepo <T> where T : BaseClass
    {
        Task<T?> GetAsync(int id);   
        Task<IReadOnlyList<T>> GetAllAsync();
        Task Add(T entity);

        
        void Update(T entity);
        void Delete(T entity);
        ///////////////////////////// spec ////////////////////
        Task<IReadOnlyList<T>> GetAllWithSpesAsync(ISpacification<T> Spec);
        Task<T?> GetWithSpec(ISpacification<T> Spec);
        Task<int> GetCountAsync(ISpacification<T> Spec);
    } 
   
}

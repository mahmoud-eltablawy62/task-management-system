using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.Core.Services.Contract
{
    public interface ICategoryService
    {
        void DeleteCategory(Category category);
        Task<Category?> AddCategory(string name, string description);

        Task<Category?> GetOrderById(int Id);

        Category UpdateCat(Category category);  
    }
}

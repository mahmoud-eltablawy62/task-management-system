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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _Unit;
        public CategoryService(IUnitOfWork Unit)
        {
            _Unit = Unit;
        }

        public async Task<Category?> AddCategory(string name, string description)
        {
            var category = new Category(name, description);
            await _Unit.Repo<Category>().Add(category);
            await _Unit.CompleteAsync();
            return category;
        }

        public void DeleteCategory(Category category) => 
            _Unit.Repo<Category>().Delete(category);

        public async Task<Category?> GetOrderById(int Id) => 
            await _Unit.Repo<Category>().GetAsync(Id);

        public Category UpdateCat(Category category)
        {
            _Unit.Repo<Category>().Update(category);
            _Unit.CompleteAsync();
            return category;    
        }

        
    }
}

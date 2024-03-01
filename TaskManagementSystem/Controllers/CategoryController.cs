using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.api.Dtos;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Services.Contract;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.api.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CategoryController : BController
    {

        private readonly ICategoryService _CatService;    
        public CategoryController(ICategoryService CatService)
        {
            _CatService = CatService;
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<Category>> AddCategory(CategoryDto dto)
        {
            var category = await _CatService.AddCategory(dto.Name, dto.Description);
            return Ok(category);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int Id, Category Dto)
        {
            var cat = await _CatService.GetOrderById(Id);
            if (cat == null)
            {
                return BadRequest("Not Found");
            }
            cat.Description = Dto.Description;
            cat.Name = Dto.Name;

            _CatService.UpdateCat(cat);

            return Ok(cat);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult<Category>> DeleteCategory (int Id)
        {
            var category = await _CatService.GetOrderById(Id);
            if(category == null)
            {
                return BadRequest("Not Found");
            }else
            {
                _CatService.DeleteCategory(category);              
            }
            return Ok(category);
        }

      
    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.api.Dtos;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Services.Contract;

namespace TaskManagementSystem.api.Controllers
{
   
    public class TaskController : BController
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost("CreateTask")]
        public async Task<ActionResult<Taskat>> CreateTask(TaskDto dto)
        {
            //var user_email = User.FindFirstValue(ClaimTypes.Email);
          
            var task = await _taskService.CreateTaskAsync( dto.CategoryId , dto.User_Email ,dto.Title , dto.Description , dto.DeadLine );

            return Ok(task);
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<Category>> AddCategory(CategoryDto dto)
        {
           var category =  await _taskService.AddCategory(dto.Name, dto.Description);
            return Ok(category);
        }

        [HttpGet]
        public async Task <ActionResult<IReadOnlyList<Taskat>>> GetAllAsync()
        {
           var tasks =   await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{Id}")]
        public async Task <ActionResult<Taskat>> GetByIdAsync( int Id)
        {
            var task = await _taskService.GetOrderById(Id);
            return Ok(task);    
        }





    }
}
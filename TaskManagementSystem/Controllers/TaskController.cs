using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Security.Claims;
using TaskManagementSystem.api.Dtos;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Services.Contract;

namespace TaskManagementSystem.api.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
            var user_email = User.FindFirstValue(ClaimTypes.Email);
            var task = await _taskService.CreateTaskAsync(dto.CategoryId, user_email, dto.Title, dto.Description, dto.DeadLine);
            return Ok(task);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Taskat>>> GetAllAsync()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Taskat>> GetByIdAsync(int Id)
        {
            var task = await _taskService.GetOrderById(Id);
            return Ok(task);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Taskat>> DeleteTakat(int Id)
        {
            var task = await _taskService.GetOrderById(Id);
            if (task == null)
            {
                return BadRequest("NotFound");
            }
            else
            {
                _taskService.Delete(task);
            }
            return Ok(task);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Taskat>> UpdateTask(int Id , TaskDto Dto)
        {
            var task = await _taskService.GetOrderById(Id);
            if (task == null)
            {
                return BadRequest("Not Found");
            }
            task.Title = Dto.Title;
            task.Description = Dto.Description;
            task.DeadLine = Dto.DeadLine; 
            task.CategoryId = Dto.CategoryId;          
            _taskService.Update(task);
            return Ok(task);    
        }

        [HttpPut("Status/{TaskId}")]
        public async Task<ActionResult<Taskat>> UpdateTaskStatus(int TaskId, Status newStatus)
        {
            var task = await _taskService.GetOrderById(TaskId);
            if (task == null)
            {
                return BadRequest(); 
            }
            task.Status = newStatus;
            return Ok(task);
        }
    }
}

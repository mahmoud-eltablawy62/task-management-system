using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.api.Dtos;
using TaskManagementSystem.api.Errors;
using TaskManagementSystem.api.Helpers;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Entities.Identity;
using TaskManagementSystem.Core.Services.Contract;
using TaskManagementSystem.Core.Spacifications;

namespace TaskManagementSystem.api.Controllers
{
   // [Authorize(Roles = "Admin,User")]
    public class TaskController : BController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITaskService _taskService;
        private readonly IMapper _Mapper;
        
        public TaskController(ITaskService taskService , UserManager<User> userManager , IMapper mapper)
        {
            _taskService = taskService;
            _userManager = userManager;
            _Mapper = mapper;
        }
        //////////GetMethod ////////////////
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

        [HttpGet("Pagination/AllTasks")]
        public async Task<ActionResult<Pagination<TaskToReturnDto>>> GetAllTasks([FromQuery] TaskParams Param)
        {
            var products = await _taskService.GetAllTasksAsync(Param);
            var Data = _Mapper.Map<IReadOnlyList<Taskat>, IReadOnlyList<TaskToReturnDto>>(products);
            var count = await _taskService.GetCount(Param);
            return Ok(new Pagination<TaskToReturnDto>(Param.PageSize, Param.PageIndex, Data, count));
        }

        [HttpGet("Taskat/GetTaskByAssignUserID")]
        public async Task<ActionResult<IReadOnlyList<TaskToReturnDto>>> GetTaskByAssignUserID(string UserAssign_Id)
        {
            var Tasks = await _taskService.OrderAsync(UserAssign_Id);
            return Ok(_Mapper.Map<IReadOnlyList<TaskToReturnDto>>(Tasks));
        }
        ///////////////PostMethod/////////////////
        [HttpPost("CreateTask")]
        public async Task<ActionResult<Taskat>> CreateTask(TaskDto dto)
        {
            var user_email = User.FindFirstValue(ClaimTypes.Email);
            var task = await _taskService.CreateTaskAsync(dto.CategoryId, user_email, dto.Title, dto.Description, dto.DeadLine);
            return Ok(task);
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignUserToTask(int Task_Id, string UserId)
        {
            var task = await _taskService.GetOrderById(Task_Id);
            if (task == null)
                return BadRequest(new ApiResponse(400));
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return BadRequest(new ApiResponse(400));
            task.AssignUserId = user.Id;
            _taskService.Update(task);
            return Ok(task);
        }
        ////////////////////// Put Method /////
        [HttpPut("{Id}")]
        public async Task<ActionResult<Taskat>> UpdateTask(int Id, TaskDto Dto)
        {
            var task = await _taskService.GetOrderById(Id);
            if (task == null)
            {
                return BadRequest(new ApiResponse(400));
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
                return BadRequest(new ApiResponse(400));
            }
            task.Status = newStatus;
            _taskService.Update(task);
            if (newStatus == Status.Completed)
            {
                await DeleteTakat(TaskId);
            }
            return Ok(task);
        }
         
         /////////////// Delete Method 
       
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
        [HttpDelete("Delete/DeleteTaskWithDeadLine")]
        public async Task<ActionResult<Taskat>> DeleteTaskDeadLine(DateTime DeadLine)
        {
            _taskService.GetTaskByDeadLineAndDeleteAsync(DeadLine);
            return Ok();
        }




    }
}

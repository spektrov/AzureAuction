using Auction.Business.Interfaces;
using Auction.Business.Requests;
using Auction.Business.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;
using Task = Auction.Data.Entities.Task;


[Route("api/tasks")]
[Authorize]
[ApiController]
public class TasksController : BaseApiController
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var getTasksResponse = await _taskService.GetTasks(UserId);

        if (!getTasksResponse.Success)
        {
            return UnprocessableEntity(getTasksResponse);
        }
            
        var tasksResponse = getTasksResponse.Tasks.Select(o => 
            new TaskResponse { Id = o.Id, IsCompleted = o.IsCompleted, Name = o.Name, Ts = o.Ts });

        return Ok(tasksResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(TaskRequest taskRequest)
    {
        var task = new Task
        {
            IsCompleted = taskRequest.IsCompleted,
            Ts = taskRequest.Ts, 
            Name = taskRequest.Name!,
            UserId = UserId
        };

        var saveTaskResponse = await _taskService.SaveTask(task);

        if (!saveTaskResponse.Success)
        {
            return UnprocessableEntity(saveTaskResponse);
        }

        var taskResponse = new TaskResponse
        {
            Id = saveTaskResponse.Task!.Id,
            IsCompleted = saveTaskResponse.Task.IsCompleted,
            Name = saveTaskResponse.Task.Name,
            Ts = saveTaskResponse.Task.Ts
        };
            
        return Ok(taskResponse);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, TaskUpdateRequest taskRequest)
    {
        if (id != taskRequest.Id)
        {
            return BadRequest("Task not found");
        }
        
        var task = new Task
        {
            Id = taskRequest.Id, IsCompleted = taskRequest.IsCompleted, 
            Ts = taskRequest.Ts, Name = taskRequest.Name, UserId = UserId
        };

        var saveTaskResponse = await _taskService.SaveTask(task);

        if (!saveTaskResponse.Success)
        {
            return UnprocessableEntity(saveTaskResponse);
        }

        var taskResponse = new TaskResponse
        {
            Id = saveTaskResponse.Task!.Id, IsCompleted = saveTaskResponse.Task.IsCompleted,
            Name = saveTaskResponse.Task.Name, Ts = saveTaskResponse.Task.Ts
        };

        return Ok(taskResponse);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteTaskResponse = await _taskService.DeleteTask(id, UserId);
        if (!deleteTaskResponse.Success)
        {
            return UnprocessableEntity(deleteTaskResponse);
        }

        return Ok(deleteTaskResponse.TaskId);
    }
}
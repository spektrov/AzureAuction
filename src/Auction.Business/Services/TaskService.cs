using Auction.Business.Interfaces;
using Auction.Business.Responses;
using Auction.Data;
using Microsoft.EntityFrameworkCore;
using Task = Auction.Data.Entities.Task;

namespace Auction.Business.Services;

public class TaskService : ITaskService
{
     private readonly AuctionDbContext _auctionDbContext;

    public TaskService(AuctionDbContext auctionDbContext)
    {
        _auctionDbContext = auctionDbContext;
    }
    
    public async Task<DeleteTaskResponse> DeleteTask(Guid taskId, Guid userId)
    {
        var task = await _auctionDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
        {
            return new DeleteTaskResponse
            {
                Success = false,
                Error = "Task not found",
                ErrorCode = "T01"
            };
        }

        if (task.UserId != userId)
        {
            return new DeleteTaskResponse
            {
                Success = false,
                Error = "You don't have access to delete this task",
                ErrorCode = "T02"
            };
        }

        _auctionDbContext.Tasks.Remove(task);

        var saveResponse = await _auctionDbContext.SaveChangesAsync();

        if (saveResponse >= 0)
        {
            return new DeleteTaskResponse
            {
                Success = true,
                TaskId = task.Id
            };
        }

        return new DeleteTaskResponse
        {
            Success = false,
            Error = "Unable to delete task",
            ErrorCode = "T03"
        };
    }

    public async Task<GetTasksResponse> GetTasks(Guid userId)
    {
        var tasks = await _auctionDbContext.Tasks.Where(o => o.UserId == userId).ToListAsync();

        return new GetTasksResponse { Success = true, Tasks = tasks };

    }

    public async Task<SaveTaskResponse> SaveTask(Task task)
    {
        if (task.Id.Equals(Guid.Empty))
        {
            await _auctionDbContext.Tasks.AddAsync(task);
        }
        else
        {
            var taskRecord = await _auctionDbContext.Tasks.FindAsync(task.Id);

            if (taskRecord != null)
            {
                taskRecord.IsCompleted = task.IsCompleted;
                taskRecord.Ts = task.Ts;
            }
        }

        var saveResponse = await _auctionDbContext.SaveChangesAsync();
        
        if (saveResponse >= 0)
        {
            return new SaveTaskResponse
            {
                Success = true,
                Task = task
            };
        }
        return new SaveTaskResponse
        {
            Success = false,
            Error = "Unable to save task",
            ErrorCode = "T05"
        };
    }
}
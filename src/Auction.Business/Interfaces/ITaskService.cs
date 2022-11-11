using Auction.Business.Responses;
using Task = Auction.Data.Entities.Task;

namespace Auction.Business.Interfaces;

public interface ITaskService
{
    Task<GetTasksResponse> GetTasks(Guid userId);

    Task<SaveTaskResponse> SaveTask(Task task);

    Task<DeleteTaskResponse> DeleteTask(Guid taskId, Guid userId);
}
using LongoToDoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LongoToDoApp.Services
{
    public interface ITodoApi
    {
        Task<bool> AddTodoItemAsync(TodoItem item);
        Task<bool> UpdateTodoItemAsync(TodoItem item);
        Task<bool> DeleteTodoItemAsync(TodoItem item);
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync(bool forceRefresh = false);
    }
}
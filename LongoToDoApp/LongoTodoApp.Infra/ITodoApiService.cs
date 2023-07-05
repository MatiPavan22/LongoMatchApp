using System.Collections.Generic;
using System.Threading.Tasks;

namespace LongoToDoApp.Infra
{
    public interface ITodoApiService
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task Create(TodoItem item);
        Task<TodoItem> Delete(string key);
        Task Update(TodoItem item);
        Task<TodoItem> GetDetail(string key);

    }
}

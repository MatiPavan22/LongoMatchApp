using System.Diagnostics;
using System;
using Newtonsoft.Json;
using LongoToDoApp.Infra;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LongoToDoApp.Infra
{
    public class TodoApiService : ITodoApiService
    {
        private readonly HttpClient _httpClient;

        public TodoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task Create(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> Delete(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            IEnumerable<TodoItem> todoItemList = null;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(Constants.BASE_URL);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    todoItemList = JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return todoItemList;
        }

        public Task<TodoItem> GetDetail(string key)
        {
            throw new NotImplementedException();
        }

        public Task Update(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}
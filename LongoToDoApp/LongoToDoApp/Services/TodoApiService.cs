using LongoToDoApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LongoToDoApp.Services
{
    public class TodoApiService : ITodoApi
    {
        readonly HttpClient client;

        public TodoApiService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> AddTodoItemAsync(TodoItem item)
        {
            Uri uri = new Uri(Constants.BASE_URL);
            try
            {
                string jsonData = JsonConvert.SerializeObject(item, Formatting.Indented);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                return await Task.FromResult(response.IsSuccessStatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteTodoItemAsync(TodoItem item)
        {
            Uri uri = new Uri($"{Constants.BASE_URL}/{item.Key}");
            try
            {
                string jsonData = JsonConvert.SerializeObject(item, Formatting.Indented);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.DeleteAsync(uri);

                return await Task.FromResult(response.IsSuccessStatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync(bool forceRefresh = false)
        {
            Uri uri = new Uri(string.Format(Constants.BASE_URL, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();

                var TodoItemsList = JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(result);

                return await Task.FromResult(TodoItemsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateTodoItemAsync(TodoItem item)
        {
            Uri uri = new Uri($"{Constants.BASE_URL}/{item.Key}");
            try
            {
                string jsonData = JsonConvert.SerializeObject(item, Formatting.Indented);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(uri, content);

                return await Task.FromResult(response.IsSuccessStatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

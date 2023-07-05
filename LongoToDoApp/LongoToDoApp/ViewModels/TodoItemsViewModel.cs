using LongoToDoApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LongoToDoApp.ViewModels
{
    public class TodoItemsViewModel : BaseViewModel
    {
        public ObservableCollection<TodoItem> TodoItems { get; }
        public Command LoadTodoItemsCommand { get; }

        public Command TodoItemUpdated { get; }
        public Command TodoItemDeleted { get; }

        public TodoItemsViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            LoadTodoItemsCommand = new Command(async () => await ExecuteLoadTodoItemsCommand());

            TodoItemUpdated = new Command(OnTodoItemUpdated);
            TodoItemDeleted = new Command(OnTodoItemDeleted);
        }

        async Task ExecuteLoadTodoItemsCommand()
        {
            IsBusy = true;

            try
            {
                TodoItems.Clear();
                var items = await TodoApi.GetTodoItemsAsync(false);
                foreach (var item in items)
                {
                    TodoItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                await Task.Delay(1000);
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            LoadTodoItemsCommand.Execute(this);
        }

        private async void OnTodoItemUpdated(object obj)
        {
            await TodoApi.UpdateTodoItemAsync((TodoItem)obj);
        }

        private async void OnTodoItemDeleted(object obj)
        {
            await TodoApi.DeleteTodoItemAsync((TodoItem)obj);
            await ExecuteLoadTodoItemsCommand();
        }
    }
}

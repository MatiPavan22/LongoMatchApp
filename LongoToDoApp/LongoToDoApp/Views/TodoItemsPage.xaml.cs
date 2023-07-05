using LongoToDoApp.Models;
using LongoToDoApp.ViewModels;
using System;
using Xamarin.Forms;

namespace LongoToDoApp.Views
{
    public partial class TodoItemsPage : ContentPage
    {
        TodoItemsViewModel _viewModel;

        public TodoItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TodoItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void onUpdateTodoItem(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox.BindingContext != null)
                _viewModel.TodoItemUpdated.Execute(checkbox.BindingContext);
        }

        async void onAddTodoItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemTodoPage());
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            var todoItem = (TodoItem)mi.CommandParameter;
            bool answer = await DisplayAlert("Delete item?", String.Format("ToDo item {0} has been deleted\r\ncorrectly", todoItem.Name), "Yes", "No");
            if (answer)
            {
                _viewModel.TodoItemDeleted.Execute(todoItem);
            }
        }
    }
}
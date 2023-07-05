using LongoToDoApp.Models;
using System;
using Xamarin.Forms;

namespace LongoToDoApp.ViewModels
{
    public class NewTodoItemViewModel : BaseViewModel
    {
        private string text;

        public NewTodoItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public Command SaveCommand { get; }

        private async void OnSave()
        {
            await TodoApi.AddTodoItemAsync(new TodoItem() { Name = Text });
        }
    }
}

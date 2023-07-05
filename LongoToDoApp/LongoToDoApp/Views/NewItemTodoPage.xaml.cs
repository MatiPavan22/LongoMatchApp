using LongoToDoApp.ViewModels;
using Xamarin.Forms;

namespace LongoToDoApp.Views
{
    public partial class NewItemTodoPage : ContentPage
    {
        NewTodoItemViewModel _viewModel;
        public NewItemTodoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewTodoItemViewModel();
        }

        async void onSaveTodoItem(object sender, System.EventArgs e)
        {
            _viewModel.SaveCommand.Execute(this);
            await Navigation.PopAsync();
        }
    }
}
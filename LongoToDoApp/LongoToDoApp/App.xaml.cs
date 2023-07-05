using LongoToDoApp.Services;
using LongoToDoApp.Views;
using Xamarin.Forms;

namespace LongoToDoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<TodoApiService>();
            MainPage = new NavigationPage(new TodoItemsPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

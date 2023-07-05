using Xamarin.UITest;

namespace LongoToDo.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .InstalledApp("com.longomatch.longotodoapp")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}
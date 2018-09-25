using NoQlue.Views;
using Xamarin.Forms;
using NoQlue.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NoQlue
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public static User LoggedInUser { get; set; }
        public static List<Class> Classes { get; set; }
        public static List<Question> Questions { get; set; }
        public static ObservableCollection<Class> ObservableClasses { get; set; }
        public static ObservableCollection<Question> ObservableQuestions { get; set; }

        public App()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new ClassListPage());
            }
            Classes = new List<Class>();
            Questions = new List<Question>();
            ObservableClasses = new ObservableCollection<Class>();
            ObservableQuestions = new ObservableCollection<Question>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using NoQlue.Model;
using NoQlue.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoQlue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassListPage : ContentPage
    {
        public ClassListPage()
        {
            InitializeComponent();

            if (App.LoggedInUser.type.Equals(User.Type.Teacher))
            {
                RepositoryMySql.GetInstance().GetPartimsTeacher(this, int.Parse(App.LoggedInUser.Id));
            }
            else
            {
                RepositoryMySql.GetInstance().GetPartimsStudent(this, int.Parse(App.LoggedInUser.Id));
            }

            Classes.ItemsSource = App.ObservableClasses;

            Classes.ItemTapped += async (sender, e) =>
            {
                await Navigation.PushAsync(new AskQuestionPage((Class)e.Item));
            };
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPartimPage());
        }

        private void Logoff_Clicked(object sender, EventArgs e)
        {
            App.LoggedInUser = null;
            App.IsUserLoggedIn = false;
            Navigation.PushAsync(new LoginPage());
        }

        private void Classes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
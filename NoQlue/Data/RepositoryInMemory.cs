using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoQlue.Model;
using NoQlue.Views;
using Xamarin.Forms;

namespace NoQlue.Data
{
    public class RepositoryInMemory : IRepository
    {
        private static IRepository INSTANCE;

        private List<Class> Classes;
        private List<Question> Questions;

        private RepositoryInMemory()
        {
            Classes = new List<Class>
            {
                new Class { Name="Advanced Server Web", TeacherName="Matthias", Code = 1, Id = 1},
                new Class { Name="Native Mobile Apps", TeacherName="Koen", Code = 2, Id = 2},
                new Class { Name="Smart Devices", TeacherName="Koen", Code = 3, Id = 3}
            };
            Questions = new List<Question>
            {
                new Question { TheClass = Classes[0], TheQuestion = $"Random question for {Classes[0].Name}"},
                new Question { TheClass = Classes[1], TheQuestion = $"Random question for {Classes[1].Name}"},
                new Question { TheClass = Classes[2], TheQuestion = $"Random question for {Classes[2].Name}"}
            };
        }

        public static IRepository GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new RepositoryInMemory();
            }
            return INSTANCE;
        }

        public async void AddPartimStudent(AddPartimPage addPartimPage, string text)
        {
            Classes.Add(new Class { Name = "Add Partim Student" });
            await addPartimPage.DisplayAlert("Success", "", "OK");
        }

        public async void AddPartimTeacher(AddPartimPage addPartimPage, string text1, string text2)
        {
            Classes.Add(new Class { Name = "Add Partim Teacher" });
            await addPartimPage.DisplayAlert("Success", "", "OK");
        }

        public async void AskQuestion(AskQuestionPage askQuestionPage, string question, Class theClass)
        {
            Questions.Add(new Question { TheClass = theClass, TheQuestion = question });
            App.ObservableQuestions.Add(new Question { TheClass = theClass, TheQuestion = question });
            await askQuestionPage.DisplayAlert("Success", "", "OK");
        }

        public void GetPartimsStudent(Page page, int studentId)
        {
            App.Classes = Classes;
        }

        public void GetPartimsTeacher(Page page, int teacherId)
        {
            App.Classes = Classes;
        }

        public void GetQuestionsByPartim(AskQuestionPage askQuestionPage, int id)
        {
            App.Questions = Questions.Where(q => q.TheClass.Code == id).ToList();
            App.ObservableQuestions = new ObservableCollection<Question>(Questions.Where(q => q.TheClass.Code == id).ToList());
        }

        public async void LoginUser(LoginPage loginPage, string email, string password)
        {
            App.LoggedInUser = new User
            {
                Email = email,
                Password = password,
                type = email.Contains("student") ? User.Type.Student : User.Type.Teacher,
                Id = 1 + ""
            };
            App.IsUserLoggedIn = true;
            loginPage.Navigation.InsertPageBefore(new ClassListPage(), loginPage);
            await loginPage.Navigation.PopAsync();
        }
    }
}

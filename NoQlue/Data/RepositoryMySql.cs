using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoQlue.Model;
using NoQlue.Views;
using Xamarin.Forms;

namespace NoQlue.Data
{
    public class RepositoryMySql : IRepository
    {
        private static bool isOnline = true;
        private static IRepository INSTANCE = null;
        private HttpClient client = new HttpClient();

        private RepositoryMySql()
        {
            client.Timeout = new TimeSpan(0, 0, 2);
        }

        public static IRepository GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new RepositoryMySql();
            }
            if (!isOnline)
            {
                return RepositoryInMemory.GetInstance();
            }
            return INSTANCE;
        }

        public async Task<HttpResponseMessage> DoPostAndGetResponse(string url, string json)
        {
            try
            {
                Debug.WriteLine($"Posting {url}");
                return await client.PostAsync(new Uri(url), new StringContent(json, Encoding.UTF8, "application/json"));
            } catch (Exception e)
            {
                isOnline = false;
                Debug.WriteLine(e);
                Debug.WriteLine($"failed posting {url}");
                return null;
            }
        }

        public async void LoginUser(LoginPage loginPage, string email, string password)
        {
            var json = JsonConvert.SerializeObject(new { email, password });
            var response = await DoPostAndGetResponse(Constants.LoginUserUrl, json);

            if (response != null && response.IsSuccessStatusCode)
            {
                var id = await response.Content.ReadAsStringAsync();

                App.LoggedInUser = new User
                {
                    Id = id,
                    type = email.Contains("student") ? User.Type.Student : User.Type.Teacher
                };
                App.IsUserLoggedIn = true;

                loginPage.Navigation.InsertPageBefore(new ClassListPage(), loginPage);
                await loginPage.Navigation.PopAsync();
            } else // check for 403, 404, ...
            {
                
            }

        }

        public async void AskQuestion(AskQuestionPage page, string theQuestion, Class selectedClass)
        {
            var json = JsonConvert.SerializeObject(new Question
            {
                TheUser = App.LoggedInUser,
                TheQuestion = theQuestion,
                TheClass = selectedClass
            });
            var response = await DoPostAndGetResponse(Constants.AskQuestionUrl, json);
            if (response != null && response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"Succesfully posted question");
                await page.DisplayAlert("Success", "Your question was posted, wait for the teacher to answer it.", "OK");
                GetQuestionsByPartim(page, selectedClass.Id);
            } else
            {
                await page.DisplayAlert("Fail", "Something went wrong, please try again.", "OK");
            }
        }

        public async void AddPartimStudent(AddPartimPage addPartimPage, string text)
        {
            var json = JsonConvert.SerializeObject(new
            {
                App.LoggedInUser.Id,
                Code = text
            });
            var res = await DoPostAndGetResponse(Constants.AddPartimStudentUrl, json);
            if (res != null && res.IsSuccessStatusCode)
            {
                await addPartimPage.DisplayAlert("Succes", "Partim successfully added.", "OK");
                await addPartimPage.Navigation.PopAsync();
                GetPartimsStudent(new ClassListPage(), int.Parse(App.LoggedInUser.Id));
            } else
            {
                await addPartimPage.DisplayAlert("Error", "Something went wrong.", "OK");
            }
        }

        public async void AddPartimTeacher(AddPartimPage addPartimPage, string PartimName, string PartimCode)
        {
            var json = JsonConvert.SerializeObject(new
            {
                App.LoggedInUser.Id,
                PartimName,
                PartimCode
            });
            var res = await DoPostAndGetResponse(Constants.AddPartimTeacherUrl, json);
            if (res != null && res.IsSuccessStatusCode)
            {
                await addPartimPage.DisplayAlert("OK", "Added new partim", "continue");
                await addPartimPage.Navigation.PopAsync();
                GetPartimsTeacher(new ClassListPage(), int.Parse(App.LoggedInUser.Id));
            } else
            {
                await addPartimPage.DisplayAlert("Failure", "Something went wrong", "ok");
            }
        }

        public async void GetPartimsStudent(Page page, int studentId)
        {
            var uri = new Uri($"{Constants.ApiBaseUrl}partims/student/{studentId}");
            HttpResponseMessage res = null;
            res = await client.GetAsync(uri);
            if (res.IsSuccessStatusCode)
            {
                var str = await res.Content.ReadAsStringAsync();
                SaveClasses(str);
            }
        }

        public async void GetPartimsTeacher(Page page, int teacherId)
        {
            var uri = new Uri($"{Constants.ApiBaseUrl}partims/teacher/{teacherId}");
            HttpResponseMessage res = null;
            res = await client.GetAsync(uri);
            if (res.IsSuccessStatusCode)
            {
                var str = await res.Content.ReadAsStringAsync();
                SaveClasses(str);
            }
        }

        private void SaveClasses(string str)
        {
            Debug.WriteLine(str);
            var json = JsonConvert.DeserializeAnonymousType(str, new[] {
                    new { id = "", teacher_id = "", naam = "", code = "", teacher_naam = "" }
                });
            App.ObservableClasses.Clear();
            foreach (var jj in json)
            {
                App.ObservableClasses.Add(new Class { Name = jj.naam, Id = int.Parse(jj.id), TeacherName = jj.teacher_naam });
            }
        }

        public async void GetQuestionsByPartim(AskQuestionPage askQuestionPage, int partimId)
        {
            var uri = new Uri($"{Constants.ApiBaseUrl}questions/{partimId}");
            HttpResponseMessage res = null;
            res = await client.GetAsync(uri);
            if (res.IsSuccessStatusCode)
            {
                var str = await res.Content.ReadAsStringAsync();
                Debug.WriteLine(str);
                var json = JsonConvert.DeserializeAnonymousType(str, new[] {
                    new { id = "", vraag = "", user_id = "", partim_id = ""}
                });
                App.ObservableQuestions.Clear();
                foreach (var jj in json)
                {
                    App.ObservableQuestions.Add(new Question { TheQuestion = jj.vraag });
                }
            }
        }
    }
}

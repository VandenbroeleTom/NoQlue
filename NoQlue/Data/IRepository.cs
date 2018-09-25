using NoQlue.Model;
using NoQlue.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoQlue.Data
{
    public interface IRepository
    {
        void LoginUser(LoginPage page, string email, string password);

        void AskQuestion(AskQuestionPage page, string question, Class theClass);

        void GetPartimsStudent(Page page, int studentId);

        void GetPartimsTeacher(Page page, int teacherId);
        void AddPartimStudent(AddPartimPage addPartimPage, string text);
        void AddPartimTeacher(AddPartimPage addPartimPage, string text1, string text2);
        void GetQuestionsByPartim(AskQuestionPage askQuestionPage, int id);
    }
}

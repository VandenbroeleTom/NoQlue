using System;
using System.Collections.Generic;
using System.Net.Http;

using Newtonsoft.Json;

using Xamarin.Forms;
using NoQlue.Model;
using System.Text;
using System.Diagnostics;
using NoQlue.Data;

namespace NoQlue.Views
{
    public partial class AskQuestionPage : ContentPage
    {
        private Class SelectedClass { get; set; }

        public AskQuestionPage()
        {
            InitializeComponent();
        }

        public AskQuestionPage(Class selectedClass)
        {
            InitializeComponent();
            SelectedClass = selectedClass;
            Title = selectedClass.Name;
            RepositoryMySql.GetInstance().GetQuestionsByPartim(this, SelectedClass.Id);
            Questions.ItemsSource = App.ObservableQuestions;
        }

        void OnSendQuestion_Clicked(object sender, EventArgs e)
        {
            RepositoryMySql.GetInstance().AskQuestion(this, txtMessage.Text, SelectedClass);
            txtMessage.Text = "";
        }

        private void Questions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}

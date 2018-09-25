using NoQlue.Data;
using NoQlue.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace NoQlue.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            RepositoryMySql.GetInstance().LoginUser(this, usernameEntry.Text, passwordEntry.Text);
        }
    }
}

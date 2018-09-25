using System;
using System.Collections.Generic;
using NoQlue.Data;
using NoQlue.Model;
using Xamarin.Forms;

namespace NoQlue.Views
{
    public partial class AddPartimPage : ContentPage
    {
        public AddPartimPage()
        {
            InitializeComponent();
            if (App.LoggedInUser.type.Equals(User.Type.Student))
            {
                PartimName.Opacity = 0;
            }
        }

        private void btn_Add_Clicked(object sender, EventArgs e)
        {
            if (App.LoggedInUser.type.Equals(User.Type.Student))
            {
                RepositoryMySql.GetInstance().AddPartimStudent(this, PartimCode.Text);
            } else
            {
                RepositoryMySql.GetInstance().AddPartimTeacher(this, PartimName.Text, PartimCode.Text);
            }
        }
    }
}

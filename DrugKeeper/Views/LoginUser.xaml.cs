using DrugKeeper.Models;
using DrugKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrugKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUser : ContentPage
    {
        private User LoggedUser;

        private readonly UserViewModel BaseViewModel;
        public LoginUser()
        {
            InitializeComponent();
            LoggedUser = new User()
            {
                Username = "username",
                Password = "password",
                Age = 0,
                Gender = "M"
            };
            BaseViewModel = new UserViewModel();
            BindingContext = LoggedUser;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (BaseViewModel.MongoRepo.CheckUser(LoggedUser))
            {
                var logged = await BaseViewModel.MongoRepo.GetUserAsync(LoggedUser.Username);
                MessagingCenter.Send(this, "LoggedUser", logged);
                MessagingCenter.Send(this, "LoggedUserReminder", logged);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Login Process", "Username or Password incorrect!", "OK");
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));
        }
    }
}
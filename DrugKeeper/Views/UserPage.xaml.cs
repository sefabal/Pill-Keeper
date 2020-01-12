using DrugKeeper.Models;
using DrugKeeper.Services;
using DrugKeeper.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrugKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class UserPage : ContentPage
    {
        private UserViewModel UserViewModel;
        private MongoRepo MongoRepo => DependencyService.Get<MongoRepo>();
        public UserPage()
        {
            InitializeComponent();

            UserViewModel = new UserViewModel();

            MessagingCenter.Subscribe<LoginUser, User>(this, "LoggedUser", (obj, user) =>
            {
                var logUser = user as User;

                LoginButton.IsVisible = false;

                UserViewModel.User = user;
            });

            BindingContext = UserViewModel;
        }

        async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LoginUser()));
        }

        async void Logout_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Login Process", "LOGGED OUT!", "OK");
            MessagingCenter.Send(this, "LoggedOutUser", new User());
            MongoRepo.LoggedUser = null;
            UserViewModel.User = null;
            LoginButton.IsVisible = true;
        }

    }
}
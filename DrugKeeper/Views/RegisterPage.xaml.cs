using DrugKeeper.Models;
using DrugKeeper.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrugKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private BaseViewModel BaseView => new BaseViewModel();

        public User RegisteredUser;

        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = RegisteredUser = new User()
            {
                Reminders = new System.Collections.Generic.List<Reminder>()
            };
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            //Check for related fields
            // TODO : Username,Password,Gender,Age
            if (!string.IsNullOrEmpty(RegisteredUser.Username) && !string.IsNullOrEmpty(RegisteredUser.Password))
            {
                BaseView.MongoRepo.RegisterUser(RegisteredUser);
                await DisplayAlert("Register Process", "You registered succesfully!", "OK");
                await Navigation.PopModalAsync();
            }
        }
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
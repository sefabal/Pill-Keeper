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
    public partial class ReminderPage : ContentPage
    {

        RemindersViewModel ReminderViewModel;

        public ReminderPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<LoginUser, User>(this, "LoggedUser", (obj, user) =>
            {
                var logUser = user as User;
                NotLogged.IsVisible = false;
            });

            MessagingCenter.Subscribe<LoginUser, User>(this, "LoggedOutUser", (obj, user) =>
            {
                var logUser = user as User;
                NotLogged.IsVisible = true;
                ReminderViewModel.Reminders.Clear();
            });

            BindingContext = ReminderViewModel = new RemindersViewModel();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewReminderPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ReminderViewModel.Reminders.Count == 0)
            {
                if (ReminderViewModel.MongoRepo.LoggedUser != null)
                {
                    NotLogged.IsVisible = false;
                    ReminderViewModel.LoadItemsCommand.Execute(null);
                }
                else
                {
                    NotLogged.IsVisible = true;
                }
            }
        }
    }
}
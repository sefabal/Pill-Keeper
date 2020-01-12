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

        RemindersViewModel reminderViewModel;

        public ReminderPage()
        {
            InitializeComponent();

            BindingContext = reminderViewModel = new RemindersViewModel();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewReminderPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (reminderViewModel.Reminders.Count == 0)
            {
                if (reminderViewModel.MongoRepo.LoggedUser != null)
                {
                    reminderViewModel.LoadItemsCommand.Execute(null);
                }
            }
        }
    }
}
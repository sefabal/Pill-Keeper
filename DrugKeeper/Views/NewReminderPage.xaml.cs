using DrugKeeper.Models;
using DrugKeeper.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrugKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewReminderPage : ContentPage
    {

        NewReminderViewModel NewreminderViewModel;

        public Reminder NewReminder;

        public NewReminderPage()
        {
            InitializeComponent();

            this.NewreminderViewModel = new NewReminderViewModel();

            BindingContext = NewReminder = new Reminder()
            {
                Amount = 0,
                Dose = 1,
                FrequencyHour = 12,
                StartingDate = DateTime.Now
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (PillPicker.ItemsSource == null)
                PillPicker.ItemsSource = await NewreminderViewModel.MongoRepo.GetAllPills();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var Item = NewReminder;
            Item.Pill = (Pill)PillPicker.SelectedItem;
            if (Item.Pill != null && Item.StartingDate != null && Item.Amount != 0)
            {
                Item.Id = new Guid();
                MessagingCenter.Send(this, "AddReminder", Item);
                NewreminderViewModel.MongoRepo.AddReminder(Item);
                await DisplayAlert("Reminders", "Reminder added succesfully.", "OK");

                await Navigation.PopModalAsync();
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
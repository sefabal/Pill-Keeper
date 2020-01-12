using DrugKeeper.Models;
using DrugKeeper.Services;
using DrugKeeper.ViewModels;
using System;
using System.Globalization;
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
                Amount = 20,
                Dose = 1,
                FrequencyHour = 12,
                StartingDate = DateTime.Now
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (PillPicker.ItemsSource == null)
            {
                PillPicker.ItemsSource = await NewreminderViewModel.MongoRepo.GetAllPills();
                PillPicker.ItemDisplayBinding = new Binding("Name");
            }
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
                SaveLocalNotification(Item);
                await DisplayAlert("Reminders", "Reminder added succesfully.", "OK");
                await Navigation.PopModalAsync();
            }
        }
        async void SaveLocalNotification(Reminder reminder)
        {

            var selectedDate = reminder.StartingDate;
            var frequencyHour = reminder.FrequencyHour;
            var MessageText = "Time to take your pill : " + reminder.Pill.Name;
            DependencyService.Get<ILocalNotificationService>().Cancel(0);
            DependencyService.Get<ILocalNotificationService>().LocalNotification("Pill Notification", MessageText, 0, selectedDate, frequencyHour);
            await DisplayAlert("Pill Notification", "Notification details saved successfully ", "Ok");
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
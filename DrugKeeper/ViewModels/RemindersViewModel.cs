using DrugKeeper.Models;
using DrugKeeper.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrugKeeper.ViewModels
{
    public class RemindersViewModel : BaseViewModel
    {
        public ObservableCollection<Reminder> Reminders { get; set; }

        public Command LoadItemsCommand { get; set; }

        public RemindersViewModel()
        {
            Title = "Reminders";
            Reminders = new ObservableCollection<Reminder>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewReminderPage, Reminder>(this, "AddReminder", (obj, reminder) =>
            {
                var newReminder = reminder as Reminder;
                Reminders.Add(newReminder);
                MongoRepo.AddReminder(newReminder);
            });
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Reminders.Clear();
                if (MongoRepo.LoggedUser != null)
                {
                    var pills = await MongoRepo.GetAllRemindersAsync(MongoRepo.LoggedUser.Username);

                    foreach (var pill in pills)
                    {
                        Reminders.Add(pill);
                    }
                } else
                {
                    await Application.Current.MainPage.DisplayAlert("Reminders", "You should login to see reminders!", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}


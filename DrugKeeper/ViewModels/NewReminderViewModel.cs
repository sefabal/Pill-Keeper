using DrugKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugKeeper.ViewModels
{
    class NewReminderViewModel : BaseViewModel
    {
        public IList<Pill> Pills { get; set; }

        public Reminder NewReminder;

        public double amount;
        public double frequencyHour;
        public double dose;

        public string Amount { get { return String.Format("{0:N}", this.amount); } }
        public string FrequencyHour { get { return String.Format("{0:N}", this.frequencyHour); } }
        public DateTime StartingDate { get; set; }
        public string Dose { get { return String.Format("{0:N}", this.dose); } }

        public NewReminderViewModel()
        {
            NewReminder = new Reminder();

            Pills = new List<Pill>();
        }

    }
}

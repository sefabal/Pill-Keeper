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

        public NewReminderViewModel()
        {
            NewReminder = new Reminder();

            Pills = new List<Pill>();
        }

    }
}

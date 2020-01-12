using DrugKeeper.Models;
using DrugKeeper.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DrugKeeper.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        User user;

        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        public UserViewModel()
        {
            Title = "User";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
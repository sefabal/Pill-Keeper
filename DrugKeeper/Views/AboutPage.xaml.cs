using DrugKeeper.Services;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrugKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        private MongoRepo MongoRepo => DependencyService.Get<MongoRepo>();
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}
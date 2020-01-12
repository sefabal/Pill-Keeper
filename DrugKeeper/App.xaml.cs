using DrugKeeper.Services;
using DrugKeeper.Views;
using Xamarin.Forms;

namespace DrugKeeper
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MongoRepo>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}

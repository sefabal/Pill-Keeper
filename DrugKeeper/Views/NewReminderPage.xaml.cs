using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrugKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewReminderPage : ContentPage
    {
        public NewReminderPage()
        {
            InitializeComponent();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Item.Details) && !string.IsNullOrEmpty(Item.Name) && !string.IsNullOrEmpty(Item.SideEffects))
            //{
            //    Item.Id = new Guid();
            //    MessagingCenter.Send(this, "AddItem", Item);
            //    await Navigation.PopModalAsync();
            //}
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
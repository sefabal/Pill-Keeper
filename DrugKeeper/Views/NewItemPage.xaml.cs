using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DrugKeeper.Models;

namespace DrugKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Pill Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Pill
            {
                Name = "Pill Name",
                Details = "This is an pill details.",
                SideEffects = "Pill side effects"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Item.Details) && !string.IsNullOrEmpty(Item.Name) && !string.IsNullOrEmpty(Item.SideEffects))
            {
                Item.Id = new Guid();
                MessagingCenter.Send(this, "AddItem", Item);
                await Navigation.PopModalAsync();
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
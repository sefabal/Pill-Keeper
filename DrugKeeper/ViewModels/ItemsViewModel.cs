using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DrugKeeper.Models;
using DrugKeeper.Views;

namespace DrugKeeper.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Pill> Pills { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Pills";
            Pills = new ObservableCollection<Pill>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Pill>(this, "AddItem", (obj, item) =>
            {
                var newItem = item as Pill;
                Pills.Add(newItem);
                MongoRepo.AddPill(newItem);
            });
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Pills.Clear();
                var pills = await MongoRepo.GetAllPills();
                foreach (var pill in pills)
                {
                    Pills.Add(pill);
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
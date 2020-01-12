using DrugKeeper.Models;
using DrugKeeper.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace DrugKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MapPage : ContentPage
    {
        private BaseViewModel BaseView => new BaseViewModel();
        public MapPage()
        {
            InitializeComponent();

            var location = Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync().Result;
            Position position;
            if (location != null)
            {

                position = new Position(location.Latitude, location.Longitude);
            }
            else
            {
                position = new Position(0, 0);
            }
            MapSpan mapSpan = new MapSpan(position, 0.04, 0.04);
            map.MoveToRegion(mapSpan);
            var online = BaseView.MongoRepo.GetOnlinePharmacy();

            foreach (OnlinePharmacy.Data data in online.data)
            {
                string[] locations = data.konum.Split(',');
                var latitute = Double.Parse(locations[0]);
                var longtitute = Double.Parse(locations[1]);
                Position pharmacyPos = new Position(latitute, longtitute);

                map.Pins.Add(new Pin() { Label = data.eczane_adi + " Eczanesi", Position = pharmacyPos, Type = PinType.Place });
            }
        }
    }
}
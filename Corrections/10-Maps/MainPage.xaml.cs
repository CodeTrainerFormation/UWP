using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _10_Maps
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<MapLocation> LocationsFound { get; set; }


        public MainPage()
        {
            this.InitializeComponent();
            LocationsFound = new ObservableCollection<MapLocation>();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            pbSearch.Visibility = Visibility.Visible;
            var access = await Geolocator.RequestAccessAsync();

            if(access == GeolocationAccessStatus.Allowed)
            {
                string addressToGeocode = tbSearch.Text;

                BasicGeoposition queryHint = new BasicGeoposition();
                queryHint.Latitude = 45.75801;
                queryHint.Longitude = 4.8001016;
                Geopoint hintPoint = new Geopoint(queryHint);

                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 5);

                LocationsFound.Clear();
                
                foreach(var location in result.Locations)
                {
                    LocationsFound.Add(location);
                }

            }
            else if(access == GeolocationAccessStatus.Denied)
            {
                bool result = await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
            }

            pbSearch.Visibility = Visibility.Collapsed;
        }

        private void PinToMap_Click(object sender, ItemClickEventArgs e)
        {
            var location = (MapLocation)e.ClickedItem;

            this.AddPointOnMap(location.Point.Position.Latitude,
                location.Point.Position.Longitude,
                location.DisplayName);
        }

        private void AddPointOnMap(double latitude, double longitude, string label)
        {
            BasicGeoposition snPosition = new BasicGeoposition() { Latitude = latitude, Longitude = longitude };
            Geopoint snPoint = new Geopoint(snPosition);

            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = snPoint;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = label;
            mapIcon1.ZIndex = 0;

            mcMap.MapElements.Add(mapIcon1);

            mcMap.Center = snPoint;
            mcMap.ZoomLevel = 17;

            //LocationsFound.Clear();
        }
    }
}

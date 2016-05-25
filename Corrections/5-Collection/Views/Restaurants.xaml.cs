using _5_Collection.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _5_Collection.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Restaurants : Page
    {
        public ObservableCollection<Restaurant> RestaurantsList { get; set; }

        public Restaurants()
        {
            this.InitializeComponent();
            RestaurantsList = new ObservableCollection<Restaurant>();

            this.LoadRestaurants();
        }

        public async void LoadRestaurants()
        {
            Uri jsonPath = new Uri("ms-appx:///Data/restaurants.json");
            StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(jsonPath);

            string contentRestaurantsJson = await FileIO.ReadTextAsync(f);

            RestaurantsList = JsonConvert.DeserializeObject<ObservableCollection<Restaurant>>(contentRestaurantsJson);
        }
    }
}

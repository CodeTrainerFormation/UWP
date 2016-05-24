using _4_Json.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace _4_Json.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();

            //this.LoadJsonAsync();
        }

        public async void LoadJsonAsync()
        {
            StorageFile sampleFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Data/one_restaurant.json"));

            string contentFileJson = await FileIO.ReadTextAsync(sampleFile);

            Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(contentFileJson);

            this.DataContext = restaurant;
        }
    }
}

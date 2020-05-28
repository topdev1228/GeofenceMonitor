using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeofenceMonitor
{
    public partial class MainPage : ContentPage
    {
        public List<GeofenceList> list { get; set; }

        public MainPage()
        {
            InitializeComponent();
            this.GetGeofencesList();
            //          this.GeofencingPost();
            //foreach (GeofenceList item in list)
            //{
            //    logview.Text += item.name + "," + item.latitude + "," + item.longitude + "," + item.entryRadius + "," + item.exitRadius + "\n";
            //}
            System.Console.WriteLine("DEBUG - ## MainPage.xaml.cs ## Start from here !!!");
        }

        void OnStartButtonClicked(object sender, EventArgs e)
        {
            System.Console.WriteLine("DEBUG - Start-Button Click: ----------------------");
        }

        void OnStopButtonClicked(object sender, EventArgs e)
        {
            System.Console.WriteLine("DEBUG - Stop-Button Click: -----------------------");
        }

        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            string userId = e.NewTextValue;
            System.Console.WriteLine("DEBUG - userId: " + userId);
        }

        void OnEditorCompleted(object sender, EventArgs e)
        {
            string text = ((Editor)sender).Text;
            System.Console.WriteLine("DEBUG - EditorCompleted: " + text );
        }

        private async void GeofencingPost()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://dev.powerguides.net/api/power/postgeofenceevent?licensekey=geofence&id=1&direction=enter");

            string jsonData = @"device=93AA1A61-6668-4139-8E2C-A142ACAD93E3&device_model=iPhone&device_type=iOS&id=1&latitude=55.95130496222&longitude=12.2736611&timestamp=1589116686.642903&trigger";

            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();

                // debug response result for request | "success":?
                System.Console.WriteLine(" ###### DEBUG AAAAA - Response Result: " + result);
            }
            catch (Exception er)
            {
                var lb = er.ToString();
                var js = "xs";
            }
        }

        private async void GetGeofencesList()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://dev.powerguides.net/api/power/RequestGeofenceLocations?licensekey=GeofenceAndroid");

            string jsonData = @"{""first_name"" : ""Sashell"", ""last_name"" : ""hijo de haskell"",  ""phone"" : ""555-555-555"",  ""email"" : ""blancavergas@gmail.com"", ""address"" : ""revolucion 205"", ""city"" : ""mexico"", ""state"" : ""MX""}";

            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "text/plain");
                HttpResponseMessage response = await client.PostAsync("", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<GeofenceList>>(result);

                foreach (GeofenceList item in list)
                {
                    logview.Text += item.name + ", " + item.latitude + ", " + item.longitude + ", " + item.entryRadius + ", " + item.exitRadius + "\n";
                }

                System.Console.WriteLine(" ###### DEBUG BBBB - Response Result List: " + list);
            }
            catch (Exception er)
            {
                var lb = er.ToString();
                var js = "xs";
            }
        }
    }

    public class GeofenceList
    {
        public string ID { get; set; }
        public string name { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int entryRadius { get; set; }
        public int exitRadius { get; set; }
    }
}

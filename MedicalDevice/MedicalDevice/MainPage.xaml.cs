using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MedicalDevice
{
    public class BMP
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("last_app")]
        public string LastApp { get; set; }
        [JsonProperty("connected")]
        public bool Connected { get; set; }
        [JsonProperty("return_value")]
        public int ReturnValue { get; set; }
    }

    public class SPO2
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("last_app")]
        public string LastApp { get; set; }
        [JsonProperty("connected")]
        public bool Connected { get; set; }
        [JsonProperty("return_value")]
        public int ReturnValue { get; set; }
    }

    public class Temperature
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("last_app")]
        public string LastApp { get; set; }
        [JsonProperty("connected")]
        public bool Connected { get; set; }
        [JsonProperty("return_value")]
        public int ReturnValue { get; set; }
    }

    public partial class MainPage : ContentPage
	{
	    private const string Url = "https://api.particle.io/v1/devices/44002f001047333439313830";
       
        private HttpClient PhotonHttpClient = new HttpClient();

	    private async void GetBMP()
	    {
	        var body = new List<KeyValuePair<string, string>>
	        {
	            new KeyValuePair<string, string>("access_token", "a2a3c965e642e69ba65ec750471e0c0ccd3f0be6"),
	        };
	        var content = new FormUrlEncodedContent(body);
	        var response = await PhotonHttpClient.PostAsync(Url + "/BPM", content);
	        var responseContent = await response.Content.ReadAsStringAsync();
	        var photonValues = JsonConvert.DeserializeObject<BMP>(responseContent);
	        BMP_label.Text = String.Format("{0} BMP", photonValues.ReturnValue);
	    }

	    private async void GetSPO2()
	    {
	        var body = new List<KeyValuePair<string, string>>
	        {
	            new KeyValuePair<string, string>("access_token", "a2a3c965e642e69ba65ec750471e0c0ccd3f0be6"),
	        };
	        var content = new FormUrlEncodedContent(body);
	        var response = await PhotonHttpClient.PostAsync(Url + "/Spo2", content);
	        var responseContent = await response.Content.ReadAsStringAsync();
	        var photonValues = JsonConvert.DeserializeObject<SPO2>(responseContent);
	        SPO2_label.Text = String.Format("{0} %", photonValues.ReturnValue);
	    }

        private async void GetTemp()
	    {
	        var body = new List<KeyValuePair<string, string>>
	        {
	            new KeyValuePair<string, string>("access_token", "a2a3c965e642e69ba65ec750471e0c0ccd3f0be6"),
	        };
	        var content = new FormUrlEncodedContent(body);
	        var response = await PhotonHttpClient.PostAsync(Url + "/Tmp", content);
	        var responseContent = await response.Content.ReadAsStringAsync();
	        var photonValues = JsonConvert.DeserializeObject<Temperature>(responseContent);
	        Temp_label.Text = String.Format("{0:F1} °C", (photonValues.ReturnValue)/10.0);
	    }

        public MainPage()
		{
			InitializeComponent();
		}

	    protected override async void OnAppearing()
	    {
	        await ShowValuesAsync();
	        base.OnAppearing();
	    }


        private async void Settings_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new SettingsPage());
	    }

	    private async Task ShowValuesAsync()
	    {
	        while (true)
	        {
	            await Task.Delay(1000);
	            GetBMP();
                GetSPO2();
                GetTemp();
	        }
	    }
    }
}

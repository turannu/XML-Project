using Azure.Core;
using dotenv.net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SipsBites;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XML_Project.Pages
{
    public class BreweryModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<BreweryModel> _logger;

        public List<Brewery> Breweries { get; set; }
        public string Brand { get; set; }
        public string GoogleMapsApiKey { get; set; }

        public BreweryModel(ILogger<BreweryModel> logger)
        {
            _logger = logger;
            Breweries = new List<Brewery>();
        }

        public async Task OnGetAsync()
        {
            // Load the API key from the .env file
            DotEnv.AutoConfig();  // Load the .env file to get API key
            GoogleMapsApiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY");

            string brand = "Sips and Bites Navigator";
            string inBrand = Request.Query["Brand"];

            if (!string.IsNullOrEmpty(inBrand))
            {
                brand = inBrand;
            }

            Brand = brand;
            Breweries = await GetBreweryDataAsync();
        }

        private async Task<List<Brewery>> GetBreweryDataAsync()
        {
            // Get brewery data from API
            HttpResponseMessage result = await client.GetAsync("https://api.openbrewerydb.org/breweries");
            List<Brewery> breweries = new List<Brewery>();

            if (result.IsSuccessStatusCode)
            {
                string breweryJson = await result.Content.ReadAsStringAsync();
                JArray jsonarray = JArray.Parse(breweryJson);
                breweries = jsonarray.ToObject<List<Brewery>>();

                // Loop through each brewery and add the Google Maps URL
                foreach (var brewery in breweries)
                {
                    // Construct Google Maps URL using latitude and longitude (if available)
                    if (!string.IsNullOrEmpty(brewery.Latitude) && !string.IsNullOrEmpty(brewery.Longitude))
                    {
                        string mapUrl = $"https://www.google.com/maps?q={brewery.Latitude},{brewery.Longitude}&key={GoogleMapsApiKey}";
                        brewery.MapUrl = mapUrl;
                    }
                }
            }

            return breweries;
        }
    }
}

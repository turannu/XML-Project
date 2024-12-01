using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SipsBites;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XML_Project.Pages
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            List<Brewery> breweries = await GetBreweryDataAsync();
            ViewData["Breweries"] = breweries;
        }
      

        private async Task<List<Brewery>> GetBreweryDataAsync()
        {
            string brand = "Sips n Bites ";
            string inBrand = Request.Query["Brand"];
            if (!string.IsNullOrEmpty(inBrand))
            {
                brand = inBrand;
            }
            ViewData["Brand"] = brand;

            HttpResponseMessage result = await client.GetAsync("https://api.openbrewerydb.org/breweries");
            List<Brewery> breweries = new List<Brewery>();

            if (result.IsSuccessStatusCode)
            {
                string breweryJson = await result.Content.ReadAsStringAsync();
                JArray jsonarray = JArray.Parse(breweryJson);

                breweries = jsonarray.ToObject<List<Brewery>>();
            }
            BreweryRepo.allBreweries = breweries;
            return breweries;

        }

    }
}
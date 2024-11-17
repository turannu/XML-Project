using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SipsBites;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using USAState;

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

        public void OnGet()
        {
            List<Brewery> breweries = GetBreweryData();
            ViewData["Breweries"] = breweries;
        }

        private List<Brewery> GetBreweryData()
        {
            string brand = "Sips and Bites Navigator";
            string inBrand = Request.Query["Brand"];
            if (!string.IsNullOrEmpty(inBrand))
            {
                brand = inBrand;
            }
            ViewData["Brand"] = brand;

            var task = client.GetAsync("https://api.openbrewerydb.org/breweries");
            HttpResponseMessage result = task.Result;
            List<Brewery> breweries = new List<Brewery>();

            if (result.IsSuccessStatusCode)
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string jsonString = readString.Result;
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("Data/breweryschema.json"));
                JArray jsonarray = JArray.Parse(jsonString);
                IList<string> validationEvents = new List<string>();

                if (jsonarray.IsValid(schema, out validationEvents))
                {
                    breweries = Brewery.FromJson(jsonString);
                }
                else
                {
                    foreach (string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                    }
                }
            }
            Task<HttpResponseMessage> Brewerystate = client.GetAsync("https://worldpopulationreview.com/static/states/abbr-name-list.json");
            HttpResponseMessage stateresult = Brewerystate.Result;
            Task<string> BrewerystateString = stateresult.Content.ReadAsStringAsync();
            string stateJson = BrewerystateString.Result;
            List<UsState> states = UsState.FromJson(stateJson);
            
            // Create a dictionary to map state names to their abbreviations
            IDictionary<string, string> stateAbbreviationMap = new Dictionary<string, string>();
            foreach (var state in states)
            {
                stateAbbreviationMap[state.Name] = state.Abbreviation;
            }

            // Join brewery data with state abbreviation based on state name
            foreach (var brewery in breweries)
            {
                if (brewery.StateProvince != null && stateAbbreviationMap.ContainsKey(brewery.StateProvince))
                {
                    brewery.StateAbbreviation = stateAbbreviationMap[brewery.StateProvince];  // Assign state abbreviation
                }
                else
                {
                    brewery.StateAbbreviation = "Unknown";  // If state not found, mark it as "Unknown"
                }
            }




            return breweries;

        }
    }
}

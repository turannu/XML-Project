using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SipsBites;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
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

            string brand = "Sips and Bites  Navigator";
            string inBrand = Request.Query["Brand"];
            if (inBrand != null && inBrand.Length > 0)
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
                ViewData["Breweries"] = breweries;




            }
        }
} }

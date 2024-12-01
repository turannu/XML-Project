using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SipsBites;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XML_Project.Pages
{
    public class BreweryModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<SearchModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string State { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BreweryType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string City { get; set; }

        public List<Brewery> SearchResults { get; set; } = new List<Brewery>();

        public BreweryModel(ILogger<SearchModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            // Only perform search if any field is provided
            if (!string.IsNullOrEmpty(State) || !string.IsNullOrEmpty(BreweryType) || !string.IsNullOrEmpty(City))
            {
                string baseUrl = "https://api.openbrewerydb.org/breweries?";
                var queryParams = new List<string>();

                if (!string.IsNullOrEmpty(State))
                {
                    queryParams.Add($"by_state={Uri.EscapeDataString(State)}");
                }

                if (!string.IsNullOrEmpty(City))
                {
                    queryParams.Add($"by_city={Uri.EscapeDataString(City)}");
                }

                if (!string.IsNullOrEmpty(BreweryType))
                {
                    queryParams.Add($"by_type={Uri.EscapeDataString(BreweryType)}");
                }

                string apiUrl = baseUrl + string.Join("&", queryParams);

                try
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        SearchResults = Brewery.FromJson(jsonString);
                    }
                    else
                    {
                        _logger.LogError($"API request failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error fetching brewery data: {ex.Message}");
                }
            }
        }
    }
}

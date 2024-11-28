using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SipsBites;

namespace XML_Project.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<SearchModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string State { get; set; }

        [BindProperty(SupportsGet = true)]
        public string City { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BreweryType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Brewery> SearchResults { get; set; } = new List<Brewery>();
        public List<Restaurant.RestaurantUsa> Restaurants { get; set; } = new List<Restaurant.RestaurantUsa>();
        public List<USAState.UsState> USStates { get; set; } = new List<USAState.UsState>();
        public List<string> AvailableCities { get; set; } = new List<string>();

        public SearchModel(IHttpClientFactory clientFactory, ILogger<SearchModel> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                await LoadDataAsync();
                PopulateAvailableCities();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data in OnGetAsync");
                ModelState.AddModelError(string.Empty, "Unable to load data. Please try again later.");
            }
        }

        private void PopulateAvailableCities()
{    //By help of AI
    // If a state is selected, show cities only for that state
    if (!string.IsNullOrEmpty(State))
    {
        // Combine cities from breweries and restaurants for the selected state
        AvailableCities = SearchResults
            .Where(b => GetStateNameByAbbreviation(b.State) == State)
            .Select(b => b.City)
            .Concat(Restaurants
                .Where(r => GetStateNameByAbbreviation(r.Province) == State)
                .Select(r => r.City))
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }
    else
    {
        // If no state selected, show all unique cities from both sources
        AvailableCities = SearchResults
            .Select(b => b.City)
            .Concat(Restaurants.Select(r => r.City))
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }
}

        private async Task LoadDataAsync()
        {
            await Task.WhenAll(
                LoadBreweriesAsync(),
                LoadRestaurantsAsync(),
                LoadUSStatesAsync()
            );

            // Apply filters after loading
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            // Filter Breweries
            var breweryQuery = SearchResults.AsEnumerable();

            // State filter
            if (!string.IsNullOrEmpty(State))
            {
                breweryQuery = breweryQuery.Where(b =>
                    GetStateNameByAbbreviation(b.State) == State);
            }

            // City filter
            if (!string.IsNullOrEmpty(City))
            {
                breweryQuery = breweryQuery.Where(b =>
                    b.City.Equals(City, StringComparison.OrdinalIgnoreCase));
            }

            // Brewery Type filter
            if (!string.IsNullOrEmpty(BreweryType))
            {
                breweryQuery = breweryQuery.Where(b =>
                    b.BreweryType.Equals(BreweryType, StringComparison.OrdinalIgnoreCase));
            }

            // Search Term filter
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                breweryQuery = breweryQuery.Where(b =>
                    b.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    b.City.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            SearchResults = breweryQuery.ToList();

            // Filter Restaurants
            var restaurantQuery = Restaurants.AsEnumerable();

            // State filter
            if (!string.IsNullOrEmpty(State))
            {
                restaurantQuery = restaurantQuery.Where(r =>
                    GetStateNameByAbbreviation(r.Province) == State);
            }

            // City filter
            if (!string.IsNullOrEmpty(City))
            {
                restaurantQuery = restaurantQuery.Where(r =>
                    r.City.Equals(City, StringComparison.OrdinalIgnoreCase));
            }

            // Search Term filter
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                restaurantQuery = restaurantQuery.Where(r =>
                    r.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            Restaurants = restaurantQuery.ToList();
        }

        private async Task LoadBreweriesAsync()
        {
            var client = _clientFactory.CreateClient();
            try
            {
                string breweryApiUrl = "https://api.openbrewerydb.org/v1/breweries";
                var response = await client.GetAsync(breweryApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var breweries = JsonConvert.DeserializeObject<List<Brewery>>(jsonString) ?? new List<Brewery>();

                    // Enrich brewery data with state names
                    foreach (var brewery in breweries)
                    {
                        brewery.State = GetStateNameByAbbreviation(brewery.State);
                    }

                    SearchResults = breweries;
                }
                else
                {
                    _logger.LogWarning($"Failed to load breweries. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading breweries");
                throw;
            }
        }

        private async Task LoadRestaurantsAsync()
        {
            var client = _clientFactory.CreateClient();
            try
            {
                string restaurantDataUrl = "https://raw.githubusercontent.com/turannu/IS-7012-NT/refs/heads/main/FastFoodRestaurants2.json";
                var response = await client.GetAsync(restaurantDataUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Restaurants = JsonConvert.DeserializeObject<List<Restaurant.RestaurantUsa>>(jsonString) ?? new List<Restaurant.RestaurantUsa>();
                }
                else
                {
                    _logger.LogWarning($"Failed to load restaurants. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading restaurants");
                throw;
            }
        }

        private async Task LoadUSStatesAsync()
        {
            var client = _clientFactory.CreateClient();
            try
            {
                string statesDataUrl = "https://worldpopulationreview.com/static/states/abbr-name-list.json";
                var response = await client.GetAsync(statesDataUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    USStates = JsonConvert.DeserializeObject<List<USAState.UsState>>(jsonString) ?? new List<USAState.UsState>();
                }
                else
                {
                    _logger.LogWarning($"Failed to load US states. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading US states");
                throw;
            }
        }

        public string GetStateNameByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrEmpty(abbreviation)) return abbreviation;

            var state = USStates.FirstOrDefault(s =>
                s.Abbreviation.Equals(abbreviation, StringComparison.OrdinalIgnoreCase));
            return state?.Name ?? abbreviation;
        }
    }
}
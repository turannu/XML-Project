using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace XML_Project.Pages
{
    public class RestaurantModel : PageModel
    {
        private readonly ILogger<RestaurantModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty(SupportsGet = true)]
        public string State { get; set; }

        [BindProperty(SupportsGet = true)]
        public string City { get; set; }

        public List<RestaurantUsa> SearchResults { get; set; } = new List<RestaurantUsa>();

        public List<string> AvailableStates { get; set; } = new List<string>();
        public List<string> AvailableCities { get; set; } = new List<string>();

        private List<RestaurantUsa> AllRestaurants { get; set; } = new List<RestaurantUsa>();

        public RestaurantModel(ILogger<RestaurantModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            // Fetch all restaurants
            string apiUrl = "https://raw.githubusercontent.com/turannu/IS-7012-NT/refs/heads/main/FastFoodRestaurants2.json";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    // Use the FromJson method from your RestaurantUsa class
                    AllRestaurants = RestaurantUsa.FromJson(jsonString).ToList();

                    // Populate available states
                    AvailableStates = AllRestaurants
                        .Select(r => r.Province)
                        .Distinct()
                        .OrderBy(s => s)
                        .ToList();

                    // Determine available cities based on selected state
                    AvailableCities = string.IsNullOrEmpty(State)
                        ? AllRestaurants.Select(r => r.City).Distinct().OrderBy(c => c).ToList()
                        : AllRestaurants
                            .Where(r => r.Province == State)
                            .Select(r => r.City)
                            .Distinct()
                            .OrderBy(c => c)
                            .ToList();

                    // Apply filtering
                    SearchResults = AllRestaurants;

                    // Filter by State if provided
                    if (!string.IsNullOrEmpty(State))
                    {
                        SearchResults = SearchResults
                            .Where(r => r.Province.Equals(State, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    // Filter by City if provided
                    if (!string.IsNullOrEmpty(City))
                    {
                        SearchResults = SearchResults
                            .Where(r => r.City.Equals(City, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    // Add Google Maps URL for each restaurant
                    foreach (var restaurant in SearchResults)
                    {
                        string address = $"{restaurant.Address}, {restaurant.City}, {restaurant.Province}, {restaurant.Country}";
                        restaurant.MapUrl = $"https://www.google.com/maps?q={Uri.EscapeDataString(address)}";
                    }
                }
                else
                {
                    _logger.LogError($"API request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching restaurant data: {ex.Message}");
            }
        }
    }
}
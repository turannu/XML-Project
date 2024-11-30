using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant; // Assuming this namespace contains the RestaurantUsa class
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace XML_Project.Pages
{
    public class RestaurantModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<RestaurantModel> _logger;

        public List<RestaurantUsa> Restaurants { get; set; }

        public RestaurantModel(ILogger<RestaurantModel> logger)
        {
            _logger = logger;
            Restaurants = new List<RestaurantUsa>();
        }

        public async Task OnGetAsync()
        {
            // Fetch data from Restaurant API
            Restaurants = await GetRestaurantDataFromApiAsync();

            // Add a simple Google Maps search URL for each restaurant
            foreach (var restaurant in Restaurants)
            {
                string address = $"{restaurant.Address}, {restaurant.City}, {restaurant.Province}, {restaurant.Country}";
                // Create a general Google Maps search link
                restaurant.MapUrl = $"https://www.google.com/maps?q={Uri.EscapeDataString(address)}";
            }
        }

        // Fetch restaurant data from your external API
        private async Task<List<RestaurantUsa>> GetRestaurantDataFromApiAsync()
        {
            // Replace with the actual API endpoint URL for your restaurant data
            string apiUrl = "https://raw.githubusercontent.com/turannu/IS-7012-NT/refs/heads/main/FastFoodRestaurants2.json";  // Change this to your API URL

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize the JSON into RestaurantUsa objects
                    var restaurants = RestaurantUsa.FromJson(jsonResponse);
                    return new List<RestaurantUsa>(restaurants);
                }
                else
                {
                    // Return an empty list if the API request fails
                    return new List<RestaurantUsa>();
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                _logger.LogError($"Error fetching restaurant data: {ex.Message}");
                // Return an empty list in case of an exception
                return new List<RestaurantUsa>();
            }
        }
    }
}



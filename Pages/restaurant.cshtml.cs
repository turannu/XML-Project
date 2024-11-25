using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant;
using System.Net.Http;
using System.Collections.Generic;

namespace XML_Project.Pages
{
    public class restaurantModel : PageModel
    {
        private readonly ILogger<restaurantModel> _logger;
        private readonly HttpClient _httpClient = new HttpClient();

        // Add property for Restaurants
        public List<RestaurantUsa> Restaurants { get; set; }

        public restaurantModel(ILogger<restaurantModel> logger)  // Fixed logger type
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Task<HttpResponseMessage> task = _httpClient.GetAsync("https://raw.githubusercontent.com/turannu/IS-7012-NT/refs/heads/main/FastFoodRestaurants2.json");
                HttpResponseMessage result = task.Result;
                List<RestaurantUsa> restaurants = new List<RestaurantUsa>();

                if (result.IsSuccessStatusCode)
                {
                    Task<string> readString = result.Content.ReadAsStringAsync();
                    string restaurantJSON = readString.Result;
                    RestaurantUsa[] restaurantsArray = RestaurantUsa.FromJson(restaurantJSON);
                    restaurants = new List<RestaurantUsa>(restaurantsArray);
                }
                else
                {
                    ModelState.AddModelError("", "Failed to fetch restaurant data");
                }

                Restaurants = restaurants;  // Use property instead of ViewData
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                Restaurants = new List<RestaurantUsa>();
            }
        }
    }
}
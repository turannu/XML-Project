using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace XML_Project.Pages
{
    public class ReviewFormModel : PageModel
    {
        private readonly ILogger<ReviewFormModel> _logger;

        public ReviewFormModel(ILogger<ReviewFormModel> logger)
        {
            _logger = logger;
        }

        public List<FormData> ReviewFormData { get; private set; }

        public void OnGet()
        {
            ReviewFormData = GetReviewFormData();
        }

        private List<FormData> GetReviewFormData()
        {
            // Specify the path to your JSON file
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ReviewForm_data.json");

            // Read JSON data from the file
            return System.IO.File.Exists(jsonFilePath)
                ? JsonConvert.DeserializeObject<List<FormData>>(System.IO.File.ReadAllText(jsonFilePath))
                : new List<FormData>();
        }

        public IActionResult OnPost()
        {
            // Retrieve form data from the request
            var formData = new FormData
            {
                FirstName = Request.Form["FirstName"],
                LastName = Request.Form["LastName"],
                Email = Request.Form["email"],
                RestaurantName = Request.Form["RestaurantName"],
                BreweryName = Request.Form["BreweryName"],
                Rating = int.Parse(Request.Form["rating"]),
                Review = Request.Form["review"]
            };

            // Save form data to a JSON file
            SaveToJsonFile(formData);

            return RedirectToPage("/ReviewForm");
        }

        private void SaveToJsonFile(FormData formData)
        {
            // Specify the path to your JSON file
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ReviewForm_data.json");

            // Read existing JSON data, or create an empty list if the file doesn't exist
            List<FormData> existingData = System.IO.File.Exists(jsonFilePath)
                ? JsonConvert.DeserializeObject<List<FormData>>(System.IO.File.ReadAllText(jsonFilePath))
                : new List<FormData>();

            // Add the new form data to the list
            existingData.Add(formData);

            // Write the updated list back to the JSON file
            System.IO.File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(existingData, Formatting.Indented));
        }
    }
}

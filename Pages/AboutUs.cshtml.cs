using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace XML_Project.Pages
{
    public class AboutUsModel : PageModel
    {
        private readonly ILogger<AboutUsModel> _logger;

        public AboutUsModel(ILogger<AboutUsModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public ContactFormData ContactForm { get; set; }

        public void OnGet()
        {
            // Initialize the model
            ContactForm = new ContactFormData();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Set submission time
                ContactForm.SubmissionTime = DateTime.Now;

                // Save contact form data to a separate JSON file
                SaveContactToJsonFile(ContactForm);

                // Add a success message
                TempData["ContactSubmissionMessage"] = "Thank you for reaching out! We'll get back to you soon.";

                // Redirect to prevent form resubmission
                return RedirectToPage("/AboutUs");
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error saving contact form data");

                // Add an error message for the user
                ModelState.AddModelError("", "An error occurred while submitting the form. Please try again.");
                return Page();
            }
        }

        private void SaveContactToJsonFile(ContactFormData contactData)
        {
            // Specify the path to your JSON file for contacts
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ContactForm_data.json");

            // Read existing JSON data, or create an empty list if the file doesn't exist
            List<ContactFormData> existingData = System.IO.File.Exists(jsonFilePath)
                ? JsonConvert.DeserializeObject<List<ContactFormData>>(System.IO.File.ReadAllText(jsonFilePath))
                : new List<ContactFormData>();

            // Add the new contact form data to the list
            existingData.Add(contactData);

            // Write the updated list back to the JSON file
            System.IO.File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(existingData, Formatting.Indented));
        }
    }
}
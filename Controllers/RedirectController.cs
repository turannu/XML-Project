using Microsoft.AspNetCore.Mvc;

namespace XML_Project.Controllers
{
    public class RedirectController : Controller
    {
        // This method will handle the redirection based on the 'type' parameter
        [HttpGet("/RedirectHandler")]
        public IActionResult RedirectHandler(string type)
        {
            return type switch
            {
                "brewery" => Redirect("https://localhost:5001/Brewery"),  // Redirect to the brewery page
                "restaurant" => Redirect("https://localhost:5001/Restaurant"),  // Redirect to the restaurant page
                "both" => Redirect("https://localhost:5001/Search"),  // Redirect to the both page
                _ => Redirect("/")  // Default to home if no valid type is given
            };
        }
    }
}

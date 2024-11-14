using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SipsandBites;
namespace XML_Project.Pages
{
    public class IndexModel : PageModel
    {
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
            
            

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace XML_Project.Pages
{
    public class ReviewModel : PageModel
    {
        // Simulate a database with an in-memory list
        private static List<Review> Reviews = new List<Review>();

        public List<Review> AllReviews { get; set; } = new List<Review>();

        [BindProperty]
        public Review NewReview { get; set; }

        public void OnGet()
        {
            // Load all reviews
            AllReviews = Reviews;
        }

        public IActionResult OnPostSubmitReview()
        {
            if (NewReview != null)
            {
                Reviews.Add(NewReview);
            }
            return RedirectToPage();
        }
    }
}

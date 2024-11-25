using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using XML_Project.Data;
using XML_Project.Model;

namespace XML_Project.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly XML_Project.Data.XML_ProjectContext _context;

        public IndexModel(XML_Project.Data.XML_ProjectContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Review = await _context.Review.ToListAsync();
        }
    }
}

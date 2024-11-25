using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XML_Project.Model;

namespace XML_Project.Data
{
    public class XML_ProjectContext : DbContext
    {
        public XML_ProjectContext (DbContextOptions<XML_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<XML_Project.Model.Review> Review { get; set; } = default!;
    }
}

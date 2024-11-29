using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace XML_Project.Data
{
    public class XML_ProjectContext : DbContext
    {
        public XML_ProjectContext (DbContextOptions<XML_ProjectContext> options)
            : base(options)
        {
        }

        
    }
}

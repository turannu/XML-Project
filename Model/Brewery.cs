namespace XML_Project.Model
{
    public class Brewery
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Brewery Name
        public string BreweryType { get; set; } // e.g., "micro", "large"
        public string Address { get; set; } // Street Address
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string WebsiteUrl { get; set; }

   
    }
}

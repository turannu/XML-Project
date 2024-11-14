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

        // Foreign Keys
        public int StateId { get; set; } // Links to State
        public state? State { get; set; } // Navigation Property

        // Optional: Additional Location Info from Google API
        public int? GoogleApiDataId { get; set; }
        public GoogleApiData GoogleApiData { get; set; } // One-to-One Relationship with Google API Data
    }
}

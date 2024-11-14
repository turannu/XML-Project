namespace XML_Project.Model
{
    public class GoogleApiData
    {
        public int Id { get; set; } // Primary Key
        public int? BreweryId { get; set; } // Links to Brewery
        public Brewery Brewery { get; set; } // Navigation Property

        // Additional Fields from Google API
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Rating { get; set; } // Optional Rating from Google Places
        public int? UserRatingsTotal { get; set; } // Total number of user ratings
        public string? PlaceId { get; set; } // Unique Place ID from Google API for future reference
    }
}

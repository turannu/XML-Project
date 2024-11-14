namespace XML_Project.Model
{
    public class state
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; } // e.g., "CA"

        // Navigation Property
        public ICollection<Brewery> Breweries { get; set; } // One-to-Many Relationship with Breweries
    }
}

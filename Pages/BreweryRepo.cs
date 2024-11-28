using SipsBites;

namespace XML_Project.Pages
{
    public class BreweryRepo
    {
        static BreweryRepo()
        {
            allBreweries = new List<Brewery>();
        }

        public static IList<Brewery> allBreweries { get; set; }
    }
}
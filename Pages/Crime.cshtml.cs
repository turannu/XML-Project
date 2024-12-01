using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Crime;

namespace XML_Project.Pages
{
    public class CrimeDataModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CrimeDataModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty(SupportsGet = true)]
        public string SelectedZip { get; set; }

        public List<CrimeUsa> CrimeData { get; set; } = new List<CrimeUsa>();

        public IEnumerable<(string Offense, int Count)> CrimeDataByOffense { get; private set; }

        public IEnumerable<(string Zip, int Count)> CrimeDataByZip { get; private set; }

        public async Task OnGetAsync()
        {
            // Base URL and parameters
            string baseUrl = "https://data.cincinnati-oh.gov/resource/k59e-2pvf.json";
            string limit = "1000";
            string offset = "0";
            string whereClause = "date_reported >= '2022-11-30T00:00:00'";

            // Filter by zip code if selected
            if (!string.IsNullOrEmpty(SelectedZip))
            {
                whereClause += $" AND CAST(zip AS TEXT) = '{SelectedZip}'";
            }

            string url = $"{baseUrl}?$limit={limit}&$offset={offset}&$where={whereClause}";
            var client = _clientFactory.CreateClient();
            var response = await client.GetStringAsync(url);

            // Deserialize the JSON data into CrimeUsa objects
            CrimeData = JsonConvert.DeserializeObject<List<CrimeUsa>>(response);

            // Group crime data by offense and count
            CrimeDataByOffense = CrimeData.GroupBy(c => c.Offense)
                                         .Select(g => (g.Key, g.Count()));

            // Group crime data by zip code and count, then order by count descending
            CrimeDataByZip = CrimeData.GroupBy(c => c.Zip.ToString())
                                      .Select(g => (g.Key, g.Count()))
                                      .OrderByDescending<(string, int), int>(c => c.Item2);
        }
    }
}

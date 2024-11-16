using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace XML_Project.UsStatesAPI
{
    public class UsStateService
    {

        public static async Task<List<UsState>> FetchUsStates()
        {
            var statesApiUrl = "https://worldpopulationreview.com/static/states/abbr-name-list.json";


            var apiClient = new HttpClient();

            try
            {
                // Make API Call


                HttpResponseMessage response = await apiClient.GetAsync(statesApiUrl);


                if (response.IsSuccessStatusCode)
                {
                    //Convert JSON Response to List
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    List<UsState> usStateResponse = JsonConvert.DeserializeObject<List<UsState>>(jsonResponse);

                    return usStateResponse;

                }
                else
                {
                    return new List<UsState>();

                }

            }
            catch (Exception ex)
            {
                return new List<UsState>();
            }
        }
    }

    public class UsState
    {
        [JsonPropertyName("name")]
        public string? name;
        [JsonPropertyName("abbreviation")]
        public string? abbreviation;
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordCraft.Models;

namespace WordCraft.ViewModels
{
    public partial class HistoryPageViewModel : PageViewModel
    {
        // Define list of PreviousWordModel objects.
        public List<PreviousWordModel> PreviousWords { get; set; }

        public HistoryPageViewModel()
        {
            GetApiData(); // Fetch API data on each initalisation.
        }

        private void GetApiData()
        {
            using (var client = new HttpClient())
            {
                var uri_endpoint = new Uri("https://testapi.sail-dev.com/api/data/getworddata"); // Define endpoint.
                var result = client.GetAsync(uri_endpoint).Result; // Asynchronous GET operation on endpoint.
                var json = result.Content.ReadAsStringAsync().Result; // Content.ReadAsStringAsync.Result on result obtains data in JSON format.

                PreviousWords = JsonConvert.DeserializeObject<List<PreviousWordModel>>(json); // Convert JSON data to list of type PreviousWordModel.

            }
        }
    }
}

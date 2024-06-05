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
        public List<PreviousWordModel> PreviousWords { get; set; }

        public HistoryPageViewModel()
        {
            GetApiData();
        }

        private void GetApiData()
        {
            using (var client = new HttpClient())
            {
                var uri_endpoint = new Uri("https://testapi.sail-dev.com/api/data/getworddata");
                var result = client.GetAsync(uri_endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                PreviousWords = JsonConvert.DeserializeObject<List<PreviousWordModel>>(json);

            }
        }
    }
}

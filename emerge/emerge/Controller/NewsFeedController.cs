using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using emerge.Data;
using Newtonsoft.Json;
using static MudBlazor.FilterOperator;
namespace emerge.Controller
{
    
    public class NewsFeedController
	{
		private List<NewsAlert> alertList;

        public NewsFeedController()
		{
            alertList = new List<NewsAlert>();
            populateFeed();
        }

        //populates alert list with recent alerts by calling api endpoint
        private async void populateFeed()
        {
            string url = "https://emergeproj.azurewebsites.net/api/newsalert?";
            using var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var response = client.GetAsync("newsalert").Result;
            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                alertList = JsonConvert.DeserializeObject<List<NewsAlert>>(responseAsString);
                sortAlertList();        //sorts alert list based date times
                Console.Write("responesAsString"); //unserialized json string
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                              response.ReasonPhrase);
            }
        }

        public List<NewsAlert> getAlertList()
        {
            return alertList;
        }

        //sorts list of alerts according to alert's date/time
        private void sortAlertList()
        {
            alertList.Sort((x, y) => System.DateTime.Compare(x.Time, y.Time));

        }


    }


}


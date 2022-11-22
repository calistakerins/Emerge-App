using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using emerge.Data;
using static MudBlazor.FilterOperator;
namespace emerge.Controller
{
    
    public class NewsFeedController
	{
		private List<NewsAlert> alertList;

        //dummy data
        //private static readonly UpdateInfo[] updates = {
        //new UpdateInfo(new System.DateTime(2019, 05, 09, 09, 37, 07), " leo, ut laoreet leo blandit feugiat."),
        //new UpdateInfo(new System.DateTime(2019, 05, 09, 10, 56, 41), "laoreet turpis leo, ut laoreet leo blandit feugiat.")};

        //populates alertList
        public NewsFeedController()
		{
            alertList = new List<NewsAlert>();
            //AlertInfo dummyAlert1 = new AlertInfo(new System.DateTime(2019, 05, 09, 09, 15, 00),
            //                                                     "FLASH FLOOD EVACUATION", "harrison",
            //                                                     "Department of Energy", 5,
            //                                                       updates, "https://th.bing.com/th/id/R.543239a7b5829806a4df4b85d929e090?rik=CcAmGH8I0TMVXw&pid=ImgRaw&r=0");
            //AlertInfo dummyAlert2 = new AlertInfo(new System.DateTime(2009, 05, 09, 10, 12, 02),
            //                                                     "BUILDING FIRE", "megan",
            //                                                     "Department of Forestry and Fire", 4,
            //                                                       updates, "https://www.dailynews.com/wp-content/uploads/2020/07/LDN-L-SOLEDAD-FIRE-RMC-07.jpg?w=810");
            //AlertInfo dummyAlert3 = new AlertInfo(new System.DateTime(2019, 05, 09, 012, 02, 00),
            //                                                                 "ARMED MAN LOOSE", "ella",
            //                                                                 "Department of Police", 3,
            //                                                                   updates, "https://th.bing.com/th/id/R.5be2e3e4d97cd5fefd81ee070b5bfd54?rik=BohjSTqB2DslYg&riu=http%3a%2f%2fcdn1.bostonmagazine.com%2fwp-content%2fuploads%2f2016%2f09%2fpolice-lights-1.jpg&ehk=TI%2fwUezR3XQhinPY4zJeDWtWYk9GqrC80Qu%2bscqGD%2fk%3d&risl=&pid=ImgRaw&r=0");
            //AlertInfo dummyAlert4 = new AlertInfo(new System.DateTime(2019, 05, 09, 013, 15, 04),
            //                                                                 "ELECTION UPDATE", "calista",
            //                                                                 "Town Council", 2,
            //                                                                   updates, "https://th.bing.com/th/id/OIP.z6fjBW0cSvSBZ3Nu9QITDwHaEF?pid=ImgDet&rs=1");

            //this.alertList.Add(dummyAlert1);
            //this.alertList.Add(dummyAlert2);
            //this.alertList.Add(dummyAlert3);
            //this.alertList.Add(dummyAlert4);
            populateFeed();

        }

        private async void populateFeed()
        {
            string url = "https://emerge-app.azurewebsites.net/api/newsalert?";
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

        public List<NewsAlert> getFirstNAlerts(int n)
        {
            this.sortAlertList();
            List<NewsAlert> nList = new List<NewsAlert>();
            if (alertList.Count <= n)
            {
                return alertList;
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    nList.Add(alertList[i]);
                }
                return nList;
            }

        }

        public List<NewsAlert> getRemainingAlerts(int n)
        {
            this.sortAlertList();
            List<NewsAlert> remainingList = new List<NewsAlert>();
            int count = alertList.Count;
            for (int i = n; i < count; i++)
            {
                remainingList.Add(alertList[i]);
            }
            return remainingList;
        }


    }


}


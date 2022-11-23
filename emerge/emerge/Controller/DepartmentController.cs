using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using emerge.Data;
using static MudBlazor.FilterOperator;
namespace emerge.Controller
{

    public class DepartmentController
    {

        private List<Department> deptList;
        //dummy data
        private static readonly UpdateInfo[] updates;
        //new UpdateInfo(new System.DateTime(2019, 05, 09, 09, 37, 07), " leo, ut laoreet leo blandit feugiat."),
        //new UpdateInfo(new System.DateTime(2019, 05, 09, 10, 56, 41), "laoreet turpis leo, ut laoreet leo blandit feugiat.")};

        //populates alertList
        public DepartmentController()
        {
            //AlertInfo dummyAlert1 = new AlertInfo(new System.DateTime(2019, 05, 09, 09, 15, 00),
            //                                                    "FLASH FLOOD EVACUATION", "harrison",
            //                                                    "Department of Energy", 5,
            //                                                      updates, "https://th.bing.com/th/id/R.543239a7b5829806a4df4b85d929e090?rik=CcAmGH8I0TMVXw&pid=ImgRaw&r=0");

            //List<AlertInfo> deptAlerts1 = new List<AlertInfo>();
            //deptAlerts1.Add(dummyAlert1);

            //AlertInfo dummyAlert2 = new AlertInfo(new System.DateTime(2009, 05, 09, 10, 12, 02),
            //                                                     "BUILDING FIRE", "megan",
            //                                                     "Department of Forestry and Fire", 4,
            //                                                       updates, "https://www.dailynews.com/wp-content/uploads/2020/07/LDN-L-SOLEDAD-FIRE-RMC-07.jpg?w=810");

            //List<AlertInfo> deptAlerts2 = new List<AlertInfo>();
            //deptAlerts2.Add(dummyAlert2);


            //AlertInfo dummyAlert3 = new AlertInfo(new System.DateTime(2019, 05, 09, 012, 02, 00),
            //                                                                 "ARMED MAN LOOSE", "ella",
            //                                                                 "Department of Police", 3,
            //                                                                   updates, "https://th.bing.com/th/id/R.5be2e3e4d97cd5fefd81ee070b5bfd54?rik=BohjSTqB2DslYg&riu=http%3a%2f%2fcdn1.bostonmagazine.com%2fwp-content%2fuploads%2f2016%2f09%2fpolice-lights-1.jpg&ehk=TI%2fwUezR3XQhinPY4zJeDWtWYk9GqrC80Qu%2bscqGD%2fk%3d&risl=&pid=ImgRaw&r=0");

            //List<AlertInfo> deptAlerts3 = new List<AlertInfo>();
            //deptAlerts3.Add(dummyAlert3);

            //Department department1 = new Department("Department of Energy",
            //                                        "Harrison",
            //                                        "The home for all energy generation and distibution related news",
            //                                        deptAlerts1,
            //                                        "images/transmission_tower_worker.jpeg");

            //Department department2 = new Department("Fire Department",
            //                                        "Calista",
            //                                        "Fire Safety and Emergency news",
            //                                        deptAlerts2,
            //                                        "images/pngtree-red-fire-station-house-png-image_5402814.jpg");

            //Department department3 = new Department("Transit Department",
            //                                        "Megan",
            //                                        "News for transit",
            //                                        deptAlerts3,
            //                                        "images/imgbin-rail-transport-united-states-department-of-transportation-management-logistics-others-CFf3SHtBT2sAgexBg2Bp6wPST.jpg");


            populateDepts();

        }

        private async void populateDepts()
        {
            string url = "http://localhost:7071/api/";
            using var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var response = client.GetAsync("deptData").Result; // need to ask group about this one
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

    }
}


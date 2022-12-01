using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using emerge.Data;
using static MudBlazor.FilterOperator;
namespace emerge.Controller
{

    public class DepartmentController
    {

        private List<Department> deptList;
        public DepartmentController()
        {
            deptList = new List<Department>();
            populateDepts();
        }

        private async void populateDepts()
        {
            string url = "https://emerge-azure.azurewebsites.net/api/depts?";
            using var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var response = client.GetAsync("depts").Result; // need to ask group about this one
            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                deptList = JsonConvert.DeserializeObject<List<Department>>(responseAsString);
                Console.Write(responseAsString); //unserialized json string
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                              response.ReasonPhrase);
            }
        }

        public List<Department> getDeptList()
        {
            return deptList;
        }

    }
}
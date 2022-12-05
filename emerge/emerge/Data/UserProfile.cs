using System;
using Newtonsoft.Json;

namespace emerge.Data
{
	public class UserProfile
	{
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "admin")]
        public bool Admin { get; set; }
        [JsonProperty(PropertyName = "zipcode")]
        public string Zipcode { get; set; }
        [JsonProperty(PropertyName = "alerts")]
        public List<NewsAlert> Alerts { get; set; }
        [JsonProperty(PropertyName = "following")]
        public List<Department> Following { get; set; }



        //returns true if the parameter department is followed 
        public Boolean sameDepartment(Department department)
        {
            if(department == null)
            {
                return false;
            } else
            {
                foreach (Department userDepartment in Following)
                {
                    if (department.Id.Equals(userDepartment.Id))
                    {
                        return true;
                    }
                }
                return false;
            }
            
           
        }

        public Boolean sameZip(List<string> affected)
        {
            if(affected == null)
            {
                return false;
            } else
            {
                foreach(String zip in affected)
                {
                    if(Zipcode.Equals(zip))
                    {
                        return true;
                    }
                }
                return false;
            }
        }


    }
}


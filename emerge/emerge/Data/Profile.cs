using System;
namespace emerge.Data
{
	public class Profile
	{
        private String Name;
        private Boolean Administrator;
		private String Username;
		private String Password;
		private String Email;
		private String Location;
		private Boolean LoggedIn;

        public Profile(String name, String username, String email, String password, String location, Boolean admin)
		{
			this.Name = name;
			this.Administrator = admin;
			this.Username = username;
			this.Password = password;
			this.Email = email;
			this.Location = location;
		}

		public void setLogin(Boolean loggedIn)
		{
			this.LoggedIn = loggedIn;
		}
	}
}


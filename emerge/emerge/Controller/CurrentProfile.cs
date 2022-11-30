using System;
using emerge.Data;

namespace emerge.Controller
{
	public class CurrentProfile
	{
		public static UserProfile currentUser;
		public bool loggedIn;

		public CurrentProfile()
		{
			currentUser = new UserProfile();
            currentUser.Name = "Jane";
            loggedIn = false;
		}

		public void login(UserProfile user)
		{
			currentUser = user;
			loggedIn = true;
		}

        public void logOut(UserProfile user)
        {
            currentUser = new UserProfile();
			currentUser.Name = "Jane";
			loggedIn = false;
        }
    }
}


using System;
using emerge.Data;

namespace emerge.Controller
{
	public class CurrentProfile
	{
		public static UserProfile currentUser { get; set; }

		public static bool loggedIn { get; set; }
    }
}


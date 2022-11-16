using System;
using emerge.Data;
using static MudBlazor.FilterOperator;

namespace emerge.Controller
{
    
    public class NewsFeedController
	{
		private List<AlertInfo> alertList;

        //dummy data
        private static readonly UpdateInfo[] updates = {
        new UpdateInfo(new System.DateTime(2019, 05, 09, 09, 37, 07), " leo, ut laoreet leo blandit feugiat."),
        new UpdateInfo(new System.DateTime(2019, 05, 09, 10, 56, 41), "laoreet turpis leo, ut laoreet leo blandit feugiat.")};

        //populates alertList
        public NewsFeedController()
		{
            alertList = new List<AlertInfo>();
            AlertInfo dummyAlert1 = new AlertInfo(new System.DateTime(2019, 05, 09, 09, 15, 00),
                                                                 "FLASH FLOOD EVACUATION", "harrison",
                                                                 "Department of Energy", 5,
                                                                   updates);
            AlertInfo dummyAlert2 = new AlertInfo(new System.DateTime(2009, 05, 09, 10, 12, 02),
                                                                 "BUILDING FIRE", "megan",
                                                                 "Department of Forestry and Fire", 4,
                                                                   updates);
            AlertInfo dummyAlert3 = new AlertInfo(new System.DateTime(2019, 05, 09, 012, 02, 00),
                                                                             "ARMED MAN LOOSE", "ella",
                                                                             "Department of Police", 3,
                                                                               updates);
            AlertInfo dummyAlert4 = new AlertInfo(new System.DateTime(2019, 05, 09, 013, 15, 04),
                                                                             "ELECTION UPDATE", "calista",
                                                                             "Town Council", 2,
                                                                               updates);

            this.alertList.Add(dummyAlert1);
            this.alertList.Add(dummyAlert2);
            this.alertList.Add(dummyAlert3);
            this.alertList.Add(dummyAlert4);
        }

        public List<AlertInfo> getAlertList()
        {
            return alertList;
        }


	}
}


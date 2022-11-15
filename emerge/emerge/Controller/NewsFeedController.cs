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
        new UpdateInfo(new System.DateTime(2019, 05, 09, 09, 37, 07), "Integer risus nunc, mattis in ornare sit amet, aliquam quis ligula. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Ut mauris massa, iaculis non augue vitae, mattis tincidunt turpis. In laoreet turpis leo, ut laoreet leo blandit feugiat."),
        new UpdateInfo(new System.DateTime(2019, 05, 09, 10, 56, 41), "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Ut mauris massa, iaculis non augue vitae, mattis tincidunt turpis. In laoreet turpis leo, ut laoreet leo blandit feugiat.")};

        //populates alertList
        public NewsFeedController()
		{
            alertList = new List<AlertInfo>();
            AlertInfo dummyAlert1 = new AlertInfo(new System.DateTime(2019, 05, 09, 09, 15, 00),
                                                                 "FLASH FLOOD EVACUATION",
                                                                 "Department of Energy",
                                                                   updates);
            AlertInfo dummyAlert2 = new AlertInfo(new System.DateTime(2009, 05, 09, 10, 12, 02),
                                                                 "BUILDING FIRE",
                                                                 "Department of Forestry and Fire",
                                                                   updates);
            AlertInfo dummyAlert3 = new AlertInfo(new System.DateTime(2019, 05, 09, 012, 02, 00),
                                                                             "ARMED MAN LOOSE",
                                                                             "Department of Police",
                                                                               updates);
            AlertInfo dummyAlert4 = new AlertInfo(new System.DateTime(2019, 05, 09, 013, 15, 04),
                                                                             "ELECTION UPDATE",
                                                                             "Town Council",
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


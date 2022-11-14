using System;
using emerge.Data;
using static MudBlazor.FilterOperator;

namespace emerge.Controller
{
    
    public class NewsFeedController
	{
		private List<AlertInfo> alertList;

        //dummy data
        private static readonly string title = "FLASH FLOOD EVACUATION";
        private static readonly string author = "TOWN ADMIN";
        private static readonly System.DateTime dateTime = new System.DateTime(2019, 05, 09, 09, 15, 00);
        private static readonly UpdateInfo[] updates = {
        new UpdateInfo(new System.DateTime(2019, 05, 09, 09, 37, 07), "Integer risus nunc, mattis in ornare sit amet, aliquam quis ligula. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Ut mauris massa, iaculis non augue vitae, mattis tincidunt turpis. In laoreet turpis leo, ut laoreet leo blandit feugiat."),
        new UpdateInfo(new System.DateTime(2019, 05, 09, 10, 56, 41), "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Ut mauris massa, iaculis non augue vitae, mattis tincidunt turpis. In laoreet turpis leo, ut laoreet leo blandit feugiat.")};

        //populates alertList
        public NewsFeedController()
		{
            alertList = new List<AlertInfo>();
            AlertInfo dummyAlert = new AlertInfo(dateTime, title, author, updates);
            this.addAlert(dummyAlert);
        }

        public List<AlertInfo> getAlertList()
        {
            return alertList;
        }

        public void addAlert(AlertInfo alertInfo)
        {
            alertList.Add(alertInfo);
        }

	}
}


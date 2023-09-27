using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UserLogin
{
    public static class Logger
    {
        private static List<string> currentSessionActivities = new List<string>();

        public static string DescribeActivity(Activities activity)
        {
            String description;
            switch (activity)
            {
                case Activities.USER_LOGED:
                    description = "User has logged.";
                    break;
                case Activities.USER_ACTIVE_UNTIL_CHANGED:
                    description = "User active until has changed.";
                    break;
                case Activities.USER_ROLE_CHANGED:
                    description = "User role changed.";
                    break;
                default:
                    description = "Invalid activity!";
                    break;
            }
            return description;
        }

        public static void LogActivity(Activities activity)
        {
            string activityLine = DateTime.Now + ";"
                                    + LoginValidation.username + ";"
                                    + LoginValidation.currentUserRole + ";"
                                    + DescribeActivity(activity);
            currentSessionActivities.Add(activityLine);

            if (File.Exists("../../text.txt") == true)
            {
                File.AppendAllText("../../text.txt", activityLine);
            }
        }

        public static IEnumerable<string> OuputlogFile()
        {
            return File.ReadAllLines("../../text.txt");
        }

        public static IEnumerable<string> GetCurrentSessionActivities(string filter)
        {
            List<string> filteredActivities = (from activity in currentSessionActivities where activity.Contains(filter) select activity).ToList();

            return filteredActivities;
        }
    }
}

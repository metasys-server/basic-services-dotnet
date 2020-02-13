using JohnsonControls.Metasys.BasicServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetasysServicesExampleApp.FeaturesDemo
{
    public class TrendsDemo
    {
        private MetasysClient client;
        public TrendsDemo(MetasysClient client)
        {
            this.client = client;
        }
        public void Run()
        {
            #region Trend
            try {
                TimeFilter getDateTimeForTrend = new TimeFilter();
                Console.WriteLine("\nIndicate the object you want to run this example code on.");
                Console.Write("Enter the fully qualified reference of the object (Example: \"site:device/itemReference\"): ");
                string object1 = Console.ReadLine();
                Guid id1 = client.GetObjectIdentifier(object1);
                Console.WriteLine($"{object1} id: {id1}");
                Guid objId = id1;
                var trendedAttributes = client.Trends.GetTrendedAttributes(objId);
                Console.WriteLine(trendedAttributes[0].Description);

                Console.WriteLine("Please enter Start Date and End Date separated by space: ");
                string getDateTime = Console.ReadLine();
                string[] args;
                args = getDateTime.Split(' ');

                if (args != null)
                {
                    getDateTimeForTrend = ReadUserInputForTrends(args);
                }

                var samples = client.Trends.GetSamples(objId, 85, getDateTimeForTrend).Items;
                foreach (var s in samples)
                {
                    Console.WriteLine($"Value: {s.Value} Unit: {s.Unit} Timestamp: {s.Timestamp}");
                }
            }
            catch (Exception) {
                Console.WriteLine("\n \nAn Error occurred. Press Enter to return to Main Menu");
            }

            Console.ReadLine();
            #endregion
        }

        private TimeFilter ReadUserInputForTrends(string[] args)
        {
            TimeFilter timeFilter = new TimeFilter
            {
                StartTime = DateTime.Parse(args[0]),
                EndTime = DateTime.Parse(args[1]),
            };
            return timeFilter;
        }
    }
}

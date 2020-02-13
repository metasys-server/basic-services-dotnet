using System;
using System.Globalization;
using JohnsonControls.Metasys.ComServices;

namespace MetasysServicesComExampleApp.FeaturesDemo
{
    public class IComAlarmsDemo
    {
        private ILegacyMetasysClient legacyClient;

        public IComAlarmsDemo(ILegacyMetasysClient legacyClient)
        {
            this.legacyClient = legacyClient;
        }

        public void Run()
        {
            #region Alarms
            try {
                Console.WriteLine("Enter alarm id to get alarm details: ");
                string alarmId = Console.ReadLine();

                IComProvideAlarmItem alarmItem = (IComProvideAlarmItem)legacyClient.GetSingleAlarm(alarmId);

                Console.WriteLine(string.Format("\n Alarm details found for {0}", alarmId));
                Console.WriteLine($"\n Id: {alarmItem.Id}, Name: {alarmItem.Name}, ItemReference: {alarmItem.ItemReference}");

                string getAlarms;
                ComAlarmFilter getAlarmsFilter = new ComAlarmFilter();

                Console.WriteLine("Please enter these parameters separated by space: Start Time, End Time, Priority range, Type, Exclude pending, Exclude acknowledged, Exclude discarded, Attribute, Category, Page, Page Size, Sort" +
                    "\nRefer to the metasys-server/basic-services-dotnet README if you need help getting started.");
                getAlarms = Console.ReadLine();
                string[] args;
                args = getAlarms.Split(' ');

                if (args != null)
                {
                    getAlarmsFilter = ReadUserInput(args);
                }

                Console.WriteLine("\n List of alarms with details");

                IComPagedResult PagedResult = (IComPagedResult)legacyClient.GetAlarms(getAlarmsFilter);
                IComProvideAlarmItem[] alarmItems = (IComProvideAlarmItem[])PagedResult.Items;

                Console.WriteLine($"\n Total: {PagedResult.Total}");
                Console.WriteLine($"\n Page Count: {PagedResult.PageCount}");
                Console.WriteLine($"\n Page Size: {PagedResult.PageSize}");
                Console.WriteLine($"\n Current Page: {PagedResult.CurrentPage}");

                if (alarmItems != null)
                {
                    foreach (var item in alarmItems)
                    {
                        Console.WriteLine($"\n Id: {item.Id}, Name: {item.Name}, ItemReference: {item.ItemReference}");
                    }
                }
                else
                {
                    Console.WriteLine("No alarm found.");
                }

                Console.WriteLine("\nEnter object id to get alarm details: ");
                string objectId = Console.ReadLine();

                string getAlarmsForObject;
                ComAlarmFilter alarmFilterForObject = new ComAlarmFilter();

                Console.WriteLine("\n Please enter these parameters separated by space: Start Time, End Time, Priority range, Type, Exclude pending, Exclude acknowledged, Exclude discarded, Attribute, Category, Page, Page Size, Sort" +
                        "\nRefer to the metasys-server/basic-services-dotnet README if you need help getting started.");

                getAlarmsForObject = Console.ReadLine();
                args = getAlarmsForObject.Split(' ');

                if (args != null)
                {
                    alarmFilterForObject = ReadUserInput(args);
                }

                Console.WriteLine(string.Format("\nAlarm details found for this object {0}", objectId));

                var alarmItemsForObject = legacyClient.GetAlarmsForAnObject(objectId, alarmFilterForObject);

                Console.WriteLine("\nEnter network device id to get alarm details: ");
                string networkDeviceId = Console.ReadLine();

                string getAlarmsForNetworkDevice;
                ComAlarmFilter alarmFilterModelForNetworkDevice = new ComAlarmFilter();

                Console.WriteLine("\nPlease enter these parameters separated by space: Start Time, End Time, Priority range, Type, Exclude pending, Exclude acknowledged, Exclude discarded, Attribute, Category, Page, Page Size, Sort" +
                    "\nRefer to the metasys-server/basic-services-dotnet README if you need help getting started.");

                getAlarmsForNetworkDevice = Console.ReadLine();
                args = getAlarmsForNetworkDevice.Split(' ');

                if (args == null || args.Length != 12)
                {
                    alarmFilterModelForNetworkDevice = ReadUserInput(args);

                    Console.WriteLine(string.Format("\nAlarm details found for this object {0}", objectId));

                    var alarmItemsForNetworkDevice = legacyClient.GetAlarmsForNetworkDevice(networkDeviceId, alarmFilterModelForNetworkDevice);
                }
                else
                {
                    Console.WriteLine("\nInvalid Input");
                }
            }
            catch (Exception) {
                Console.WriteLine("\n \nAn Error occurred. Press Enter to return to Main Menu");
            }

            Console.ReadLine();
            #endregion
        }

        private static ComAlarmFilter ReadUserInput(string[] args)
        {
            DateTime st = DateTime.Parse(args[0], null, DateTimeStyles.AssumeUniversal);
            DateTime et = DateTime.Parse(args[1], null, DateTimeStyles.AssumeUniversal);

            ComAlarmFilter alarmFilter = new ComAlarmFilter
            {
                StartTime = st.ToString(),
                EndTime = et.ToString(),
                PriorityRange = args[2],
                Type = args[3].ToLower() != "null" ? args[3].ToString() : null,
                ExcludePending = args[4].ToLower() != "null" ? Convert.ToBoolean(args[4]) : false,
                ExcludeAcknowledged = !string.IsNullOrEmpty(args[5]) ? Convert.ToBoolean(args[5]) : false,
                ExcludeDiscarded = !string.IsNullOrEmpty(args[6]) ? Convert.ToBoolean(args[6]) : false,
                Attribute = args[7].ToLower() != "null" ? args[7] : null,
                Category = args[8].ToLower() != "null" ? args[8] : null,
                Page = args[9].ToLower() != "null" ? args[9] : null,
                PageSize = args[10].ToLower() != "null" ? args[10] : null,
                Sort = args[11]
            };
            return alarmFilter;
        }
    }
}
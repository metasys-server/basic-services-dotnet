using System;
using System.Globalization;
using JohnsonControls.Metasys.BasicServices;

namespace MetasysServicesExampleApp.FeaturesDemo
{
    public class AuditsDemo
    {
        private MetasysClient client;
        public AuditsDemo(MetasysClient client)
        {
            this.client = client;
        }
        public void Run()
        {
            try {
                #region Audits

                Console.WriteLine("Enter audit id to get audit details: ");
                string inputAuditId = Console.ReadLine();

                Guid auditId = Guid.Parse(inputAuditId);

                var auditItem = client.Audits.GetSingleAudit(auditId);

                Console.WriteLine(string.Format("\n Audit details found for {0}", auditId));
                Console.WriteLine($"\n Id: {auditItem.Id}, User: {auditItem.User}, PreData: {auditItem.PreData}, PostDate: {auditItem.PostData}");

                string auditFilterInputItem;
                var auditFilterItem = new AuditFilter();

                Console.WriteLine("Please enter these parameters separated by space: Start Time, End Time, Origin Applications," +
                                  "Class Levels, Action Type, Exclude discarded, Page, Page Size, Sort" +
                                  "\nRefer to the metasys-server/basic-services-dotnet README if you need help getting started.");

                auditFilterInputItem = Console.ReadLine();

                string[] args;
                args = auditFilterInputItem.Split(' ');

                if (args != null)
                {
                    auditFilterItem = ReadUserInput(args);
                }

                Console.WriteLine("\n List of audits with details");

                var auditItems = client.Audits.GetAudits(auditFilterItem);

                Console.WriteLine($"\n Total: {auditItems.Total}");
                Console.WriteLine($"\n Page Count: {auditItems.PageCount}");
                Console.WriteLine($"\n Page Size: {auditItems.PageSize}");
                Console.WriteLine($"\n Current Page: {auditItems.CurrentPage}");

                foreach (var item in auditItems.Items)
                {
                    Console.WriteLine($"\n Id: {item.Id}, User: {item.User}, PreData: {item.PreData}, PostDate: {item.PostData}");
                }

                Console.WriteLine("\nEnter object id to get audit details: ");
                string objectId = Console.ReadLine();

                var auditsForObject = new AuditFilter();

                Console.WriteLine("Please enter these parameters separated by space: Start Time, End Time, Origin Applications," +
                                  "Class Levels, Action Type, Exclude discarded, Page, Page Size, Sort" +
                                  "\nRefer to the metasys-server/basic-services-dotnet README if you need help getting started.");

                auditFilterInputItem = Console.ReadLine();
                args = auditFilterInputItem.Split(' ');

                if (args != null)
                {
                    auditsForObject = ReadUserInput(args);
                }

                Console.WriteLine(string.Format("\nAudit details found for this object {0}", objectId));

                var auditItemsForObject = client.Audits.GetAuditsForAnObject(Guid.Parse(objectId), auditsForObject);

                Console.WriteLine($"\n Total: {auditItemsForObject.Total}");
                Console.WriteLine($"\n Page Count: {auditItemsForObject.PageCount}");
                Console.WriteLine($"\n Page Size: {auditItemsForObject.PageSize}");
                Console.WriteLine($"\n Current Page: {auditItemsForObject.CurrentPage}");

                foreach (var item in auditItemsForObject.Items)
                {
                    Console.WriteLine($"\n Id: {item.Id}, User: {item.User}, PreData: {item.PreData}, PostDate: {item.PostData}");
                }
            }

            catch (Exception) {
                Console.WriteLine("\n \nAn Error occurred. Press Enter to return to Main Menu");
            }
                Console.ReadLine();
           
            #endregion
        }

        private static AuditFilter ReadUserInput(string[] args)
        {
            AuditFilter auditFilter = new AuditFilter
            {
                StartTime = DateTime.Parse(args[0], null, DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse(args[1], null, DateTimeStyles.RoundtripKind),
                OriginApplications = args[2].ToLower() != "null" ? args[2] : "1,2",
                ClassLevels = args[3].ToLower() != "null" ? args[3] : "1,2",
                ActionTypes = args[4].ToLower() != "null" ? args[4] : "1,2",
                ExcludeDiscarded = !string.IsNullOrEmpty(args[5]) ? Convert.ToBoolean(args[5]) : false,
                Page = args[6].ToLower() != "null" ? Convert.ToInt32(args[6]) : 0,
                PageSize = args[7].ToLower() != "null" ? Convert.ToInt32(args[7]) : 0,
                Sort = args[8].ToLower() != "null" ? args[8] : "creationTime"
            };
            return auditFilter;
        }
    }
}

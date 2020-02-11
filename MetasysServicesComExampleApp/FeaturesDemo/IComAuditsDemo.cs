using System;
using System.Globalization;
using JohnsonControls.Metasys.ComServices;

namespace MetasysServicesComExampleApp.FeaturesDemo
{
    public class IComAuditsDemo
    {
        private ILegacyMetasysClient legacyClient;

        public IComAuditsDemo(ILegacyMetasysClient legacyClient)
        {
            this.legacyClient = legacyClient;
        }
        public void Run()
        {
            #region Audits

            Console.WriteLine("Enter audit id to get audit details: ");
            string inputAuditId = Console.ReadLine();

            IComProvideAuditItem auditItem = (IComProvideAuditItem)legacyClient.GetSingleAudit(inputAuditId);

            Console.WriteLine(string.Format("\n Audit details found for {0}", inputAuditId));
            Console.WriteLine($"\n Id: {auditItem.Id}, User: {auditItem.User}, PreData: {auditItem.PreData}, PostDate: {auditItem.PostData}");

            string auditFilterInputItem;
            ComAuditFilter auditFilterItem = new ComAuditFilter();

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

            IComPagedResult PagedResult = (IComPagedResult)legacyClient.GetAudits(auditFilterItem);
            IComProvideAuditItem[] auditItems = (IComProvideAuditItem[])PagedResult.Items;

            Console.WriteLine($"\n Total: {PagedResult.Total}");
            Console.WriteLine($"\n Page Count: {PagedResult.PageCount}");
            Console.WriteLine($"\n Page Size: {PagedResult.PageSize}");
            Console.WriteLine($"\n Current Page: {PagedResult.CurrentPage}");

            if (auditItems != null)
            {
                foreach (var item in auditItems)
                {
                    Console.WriteLine($"\n Id: {item.Id}, User: {item.User}, PreData: {item.PreData}, PostDate: {item.PostData}");
                }
            }
            else
            {
                Console.WriteLine("No audit found.");
            }

            Console.WriteLine("\nEnter object id to get audit details: ");
            string objectId = Console.ReadLine();

            var auditsForObject = new ComAuditFilter();

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

            IComPagedResult PagedResultAuditForObject = (IComPagedResult)legacyClient.GetAuditsForAnObject(objectId, auditsForObject);
            IComProvideAuditItem[] auditItemsForObject = (IComProvideAuditItem[])PagedResultAuditForObject.Items;

            Console.WriteLine($"\n Total: {PagedResultAuditForObject.Total}");
            Console.WriteLine($"\n Page Count: {PagedResultAuditForObject.PageCount}");
            Console.WriteLine($"\n Page Size: {PagedResultAuditForObject.PageSize}");
            Console.WriteLine($"\n Current Page: {PagedResultAuditForObject.CurrentPage}");

            foreach (var item in auditItemsForObject)
            {
                Console.WriteLine($"\n Id: {item.Id}, User: {item.User}, PreData: {item.PreData}, PostDate: {item.PostData}");
            }

            Console.ReadLine();
            #endregion
        }

        private static ComAuditFilter ReadUserInput(string[] args)
        {
            DateTime startTime = DateTime.Parse(args[0], null, DateTimeStyles.AssumeUniversal);
            DateTime endTime = DateTime.Parse(args[1], null, DateTimeStyles.AssumeUniversal);

            ComAuditFilter auditFilter = new ComAuditFilter
            {
                StartTime = startTime.ToString(),
                EndTime = endTime.ToString(),
                OriginApplications = args[2],
                ClassLevels = args[3],
                ActionTypes = args[4],
                ExcludeDiscarded = !string.IsNullOrEmpty(args[5]) ? Convert.ToBoolean(args[5]) : false,
                Page = args[6],
                PageSize = args[7],
                Sort = args[8]
            };
            return auditFilter;
        }
    }
}

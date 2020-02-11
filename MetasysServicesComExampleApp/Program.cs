using System;
using System.Collections.Generic;
using JohnsonControls.Metasys.ComServices;
using MetasysServicesComExampleApp.FeaturesDemo;

namespace MetasysServicesComExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var comMetasysClientFactory = new ComMetasysClientFactory();
            Console.WriteLine("Please enter your credentials." +
                   "\nRefer to the metasys-server/basic-services-dotnet README if you need help getting started.");
            Console.Write("Enter the Hostname:");
            var hostName = Console.ReadLine();
            Console.Write("Enter the Username:");
            var userName = Console.ReadLine();
            Console.Write("Enter the Password:");
            var password = Console.ReadLine();

            var legacyClient = comMetasysClientFactory.GetLegacyClient(hostName);

            #region Login

            legacyClient.TryLogin(userName, password);
            Console.WriteLine("Logging In.....");
            Console.WriteLine("Login Successfull...");

            #endregion

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu(legacyClient);
            }
        }

        private static bool MainMenu(ILegacyMetasysClient legacyClient)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get Object Identifier");
            Console.WriteLine("2) General Demo");
            Console.WriteLine("3) Refresh Token");
            Console.WriteLine("4) Spaces and Equipment");
            Console.WriteLine("5) Alarms");
            Console.WriteLine("6) Trends");
            Console.WriteLine("7) Audits");
            Console.WriteLine("8) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    new IComGetObjectIdentifierDemo(legacyClient).Run();
                    return true;
                case "2":
                    new IComGeneralDemo(legacyClient).Run();
                    return true;
                case "3":
                    new IComRefreshTokenDemo(legacyClient).Run();
                    return true;
                case "4":
                    new IComSpacesDemo(legacyClient).Run();
                    return true;
                case "5":
                    new IComAlarmsDemo(legacyClient).Run();
                    return true;
                case "6":
                    new IComTrendsDemo(legacyClient).Run();
                    return true;
                case "7":
                    new IComAuditsDemo(legacyClient).Run();
                    return true;
                case "8":
                    return false;
                default:
                    return true;
            }
        }
    }
}

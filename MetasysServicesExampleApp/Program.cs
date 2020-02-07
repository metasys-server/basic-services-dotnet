using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JohnsonControls.Metasys.BasicServices;
using MetasysServicesExampleApp.FeaturesDemo;

namespace MetasysServicesExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionDetails;
            if (args.Length != 3)
            {
                Console.WriteLine("Please enter in your credentials in this format: {username} {password} {hostname}." +
                    "\nRefer to the metasys-server/basic-services-dotnet README if you need help getting started.");                
                connectionDetails = Console.ReadLine();
                args = connectionDetails.Split(' ');
            }

            var username = args[0];
            var password = args[1];
            var hostname = args[2];      

            #region Login
            Console.WriteLine("Default culture is en_US. The culture for client translations can be changed in the code.");
            // CultureInfo culture = new CultureInfo("en-US");

            Console.WriteLine("\nLogging in...");
            var client = new MetasysClient(hostname);
            // var client = new MetasysClient(hostname, true); // Ignore Certificate Errors
            // var client = new MetasysClient(hostname, false, ApiVersion.V2, culture);

            var token = client.TryLogin(username, password);
            Console.WriteLine($"Access token: {token.Token} expires {token.Expires}.");
            #endregion

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu(client);
            }                                                                                                   
        }

        private static bool MainMenu(MetasysClient client)
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
                    new GetObjectIdentifierDemo(client).Run();
                    return true;
                case "2":
                    new GeneralDemo(client).Run();
                    return true;
                case "3":
                    new RefreshTokenDemo(client).Run();
                    return true;
                case "4":
                    new SpacesDemo(client).Run();
                    return true;
                case "5":
                    new AlarmsDemo(client).Run();
                    return true;
                case "6":
                    new TrendsDemo(client).Run();
                    return true;
                case "7":
                    new AuditsDemo(client).Run();
                    return true;
                case "8":
                    return false;
                default:
                    return true;
            }
        }
    }
}

using JohnsonControls.Metasys.BasicServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetasysServicesExampleApp.FeaturesDemo
{
    public class GetObjectIdentifierDemo
    {
        private MetasysClient client;
        public GetObjectIdentifierDemo(MetasysClient client)
        {
            this.client = client;
        }
        public void Run()
        {
            try {
                Console.WriteLine("\nIndicate the object you want to get the GUID.");
                Console.Write("Enter the fully qualified reference of the object (Example: \"site:device/itemReference\"): ");
                string object1 = Console.ReadLine();
                Console.WriteLine("\n\nGetObjectIdentifier...");
                // These variables are needed to run the other sections
                Guid id1 = client.GetObjectIdentifier(object1);
                Console.WriteLine($"{object1} id: {id1}");
            }
            catch (Exception) {
                Console.WriteLine("\n \nAn Error occurred. Press Enter to return to Main Menu");
            }

            Console.ReadLine();
        }
    }
}

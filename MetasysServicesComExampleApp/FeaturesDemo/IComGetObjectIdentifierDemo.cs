using JohnsonControls.Metasys.ComServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetasysServicesComExampleApp.FeaturesDemo
{
    public class IComGetObjectIdentifierDemo
    {
        private ILegacyMetasysClient legacyClient;

        public IComGetObjectIdentifierDemo(ILegacyMetasysClient legacyClient)
        {
            this.legacyClient = legacyClient;
        }

        public void Run()
        {
            try {
                Console.WriteLine("\nIndicate the object you want to run this example code on.");
                Console.Write("Enter the fully qualified reference of the object (Example: \"site:device/itemReference\"): ");
                string object1 = Console.ReadLine();

                Console.WriteLine("\nIndicate the second object you want to run this example code on.");
                Console.Write("Enter the fully qualified reference of the object: ");
                string object2 = Console.ReadLine();

                string object1Id = legacyClient.GetObjectIdentifier(object1);
                string object2Id = legacyClient.GetObjectIdentifier(object2);

                Console.WriteLine($"{object1} id: {object1Id}");
                Console.WriteLine($"{object2} id: {object2Id}");

                string[] ids = new string[2];
                ids[0] = object1Id;
                ids[1] = object2Id;
            }
            catch (Exception) {
                Console.WriteLine("\n \nAn Error occurred. Press Enter to return to Main Menu");
            }
        }
    }
}

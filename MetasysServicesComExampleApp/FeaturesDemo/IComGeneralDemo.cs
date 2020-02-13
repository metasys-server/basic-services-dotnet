using System;
using System.Collections.Generic;
using System.Linq;
using JohnsonControls.Metasys.ComServices;

namespace MetasysServicesComExampleApp.FeaturesDemo
{
    public class IComGeneralDemo
    {
        private ILegacyMetasysClient legacyClient;

        public IComGeneralDemo(ILegacyMetasysClient legacyClient)
        {
            this.legacyClient = legacyClient;
        }

        public void Run()
        {
            #region GetObjectIdentifier
            try {
                Console.WriteLine("\n\nGetObjectIdentifier...");

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

                #endregion

                #region Read Property

                Console.Write("\n!!!Please note ReadProperty will return null if the attribute does not exist, and will cause an exception in this example!!!");
                Console.Write("\nEnter an attribute of the objects (Examples: name, description, presentValue): ");
                string attribute1 = Console.ReadLine();

                Console.Write("Enter a second attribute of the objects: ");
                string attribute2 = Console.ReadLine();

                List<string> attributes = new List<string>() { attribute1, attribute2 };

                Console.WriteLine($"Get {attribute1} property...");

                var result1Attr1 = legacyClient.ReadProperty(object1Id, attribute1);
                var result2Attr1 = legacyClient.ReadProperty(object2Id, attribute1);

                Console.WriteLine($"{result1Attr1.Id} {result1Attr1.Attribute} values (string, int, bool, reliability): " +
                    $"\n{result1Attr1.StringValue}, {result1Attr1.NumericValue}, {result1Attr1.BooleanValue}, {result1Attr1.Reliability}");
                Console.WriteLine($"{result2Attr1.Id} {result2Attr1.Attribute} values (string, int, bool, reliability): " +
                    $"\n{result2Attr1.StringValue}, {result2Attr1.NumericValue}, {result2Attr1.BooleanValue}, {result2Attr1.Reliability}");

                #endregion

                #region ReadPropertyMultiple

                Console.WriteLine("\n\nReadPropertyMultiple...");

                Console.WriteLine($"\nGet {attribute1}, {attribute2} properties for each object...");

                var results = legacyClient.ReadPropertyMultiple(ids, attributes.ToArray());


                foreach (var varMultiple in (dynamic)(results))
                {
                    // Grab the list of Variants for each id
                    var variants = varMultiple.Variants;
                    foreach (var result in variants)
                    {
                        Console.WriteLine($"{result.Id} {result.Attribute} values (string, int, bool, reliability): " +
                            $"\n{result.StringValue}, {result.NumericValue},  {result.BooleanValue}, {result.Reliability}");
                    }
                }

                #endregion

                #region WriteProperty

                Console.Write("\nEnter an attribute of the objects to change (don't worry, this will be reset to it's original value): ");
                string attribute3 = Console.ReadLine();

                Console.Write("\nEnter one new value for this attribute (this will be applied to both objects): ");
                string change = Console.ReadLine();

                // Get original values, this code will throw an exception if the 
                var result1Attr3 = legacyClient.ReadProperty(object1Id, attribute3);
                var result2Attr3 = legacyClient.ReadProperty(object2Id, attribute3);
                Console.WriteLine($"{result1Attr3.Id} {result1Attr3.Attribute} original value: {result1Attr3.StringValue}");
                Console.WriteLine($"{result2Attr3.Id} {result2Attr3.Attribute} original value: {result2Attr3.StringValue}");

                Console.WriteLine("\nWriteProperty...");

                // Change value
                legacyClient.WriteProperty(object1Id, attribute3, change);
                System.Threading.Thread.Sleep(1000);

                // View changes
                var result1Attr3Updated = legacyClient.ReadProperty(object1Id, attribute3);
                Console.WriteLine($"{result1Attr3Updated.Id} {result1Attr3Updated.Attribute} updated value: {change}");

                // Reset value
                legacyClient.WriteProperty(object1Id, attribute3, result1Attr3.StringValue);
                System.Threading.Thread.Sleep(1000);

                #endregion

                #region Write Property Multiple

                Console.WriteLine("\nWrite Property Multiple...");

                // Change value
                string[] attributes2 = new string[1];
                attributes2[0] = attribute3;
                Console.Write("\nEnter one new value for this attribute (this will be applied to both objects): ");
                string[] changes = new string[2];
                changes[0] = Console.ReadLine();
                changes[1] = changes[0];
                legacyClient.WritePropertyMultiple(ids, attributes2, changes);
                System.Threading.Thread.Sleep(1000);

                // View changes
                var result1Attr3UpdatedM = legacyClient.ReadProperty(object1Id, attribute3);
                var result2Attr3UpdatedM = legacyClient.ReadProperty(object2Id, attribute3);
                Console.WriteLine($"{result1Attr3UpdatedM.Id} {result1Attr3UpdatedM.Attribute} updated value: {changes[0]}");
                Console.WriteLine($"{result2Attr3UpdatedM.Id} {result2Attr3UpdatedM.Attribute} updated value: {changes[1]}");
                System.Threading.Thread.Sleep(1000);

                // Reset Values
                legacyClient.WriteProperty(object1Id, attribute3, result1Attr3.StringValue);
                legacyClient.WriteProperty(object2Id, attribute3, result2Attr3.StringValue);

                #endregion

                #region Commands

                Console.WriteLine("\n\nGetCommands...");

                Console.WriteLine($"{object1Id} Commands:");

                dynamic commands = legacyClient.GetCommands(object1Id);

                if (commands.Length != null)
                {
                    // Console.WriteLine($"{commands.Count()}\n");
                    foreach (dynamic command in (dynamic)commands)
                    {
                        Console.WriteLine($"{command.Title}\n");
                    }
                }
                else
                {
                    Console.WriteLine("No commands found.");
                }

                #endregion

                #region GetNetworkDevices

                Console.WriteLine("\n\nGetNetworkDevices...");

                var types = legacyClient.GetNetworkDeviceTypes();
                foreach (var type in (dynamic)types)
                {
                    Console.WriteLine($"\nAvailable Type {type.Id}: {type.Description}, {type.DescriptionEnumerationKey}");
                    string typeId = type.Id.ToString();
                    dynamic devices = legacyClient.GetNetworkDevices(typeId);
                    Console.WriteLine($"Devices found: {devices.Length}");
                    Console.WriteLine($"First Device: {devices[0].Name}");
                }

                #endregion

                #region GetObjects

                Console.WriteLine("\n\nGet Objects..");

                Console.WriteLine("\nPlease enter the depth of objects to retrieve for the first object.");
                Console.Write("(1 = only this object, 2 = immediate children only): ");

                int level = Convert.ToInt32(Console.ReadLine());
                IComMetasysObject[] objects = (IComMetasysObject[])legacyClient.GetObjects(object2Id, level);
                if (objects.Count() > 0)
                {
                    IComMetasysObject obj = objects.ElementAt(0);

                    Console.WriteLine($"Parent object: {obj.Id}");
                    IComMetasysObject[] objchild = (IComMetasysObject[])obj.Children;
                    for (int i = 1; i < level; i++)
                    {
                        Console.WriteLine($"Child at level {i}: {obj.Id} - {obj.Name}");
                        if (objects.ElementAt(0).ChildrenCount > 0)
                        {
                            obj = objchild.ElementAt(0);
                        }
                        else
                        {
                            Console.WriteLine("This object has no children.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("This object has no children.");
                }
            }
            catch (Exception) {
                Console.WriteLine("\n \nAn Error occurred. Press Enter to return to Main Menu");
            }

            Console.ReadLine();
            #endregion 
        }
    }
}

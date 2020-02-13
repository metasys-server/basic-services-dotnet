using JohnsonControls.Metasys.BasicServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MetasysServicesExampleApp.FeaturesDemo
{
    public class RefreshTokenDemo
    {
        private MetasysClient client;
        public RefreshTokenDemo(MetasysClient client)
        {
            this.client = client;
        }
        public void Run()
        {
            #region Refresh
            try {
                Console.WriteLine("\nRefreshing Token...");
                var token = client.Refresh();
                Console.WriteLine($"Access token: {token.Token} expires {token.Expires}.");
            }
            catch (Exception) {
                Console.WriteLine("\n \nAn Error occurred. Press Enter to return to Main Menu");
            }

            Console.ReadLine();
            #endregion
        }       
    }
}

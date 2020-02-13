using System;
using JohnsonControls.Metasys.ComServices;

namespace MetasysServicesComExampleApp.FeaturesDemo
{
    public class IComRefreshTokenDemo
    {
        private ILegacyMetasysClient legacyClient;

        public IComRefreshTokenDemo(ILegacyMetasysClient legacyClient)
        {
            this.legacyClient = legacyClient;
        }

        public void Run()
        {
            #region Refresh
            try {
                Console.WriteLine("\nRefreshing Token...");
                var token = legacyClient.Refresh();
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

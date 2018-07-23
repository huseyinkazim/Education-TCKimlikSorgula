using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TCKimlikSorgula
{
    class Program
    {
        private static KPSService.KPSPublicSoapClient KPSServiceClient = null;
        private static void CreateServiceClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "KPSPublicSoap";
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            EndpointAddress address = new
            EndpointAddress("https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx");

            KPSServiceClient = new KPSService.KPSPublicSoapClient(binding, address);
        }
        static void Main(string[] args)
        {
            try {
                var x = System.Net.ServicePointManager.SecurityProtocol;//DEFAULT TLS=192
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            CreateServiceClient();
            if (KPSServiceClient == null)
                throw new Exception("Cannot Create An Instance Of KPS Service");

            CultureInfo culture = new CultureInfo("tr-TR", false);

                //You must imlement this place
            long TCKmlikNo;
            string name;
            string lastname;
            int birthDayYear;



            var response = KPSServiceClient.TCKimlikNoDogrula(TCKmlikNo, name.ToUpper(culture), lastname.ToUpper(culture), birthDayYear);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

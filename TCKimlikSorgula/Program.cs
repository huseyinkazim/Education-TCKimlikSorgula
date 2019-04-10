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
        static long TCKmlikNo;
        static string name { set; get; }
        static string lastname { set; get; }
        static int birthDayYear ; 
        private static void CreateServiceClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "KPSPublicSoap";
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            EndpointAddress address = new
            EndpointAddress("https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx");

            KPSServiceClient = new KPSService.KPSPublicSoapClient(binding, address);
        }
        private static void DegerAl()
        {
            bool uygun;
            do
            {
                Console.WriteLine("Lütfen Tc Kimlik numaranızı giriniz:");
                var tc = Console.ReadLine();
                uygun = long.TryParse(tc, out TCKmlikNo);

                uygun = uygun ? tc.Length == 11 : uygun;
                if (!uygun)
                    Console.WriteLine("Gerçek bir Tc Kimlik giriniz.");
            } while (!uygun);
            Console.WriteLine("Lütfen Adınızı giriniz:");
            name = Console.ReadLine();
            Console.WriteLine("Lütfen Soyadınızı giriniz:");
            lastname = Console.ReadLine();

            uygun = false;
            do
            {
                Console.WriteLine("Lütfen Doğum yılınızı giriniz:");
                var birthyear = Console.ReadLine();
                uygun=int.TryParse(birthyear, out birthDayYear);
                uygun = uygun ?birthyear.Length==4:uygun;
                if(!uygun)
                    Console.WriteLine("Lütfen doğum yılınızı kontrol edin.");


            } while (!uygun);

        }
        static void Main(string[] args)
        {
            try
            {
                do
                {
                    var x = System.Net.ServicePointManager.SecurityProtocol;//DEFAULT TLS=192
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    CreateServiceClient();
                    if (KPSServiceClient == null)
                        throw new Exception("Cannot Create An Instance Of KPS Service");

                    CultureInfo culture = new CultureInfo("tr-TR", false);

                    //You must imlement this place

                    DegerAl();

                    var response = KPSServiceClient.TCKimlikNoDogrula(TCKmlikNo, name.ToUpper(culture), lastname.ToUpper(culture), birthDayYear);
                    if (response)
                        Console.WriteLine("Türkiye Cumhuriyetinde girdiğiniz değerlerle bir kayıt bulunmaktadır");
                    else
                        Console.WriteLine("Türkiye Cumhuriyetinde girdiğiniz değerlerle bir kayıt bulunmamaktadır");

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

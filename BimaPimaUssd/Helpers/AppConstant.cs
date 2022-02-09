using System;

namespace BimaPimaUssd.Helpers
{
    public class AppConstant
    {
        public static readonly string Header = "application/json";
        public static readonly string Env = "ussdtest";
        public static readonly string Tenant = "acre";
        public static readonly string Url = "https://openapi-staging.etherisc.com";
        public static readonly string SMS_URL = "http://167.172.47.173/acresms/sms.php";
        public static readonly string PushSTKEP = "https://api.safaricom.co.ke/mpesa/stkpush/v1/processrequest";
        public static readonly string TOKEN_URL = "https://api.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";
        public static readonly string REVERSE_GEOCODE_URL = "https://maps.googleapis.com/maps/api/geocode/json";
        public static readonly string BASE_URL_FARMER = "https://farmer-api.agrismart.co.ke";
        public static readonly string COMPANY = "ACRE_AFRICA";
        public static readonly string Country = "KE";
        public static readonly string Currency = "KES";



        public static int GetLastDayOfWeek(int Month, int week)
        {
            return week switch
            {
                1 => 7,
                2 => 14,
                3 => 21,
                4 => 28,
                5 => new DateTime(DateTime.Now.Year, Month + 1, 1).AddDays(-1).Day,
                _ => 30,
            };


        }
    }

    public static class IFVM
    {
        public static string Invalid => "END Inavid entry. Please try again.";
        public static string InvalidCode => "CON Inavid farmer code. Please try again.";

        //common
        internal static string ThankFarmer()
        {
            throw new NotImplementedException();
        }

        internal static string CheckExisting() => "CON 1.Existing farmer.\n2. New farmer";

        internal static string PlantingWeek()
        {
            throw new NotImplementedException();
        }


        internal static string CollectCode() => "CON Enter farmer code.\n";

        internal static string LoadValueChains() => "CON 1. Maize\n2. Greengrams";

        internal static string TypeMonth() => "CON Type planting month (1-12)";

        internal static string GetWeek() => "CON 1. 1st week\n2. 2nd week\n3. 3rd week\n4. 4th week\n5. 5th week";

        internal static string SelectPayMethod() => $"CON Select payment method.\n 1. Cash\n 2. Mpesa";
        internal static string ConfirmPay(decimal premium, decimal subsidy) => $"CON Confirm. Premium amount KES {premium}, Subsidy KES {subsidy}, Final pay KES {premium - subsidy} \n 1. Confirm\n 2. Cancel";

        internal static string ProcessCash() => $"END Thank you for using our service. Find more details in your SMS";
        internal static string ProcessCancel() => $"END Thank you for using our service. Feel free to try again.";

        internal static string ProcessMpesa() => $"END Thank you\nEnter your MPESA PIN in the next prompt to complete the request";

        internal static string GetAmount() => "CON Enter premium amount";
        internal static string GetPhone(string phone) => $"CON Select phone number to pay\n 1. {phone}\n 2. Enter new number.";

        internal static string EnterMpesaNo() => "CON ENter phone number";
    }
}

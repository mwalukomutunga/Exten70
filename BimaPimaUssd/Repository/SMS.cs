using BimaPimaUssd.Contracts;
using BimaPimaUssd.Helpers;
using BimaPimaUssd.Models;
using System;
using System.Threading.Tasks;

namespace BimaPimaUssd.Repository
{
    public class SMS : IMessager
    {
        private readonly HttpHelper helper;
        public SMS(HttpHelper helper)
        {
            this.helper = helper;
        }
        public async Task<dynamic> SendMessage(string message, string To)
        {
            return await helper.ProcessPostRequest<string, SMS_OBJECT>(AppConstant.SMS_URL, payload: new SMS_OBJECT() { message = message, msisdn = To, source = "Digi farm", senderid = AppConstant.COMPANY });
        }

        public void SendMessage(string From, string message, string[] To)
        {
            throw new NotImplementedException();
        }

    }
}

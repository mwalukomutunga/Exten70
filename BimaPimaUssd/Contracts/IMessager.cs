using System.Threading.Tasks;

namespace BimaPimaUssd.Contracts
{
    public interface IMessager
    {
        Task<dynamic> SendMessage(string message, string To);
        void SendMessage(string From, string message, string[] To);
    }
}

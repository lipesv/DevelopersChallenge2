using System.Threading.Tasks;

namespace OFX.Application.Services.Interfaces
{
    public interface IParserService
    {
        Task Parse(string path);
    }
}

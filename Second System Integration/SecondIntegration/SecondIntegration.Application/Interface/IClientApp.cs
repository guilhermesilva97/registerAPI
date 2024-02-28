using SecondIntegration.Model.Common;
using SecondIntegration.Model.Request;

namespace SecondIntegration.Application.Interface
{
    public interface IClientApp
    {
        Task<ResultResponseObject<bool>> InsertClient(ClientRequest request);
    }
}

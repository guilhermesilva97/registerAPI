using FirstIntegration.Model.Common;
using FirstIntegration.Model.Request;

namespace FirstIntegration.Application.Interface
{
    public interface IClientApp
    {
        Task<ResultResponseObject<bool>> InsertClient(ClientRequest request);
    }
}

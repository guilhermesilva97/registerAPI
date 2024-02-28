using RegisterAPI.Model.Common;
using RegisterAPI.Model.Request.User;

namespace RegisterAPI.Application.Interface
{
    public interface IClientApp
    {
        Task<ResultResponseObject<Guid>> InsertClient(ClientRequest request);
    }
}

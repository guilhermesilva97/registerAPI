using FirstIntegration.Application.Interface;
using FirstIntegration.Model.Common;
using FirstIntegration.Model.Request;

namespace FirstIntegration.Application
{
    public class ClientApp : IClientApp
    {
        public async Task<ResultResponseObject<bool>> InsertClient(ClientRequest request)
        {

            ResultResponseObject<bool> result = new ResultResponseObject<bool>();

            result = await ValidateRequest(request);

            if (result.Success)
            {

            }

            return result;
        }

        private async Task<ResultResponseObject<bool>> ValidateRequest(ClientRequest request)
        {
            ResultResponseObject<bool> result = new ResultResponseObject<bool>();

            if (string.IsNullOrEmpty(request.Document) || request.Document.Length != 11)
            {
                result.AddError("Documento está incorreto");
            }

            return result;
        }
    }
}

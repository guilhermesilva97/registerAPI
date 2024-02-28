
using Newtonsoft.Json;
using RegisterAPI.CrossCutting.ExternalService.Enum;
using RegisterAPI.CrossCutting.ExternalService.Interface;
using RegisterAPI.CrossCutting.ExternalService.Models;
using RegisterAPI.Entity.Entities;
using RegisterAPI.Entity.Logger;
using RegisterAPI.Model.Common;
using RegisterAPI.Repository.Interface;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace RegisterAPI.CrossCutting.ExternalService
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly ILogRepository _logRepository;
        public IntegrationService(IIntegrationRepository integrationRepository, ILogRepository logRepository)
        {
            _integrationRepository = integrationRepository;
            _logRepository = logRepository;

        }
        public async Task SendClientToIntegrations(Client request)
        {
            IEnumerable<Integration> integrations = await _integrationRepository.GetAll();

            if (integrations.Any())
            {
                foreach (var integration in integrations)
                {
                    await SendClientToIntegration(integration, request);
                }
            }
        }
        public async Task SyncClientToIntegration(ClientIntegration request)
        {
            await SendClientToIntegration(request.Integration, request.Client);
        }
        public async Task SendClientToIntegration(Integration integration, Client client)
        {
            ResultResponseObject<bool> result = new ResultResponseObject<bool>();

            UriBuilder uri = new($"{integration.UrlBase}{Routes.InsertClient}");
            NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
            uri.Query = query.ToString();

            string clientJson = JsonConvert.SerializeObject(client);
            HttpContent content = new StringContent(clientJson, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new(HttpMethod.Post, uri.ToString());
            request.Headers.Add("X-API-KEY", integration.Token);
            request.Content = content;

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.SendAsync(request);

                string contents = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<ResultResponseObject<bool>>(contents);

                    if (!result.Success)
                    {
                        Log log = new Log
                        {
                            Id = Guid.NewGuid(),
                            Error = JsonConvert.SerializeObject(result.ErrorMessages),
                            Method = $"Send Client To Integration {integration.NameIntegration}",
                            Object = JsonConvert.SerializeObject(client)
                        };
                        await _logRepository.InsertLog(log);
                    }
                }
                else
                {
                    Log log = new Log
                    {
                        Id = Guid.NewGuid(),
                        Error = response.ReasonPhrase,
                        Method = $"Send Client To Integration {integration.NameIntegration}",
                        Object = JsonConvert.SerializeObject(client)
                    };
                    await _logRepository.InsertLog(log);
                }
            }
        }
        public async Task<Integration> GetIntegrationById(Guid integrationGuid)
        {
            return await _integrationRepository.GetIntegrationById(integrationGuid);
        }
        public async Task<IEnumerable<Integration>> GetAll()
        {
            return await _integrationRepository.GetAll();
        }
    }
}

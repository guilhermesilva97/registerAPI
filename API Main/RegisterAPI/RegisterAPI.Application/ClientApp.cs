using AutoMapper;
using Newtonsoft.Json;
using RegisterAPI.Application.Interface;
using RegisterAPI.CrossCutting.ExternalService.Interface;
using RegisterAPI.CrossCutting.ExternalService.Models;
using RegisterAPI.CrossCutting.QueueService.Interface;
using RegisterAPI.Entity.Entities;
using RegisterAPI.Entity.Logger;
using RegisterAPI.Model.Common;
using RegisterAPI.Model.Request.User;
using RegisterAPI.Service.Interface;

namespace RegisterAPI.Application
{
    public class ClientApp : IClientApp
    {
        private IClientService _clientService;
        private IMapper _mapper;
        private ISendMessageService _messageService;
        private IIntegrationService _integrationService;
        private ILogService _loggerService;
        public ClientApp(IClientService userService, IMapper mapper, ISendMessageService messageService, ILogService loggerService, IIntegrationService integrationService)
        {
            _clientService = userService;
            _mapper = mapper;
            _messageService = messageService;
            _loggerService = loggerService;
            _integrationService = integrationService;
        }
        public async Task<ResultResponseObject<Guid>> InsertClient(ClientRequest request)
        {
            ResultResponseObject<Guid> resultResponse = new ResultResponseObject<Guid>();

            try
            {
                Client client = await _clientService.GetClientByDocument(request.Document);

                if (client == null)
                {
                    Client clientToInsert = _mapper.Map<Client>(request);

                    resultResponse.Value = await _clientService.InsertClient(clientToInsert);

                    IEnumerable<Integration> integrations = await _integrationService.GetAll();

                    if (integrations.Any())
                    {
                        _messageService.SendClientMessage(clientToInsert);
                    }
                }
                else
                {
                    resultResponse.AddError("Cliente já cadastrado");
                }
            }
            catch (Exception ex)
            {
                Log log = new Log
                {
                    Id = Guid.NewGuid(),
                    Error = ex.ToString(),
                    Method = "Insert Client",
                    Object = JsonConvert.SerializeObject(request)
                };

                await _loggerService.InsertLog(log);

                resultResponse.AddError(ex.ToString());
            }

            return resultResponse;
        }
        public async Task<ResultResponseObject<bool>> SyncClients(Guid integrationGuid)
        {
            ResultResponseObject<bool> resultResponse = new ResultResponseObject<bool>();

            try
            {
                Integration integration = await _integrationService.GetIntegrationById(integrationGuid);

                if (integration != null)
                {
                    IEnumerable<Client> clients = await _clientService.GetAll();

                    if (clients.Any())
                    {
                        foreach (Client client in clients)
                        {
                            ClientIntegration clientIntegration = new ClientIntegration()
                            {
                                Client = client,
                                Integration = integration
                            };

                            _messageService.SendClientSync(clientIntegration);
                        }
                    }
                }
                else
                {
                    resultResponse.AddError("Integração não encontrada");
                }
            }
            catch (Exception ex)
            {
                Log log = new Log
                {
                    Id = Guid.NewGuid(),
                    Error = ex.ToString(),
                    Method = "Sync Client",
                    Object = integrationGuid.ToString()
                };

                await _loggerService.InsertLog(log);

                resultResponse.AddError(ex.ToString());
            }

            return resultResponse;
        }
    }
}

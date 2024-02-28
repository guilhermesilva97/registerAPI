using AutoMapper;
using Newtonsoft.Json;
using RegisterAPI.Application.Interface;
using RegisterAPI.CrossCutting.ExternalService.Interface;
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
        private ILogService _loggerService;
        public ClientApp(IClientService userService, IMapper mapper, ISendMessageService messageService, ILogService loggerService)
        {
            _clientService = userService;
            _mapper = mapper;
            _messageService = messageService;
            _loggerService = loggerService;
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

                    _messageService.SendUserMessage(clientToInsert);
                }
                else
                {
                    resultResponse.AddError("Usuário já cadastrado");
                }
            }
            catch (Exception ex)
            {
                Log log = new Log
                {
                    Error = ex.ToString(),
                    Method = "Insert User",
                    Object = JsonConvert.SerializeObject(request)
                };

                await _loggerService.InsertLog(log);
                
                resultResponse.AddError(ex.ToString());
            }

            return resultResponse;
        }
    }
}

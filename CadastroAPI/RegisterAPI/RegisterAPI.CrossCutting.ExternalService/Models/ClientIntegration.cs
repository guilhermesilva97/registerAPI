using RegisterAPI.Entity.Entities;

namespace RegisterAPI.CrossCutting.ExternalService.Models
{
    public class ClientIntegration
    {
        public Client Client { get; set; }
        public Integration Integration { get; set; }
    }
}

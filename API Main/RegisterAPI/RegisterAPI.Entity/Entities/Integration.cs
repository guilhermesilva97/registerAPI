using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterAPI.Entity.Entities
{
    [Table("Integration")]
    public class Integration
    {
        [Key]
        public Guid Id { get; set; }
        public string NameIntegration { get; set; }
        public string UrlBase { get; set; }
        public string Token { get; set; }
    }
}

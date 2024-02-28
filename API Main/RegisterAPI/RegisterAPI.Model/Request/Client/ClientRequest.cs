namespace RegisterAPI.Model.Request.User
{
    public class ClientRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
        public DateTime DateBirth { get; set; }
    }
}

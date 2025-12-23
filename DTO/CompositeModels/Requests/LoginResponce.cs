namespace DTO.CompositeModels.Requests
{
    public class LoginResponce
    {
        public int ClientId { get; set; }
        
        public string ClientFullName { get; set; } = null!;
        
        public string Token { get; set; } = null!;
        
        public string Role { get; set; } = null!;
    }
}

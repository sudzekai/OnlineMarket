namespace DTO.Models.Clients
{
    public class ClientCreateDto
    {
        public string FullName { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}

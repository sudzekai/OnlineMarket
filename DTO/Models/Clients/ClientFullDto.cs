namespace DTO.Models.Clients
{
    public class ClientFullDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}

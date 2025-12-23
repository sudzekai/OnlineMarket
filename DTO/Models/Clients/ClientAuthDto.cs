namespace DTO.Models.Clients
{
    public class ClientAuthDto
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}

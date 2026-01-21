namespace APIWEB.src.Features.User.Domain.Entity
{
    public class User
    {
        public int InternalId { get; set; }
        public string? Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public byte[] Salt { get; set; } = Array.Empty<byte>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}
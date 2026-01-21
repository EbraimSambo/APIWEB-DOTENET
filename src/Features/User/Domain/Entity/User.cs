namespace APIWEB.src.Features.User.Domain.Entity
{
    public class User
    {
        public int InternalId { get; set; }
        public string? Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
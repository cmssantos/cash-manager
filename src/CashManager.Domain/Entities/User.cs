namespace CashManager.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string GoogleId { get; private set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
    public required string CurrentPassword { get; set; } = string.Empty;
    public IList<PasswordHistory>? PasswordHistories { get; set; } = [];

    public User() => Id = Guid.NewGuid();

    public User(string googleId)
    {
        Id = Guid.NewGuid();
        GoogleId = googleId ?? throw new ArgumentNullException(nameof(googleId));
    }
}

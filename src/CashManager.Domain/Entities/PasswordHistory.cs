namespace CashManager.Domain.Entities;

public class PasswordHistory
{
    public Guid UserId { get; set; }
    public required User User { get; set; }

    public int Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}

using System.ComponentModel.DataAnnotations;

namespace UserActivity.Infrastructure;

public class UserActivityPostgreSqlOptions
{
    [Required]
    public required string Host { get; init; }
    
    [Required]
    [Range(5000, 65535, ErrorMessage = "Port must be between 5000 and 65535")]
    public required int Port { get; init; }
    
    [Required]
    public required string Username { get; init; }
    
    [Required]
    public required string Password { get; init; }
    
    [Required]
    public required string Database { get; init; }
    
    public bool? DisableSsl { get; init; }
    public string? Pooling { get; init; }
    public string? Timeout { get; init; }
    public string? MaxPoolSize { get; init; }
    public string? ConnectionIdleLifetime { get; init; }

    public string GetConnectionString()
    {
            var parameters = new List<string>
            {
                $"User ID={Username}",
                $"Password={Password}",
                $"Host={Host}",
                $"Port={Port}",
                $"Database={Database}",
                $"SSL Mode={GetSslMode}"
            };

            if (!string.IsNullOrWhiteSpace(Pooling))
            {
                parameters.Add($"Pooling={Pooling}");
            }

            if (!string.IsNullOrWhiteSpace(Timeout))
            {
                parameters.Add($"Timeout={Timeout}");
            }

            if (!string.IsNullOrWhiteSpace(MaxPoolSize))
            {
                parameters.Add($"Maximum Pool Size={MaxPoolSize}");
            }

            if (!string.IsNullOrWhiteSpace(ConnectionIdleLifetime))
            {
                parameters.Add($"Connection Idle Lifetime={ConnectionIdleLifetime}");
            }

            return string.Join(';', parameters);
    }

    private string GetSslMode => DisableSsl is true
        ? "Disable"
        : "VerifyFull";
}

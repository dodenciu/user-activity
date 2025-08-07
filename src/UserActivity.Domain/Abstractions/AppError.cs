namespace UserActivity.Domain.Abstractions;

public record AppError(string Code, string Name)
{
    public static readonly AppError None = new(string.Empty, string.Empty);

    public static readonly AppError NullValue = new("Error.NullValue", "Null value was provided");
}

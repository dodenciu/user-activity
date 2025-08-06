namespace UserActivity.Domain.Abstractions;

public record UseCaseError(string Code, string Name)
{
    public static readonly UseCaseError None = new(string.Empty, string.Empty);

    public static readonly UseCaseError NullValue = new("Error.NullValue", "Null value was provided");
}

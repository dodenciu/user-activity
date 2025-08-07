using UserActivity.Domain.Abstractions;

namespace UserActivity.Domain;

public static class UserErrors
{
    public static readonly AppError NotFound = new(
        "User.NotFound",
        "The user with the specified identifier was not found");
    
    public static readonly AppError SamePassword = new(
        "User.SamePassword",
        "The user password is the same as the previous one");
}

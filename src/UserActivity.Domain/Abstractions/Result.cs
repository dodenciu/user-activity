using System.Diagnostics.CodeAnalysis;

namespace UserActivity.Domain.Abstractions;

public class Result
{
    public Result(bool isSuccess, UseCaseError error)
    {
        if (isSuccess && error != UseCaseError.None 
            || !isSuccess && error == UseCaseError.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public UseCaseError Error { get; }

    public static Result Success() => new(true, UseCaseError.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, UseCaseError.None);

    public static Result Failure(UseCaseError error) => new(false, error);
    public static Result<TValue> Failure<TValue>(UseCaseError error) => new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(UseCaseError.NullValue);
}

public class Result<TValue>(TValue? value, bool isSuccess, UseCaseError error) : Result(isSuccess, error)
{
    [NotNull]
    public TValue Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? resultValue) => Create(resultValue);

    public Result<TValue> ToResult(TValue? resultValue) => Create(resultValue);
}

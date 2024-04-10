using Domain.Exceptions;

namespace Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    public TValue? Value => IsSuccess
        ? _value
        : throw new ResultValueAccessException();

    protected internal Result(bool isSuccess, Error error, TValue? value)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public static implicit operator Result<TValue>(Error error) => Failure<TValue>(error);
}

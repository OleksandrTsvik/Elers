using Domain.Shared;

namespace Domain.Errors;

public interface IDefaultErrors
{
    Error NullValue();
    Error NullResult();
}

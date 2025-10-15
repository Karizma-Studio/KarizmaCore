namespace KarizmaPlatform.Core.Common;

public class ServiceResult<T>
{
    public T? Data { get; }
    public ResultStatus Status { get; }
    public string? Message { get; }

    private ServiceResult(T? data, ResultStatus status, string? message)
    {
        Data = data;
        Status = status;
        Message = message;
    }

    public static ServiceResult<T> Success(T data) =>
        new(data, ResultStatus.Success, null);

    public static ServiceResult<T> NoContent() =>
        new(default, ResultStatus.NoContent, null);

    public static ServiceResult<T> Invalid(string message) =>
        new(default, ResultStatus.Invalid, message);

    public static ServiceResult<T> Forbidden(string message = "Forbidden") =>
        new(default, ResultStatus.Forbidden, message);

    public static ServiceResult<T> Error(string message = "Internal Error") =>
        new(default, ResultStatus.Error, message);
}
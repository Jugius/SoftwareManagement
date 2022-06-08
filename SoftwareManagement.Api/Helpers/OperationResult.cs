
namespace SoftwareManagement.Api.Helpers;
public class OperationResult<T>
{
    public OperationResult(T value)
    {
        Value = value;
        Success = true;
    }
    public OperationResult(Exception error)
    {
        Error = error;
        Success = false;
    }
    public T Value { get; }
    public Exception Error { get; }
    public bool Success { get; }
}

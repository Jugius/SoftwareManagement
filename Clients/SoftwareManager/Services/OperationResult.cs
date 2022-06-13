
namespace SoftwareManager.Services;
public class OperationResult<T>
{
    public OperationResult(T value)
    {
        Value = value;
        Success = true;
    }
    public OperationResult(string error)
    {
        ErrorMessage = error;
        Success = false;
    }
    public T Value { get; }
    public string ErrorMessage { get; }
    public bool Success { get; }
}

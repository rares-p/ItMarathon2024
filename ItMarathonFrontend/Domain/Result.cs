namespace Domain;

public class Result<T> where T : class
{
    public bool Success { get; }
    public T Value { get; }
    public string Error { get; }
    public Result(bool success, T value, string error)
    {
        Success = success;
        Value = value;
        Error = error;
    }
}
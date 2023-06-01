namespace salutaris.Utils;

public class Result<T>
{
    protected Result(bool success)
    {
        IsOk = success;
    }

    public Exception Error { get; private set; } = default!;
    public T Data { get; private set; } = default!;

    public bool IsOk { get; }

    public bool IsErr => !IsOk;


    public static Result<T> Ok(T data)
    {
        return new Result<T>(true)
        {
            Data = data
        };
    }

    public static Result<T> Err(Exception e)
    {
        return new Result<T>(false)
        {
            Error = e
        };
    }
    
    public static Result<T> Err(string message)
    {
        return new Result<T>(false)
        {
            Error = new Exception(message)
        };
    }
}
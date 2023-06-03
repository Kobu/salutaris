using FastEndpoints;
using salutaris.Contracts.Responses;
using salutaris.Utils;

namespace salutaris.Endpoints;

public abstract class ResultEndpoint<TRequest,TResponse>: Endpoint<TRequest,WithError<TResponse>> where TRequest : notnull
{
    public override async Task HandleAsync(TRequest req, CancellationToken ct)
    {
        await HandleResult(req);
    }

    protected abstract Task<bool> HandleResult(TRequest req) ;
   
    protected async Task<bool> HandleOk(TResponse res)
    {
        await SendOkAsync(new WithError<TResponse>
        {
            Success = true,
            Data = res
        });
        return true;
    }

    protected async Task<bool> HandleErr<T>(Result<T> res, int statusCode = StatusCodes.Status400BadRequest)
    {
        await SendAsync(new()
        {
            Success = false,
            ErrorMessage = res.Error.Message
        }, statusCode);

        return false;
    }
    
    protected async Task<bool> HandleErr(string message, int statusCode = StatusCodes.Status400BadRequest)
    {
        await SendAsync(new()
        {
            Success = false,
            ErrorMessage =message
        }, statusCode);

        return false;
    }
    
}

public abstract class ResultEndpointWithoutRequest<TResponse> : ResultEndpoint<EmptyRequest, TResponse>
{
    protected abstract  Task<bool> HandleResult();
    protected override async Task<bool> HandleResult(EmptyRequest req)
    {
        return await HandleResult();
    }
}
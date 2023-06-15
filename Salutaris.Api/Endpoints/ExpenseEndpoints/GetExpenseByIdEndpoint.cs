#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.ExpenseEndpoints;

public class GetExpenseByIdEndpoint : ResultEndpoint<GetExpenseByIdRequest, ExpenseResponse>
{
    private readonly IExpenseService _expenseService;

    public GetExpenseByIdEndpoint(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public override void Configure()
    {
        Get("expense/{id:guid}");
        AllowAnonymous();
    }

    protected override async Task<bool> HandleResult(GetExpenseByIdRequest req)
    {
        var result = await _expenseService.GetExpenseById(req.Id);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        return await HandleOk(result.Data.ToResponse());
    }
}
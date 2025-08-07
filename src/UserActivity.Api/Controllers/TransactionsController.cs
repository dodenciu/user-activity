using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserActivity.Application.Features.GetTotalTransactionAmount;
using UserActivity.Application.Features.ListTransactions;

namespace UserActivity.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ISender _sender;
    
    public TransactionsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TransactionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTransactionsAsync(
        [FromQuery] decimal? greaterThanAmount, CancellationToken cancellationToken)
    {
        var query = new ListTransactionsQuery(greaterThanAmount);
        
        IReadOnlyList<TransactionResponse> transactions = await _sender
            .Send(query, cancellationToken)
            .ConfigureAwait(false);
        
        return Ok(transactions);
    }
    
    [HttpGet("total-amount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalTransactionAmountAsync(
        [FromQuery] string? transactionType, CancellationToken cancellationToken)
    {
        var query = new GetTotalTransactionAmountQuery(transactionType);
        
        decimal totalTransactionAmount = await _sender
            .Send(query, cancellationToken)
            .ConfigureAwait(false);
        
        return Ok(totalTransactionAmount);
    }
}

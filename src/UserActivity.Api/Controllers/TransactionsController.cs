using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserActivity.Application.Features.GetTotalTransactionAmount;
using UserActivity.Application.Features.ListTransactions;

namespace UserActivity.Api.Controllers;

/// <summary>
/// Controller for managing transaction-related operations.
/// </summary>
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ISender _sender;
    
    /// <summary>
    /// </summary>
    public TransactionsController(ISender sender)
    {
        _sender = sender;
    }
    
    /// <summary>
    /// Retrieves a list of transactions, optionally filtered by a minimum amount.
    /// </summary>
    /// <param name="greaterThanAmount">Optional. Only transactions with an amount greater than this value will be returned.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of transactions matching the filter.</returns>
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
    
    /// <summary>
    /// Gets the total amount of transactions, optionally filtered by transaction type.
    /// </summary>
    /// <param name="transactionType">Optional. The type of transaction to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The total transaction amount.</returns>
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

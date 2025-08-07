using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserActivity.Application.Common;
using UserActivity.Application.Features.ChangeUserPassword;
using UserActivity.Application.Features.CreateTransaction;
using UserActivity.Application.Features.CreateUser;
using UserActivity.Application.Features.DeleteUser;
using UserActivity.Application.Features.GetUser;
using UserActivity.Application.Features.ListUsers;
using UserActivity.Application.Features.UpdateUserDetails;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    
    public UsersController(ISender sender)
    {
        ArgumentNullException.ThrowIfNull(sender);
        
        _sender = sender;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUserAsync(
        [FromBody] CreateUserCommand command, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        await _sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);

        return NoContent();
    }
    
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserAsync(
        [FromRoute] Guid userId, 
        CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(userId);
        
        Result<UserResponse> userResponse = await _sender
            .Send(query, cancellationToken)
            .ConfigureAwait(false);
        
        if (userResponse.IsFailure)
        {
            return NotFound(userResponse.Error);
        }

        return Ok(userResponse.Value);
    }
    
    [HttpPut("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserDetailsCommand updateUserDetailsCommand, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(updateUserDetailsCommand);
        
        var command = new UpdateUserCommand(userId, updateUserDetailsCommand);
        
        await _sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);

        return NoContent();
    }
    
    [HttpPatch("{userId:guid}/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeUserPasswordAsync(
        [FromRoute] Guid userId,
        [FromBody] ChangePasswordCommand changePasswordCommand,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(changePasswordCommand);
    
        var command = new ChangeUserPasswordCommand(userId, changePasswordCommand);
    
        Result result = await _sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);
    
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return NoContent();
    }
    
    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserAsync(
        [FromRoute] Guid userId, 
        CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(userId);
        
        Result userResponse = await _sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);
        
        if (userResponse.IsFailure)
        {
            return NotFound(userResponse.Error);
        }

        return NoContent();
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListUsersAsync(CancellationToken cancellationToken)
    {
        var query = new ListUsersQuery();
        
        IReadOnlyList<UserResponse> users = await _sender
            .Send(query, cancellationToken)
            .ConfigureAwait(false);
        
        return Ok(users);
    }
    
    [HttpPost("{userId:guid}/transactions")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateTransactionAsync(
        [FromRoute] Guid userId, 
        [FromBody] CreateTransactionCommand createTransactionCommand,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserTransactionCommand(userId, createTransactionCommand);
        
        Result transactionResponse = await _sender
            .Send(command, cancellationToken)
            .ConfigureAwait(false);
        
        if (transactionResponse.IsFailure)
        {
            return NotFound(transactionResponse.Error);
        }

        return NoContent();
    }
}

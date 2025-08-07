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

/// <summary>
/// Controller for managing user-related operations.
/// </summary>
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    
    /// <summary>
    /// </summary>
    public UsersController(ISender sender)
    {
        ArgumentNullException.ThrowIfNull(sender);
        
        _sender = sender;
    }
    
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="command">The user creation command containing user details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">User created successfully.</response>
    /// <response code="400">Bad request if the user data is invalid.</response>
    /// <response code="500">Internal server error.</response>
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
    
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The user details if found.</returns>
    /// <response code="200">User found and returned successfully.</response>
    /// <response code="404">User not found.</response>
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
    
    /// <summary>
    /// Updates an existing user's details.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to update.</param>
    /// <param name="updateUserDetailsCommand">The user update command containing new user details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">User updated successfully.</response>
    /// <response code="400">Bad request if the user data is invalid.</response>
    /// <response code="500">Internal server error.</response>
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
    
    /// <summary>
    /// Changes a user's password.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="changePasswordCommand">The password change command containing the new password.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">Password changed successfully.</response>
    /// <response code="400">Bad request if the password data is invalid.</response>
    /// <response code="404">User not found.</response>
    /// <response code="500">Internal server error.</response>
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
    
    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">User deleted successfully.</response>
    /// <response code="404">User not found.</response>
    /// <response code="500">Internal server error.</response>
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
    
    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of all users.</returns>
    /// <response code="200">Users retrieved successfully.</response>
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
    
    /// <summary>
    /// Creates a new transaction for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="createTransactionCommand">The transaction creation command containing transaction details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="200">Transaction created successfully.</response>
    /// <response code="404">User not found.</response>
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

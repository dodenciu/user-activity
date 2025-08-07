using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserActivity.Application.CreateUser;
using UserActivity.Application.GetUser;
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
}

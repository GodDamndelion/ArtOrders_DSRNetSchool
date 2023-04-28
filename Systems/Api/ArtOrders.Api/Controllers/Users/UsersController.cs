namespace ArtOrders.API.Controllers;

using AutoMapper;
using ArtOrders.API.Controllers.Models;
using ArtOrders.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/v{version:apiVersion}/users")]
[ApiController]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<UsersController> logger;
    private readonly IUserService userService;

    public UsersController(IMapper mapper, ILogger<UsersController> logger, IUserService userService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userService = userService;
    }

    [HttpPost("")]
    public async Task<UserAccountResponse> Register([FromQuery] RegisterUserAccountRequest request)
    {
        var user = await userService.Create(mapper.Map<RegisterUserAccountModel>(request));

        var response = mapper.Map<UserAccountResponse>(user);

        return response;
    }

    /// <summary>
    /// Get artists
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count of elements on the page</param>
    /// <response code="200">List of UserAccountResponses</response>
    [ProducesResponseType(typeof(IEnumerable<UserAccountResponse>), 200)] //Показывает, что придёт по такому коду
    [HttpGet("")]
    public async Task<IEnumerable<UserAccountResponse>> GetArtists([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var artists = await userService.GetArtists(offset, limit);
        var response = mapper.Map<IEnumerable<UserAccountResponse>>(artists);

        return response;
    }

    /// <summary>
    /// Get user by Id
    /// </summary>
    /// <response code="200">UserAccountResponse></response>
    [ProducesResponseType(typeof(UserAccountResponse), 200)]
    [HttpGet("{id}")]
    public async Task<UserAccountResponse> GetUserById([FromRoute] Guid id)
    {
        var user = await userService.GetUser(id);
        var response = mapper.Map<UserAccountResponse>(user);

        return response;
    }

    /// <summary>
    /// Get current user
    /// </summary>
    /// <response code="200">UserAccountResponse></response>
    [ProducesResponseType(typeof(UserAccountResponse), 200)]
    [Authorize]
    [HttpGet("current")]
    public async Task<UserAccountResponse> GetCurrentUser()
    {
        bool success = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
        if (!success)
        {
            throw new Exception("Current user not found!");
        }
        var user = await userService.GetUser(userId);
        var response = mapper.Map<UserAccountResponse>(user);

        return response;
    }
}

namespace ArtOrders.API.Controllers;

using AutoMapper;
using ArtOrders.API.Controllers.Models;
using ArtOrders.Services.Users;
using Microsoft.AspNetCore.Mvc;

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
}

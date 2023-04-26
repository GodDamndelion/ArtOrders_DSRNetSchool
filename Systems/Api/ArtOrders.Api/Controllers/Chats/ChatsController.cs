namespace ArtOrders.Api.Controllers;

using AutoMapper;
using ArtOrders.Api.Controllers.Models;
using ArtOrders.Common.Responses;
//using ArtOrders.Common.Security;
using ArtOrders.Services.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


/// <summary>
/// Chats controller
/// </summary>
/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/chats")]
[Authorize]
[ApiController]
[ApiVersion("1.0")]
public class ChatsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<ChatsController> logger;
    private readonly IChatService chatService;

    public ChatsController(IMapper mapper, ILogger<ChatsController> logger, IChatService chatService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.chatService = chatService;
    }


    /// <summary>
    /// Get chats
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count of elements on the page</param>
    /// <response code="200">List of ChatRequestResponses</response>
    [ProducesResponseType(typeof(IEnumerable<ChatRequestResponse>), 200)] //Показывает, что придёт по такому коду
    // TODO: Посмотреть авторизацию чатов
    //[Authorize(Policy = AppScopes.ChatsRead)]
    [HttpGet("")]
    public async Task<IEnumerable<ChatRequestResponse>> GetChats([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var chats = await chatService.GetChats(offset, limit);
        var response = mapper.Map<IEnumerable<ChatRequestResponse>>(chats);

        return response;
    }

    /// <summary>
    /// Get chat by Id
    /// </summary>
    /// <response code="200">ChatRequestResponse></response>
    [ProducesResponseType(typeof(ChatRequestResponse), 200)]
    // TODO: Посмотреть авторизацию чатов
    //[Authorize(Policy = AppScopes.ChatsRead)]
    [HttpGet("{id}")]
    public async Task<ChatRequestResponse> GetChatById([FromRoute] int id)
    {
        var chat = await chatService.GetChat(id);
        var response = mapper.Map<ChatRequestResponse>(chat);

        return response;
    }

    /// <summary>
    /// Create chat with current user ID
    /// </summary>
    [HttpPost("")]
    // TODO: Посмотреть авторизацию чатов
    //[Authorize(Policy = AppScopes.ChatsWrite)]
    public async Task<ChatRequestResponse> AddChat([FromBody] ChatRequestResponse request)
    {
        var model = mapper.Map<ChatModel>(request);
        bool success = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid customerId);
        if (!success)
        {
            throw new Exception("User not found! Chat creation failed!");
        }
        model.CustomerId = customerId;
        var chat = await chatService.AddChat(model);
        var response = mapper.Map<ChatRequestResponse>(chat);

        return response;
    }

    /// <summary>
    /// Create chat with given customer ID
    /// </summary>
    [HttpPost("order")]
    // TODO: Посмотреть авторизацию чатов
    //[Authorize(Policy = AppScopes.ChatsWrite)]
    public async Task<ChatRequestResponse> AddOrderChat([FromBody] ChatRequestResponse request)
    {
        var model = mapper.Map<ChatModel>(request);
        var chat = await chatService.AddChat(model);
        var response = mapper.Map<ChatRequestResponse>(chat);

        return response;
    }

    /// <summary>
    /// Update chat
    /// </summary>
    [HttpPut("{id}")]
    // TODO: Посмотреть авторизацию чатов
    //[Authorize(Policy = AppScopes.ChatsWrite)]
    public async Task<IActionResult> UpdateChat([FromRoute] int id, [FromBody] ChatRequestResponse request)
    {
        var model = mapper.Map<ChatModel>(request);
        await chatService.UpdateChat(id, model);

        return Ok();
    }

    /// <summary>
    /// Delete chat
    /// </summary>
    [HttpDelete("{id}")]
    // TODO: Посмотреть авторизацию чатов
    //[Authorize(Policy = AppScopes.ChatsWrite)]
    public async Task<IActionResult> DeleteChat([FromRoute] int id)
    {
        await chatService.DeleteChat(id);

        return Ok();
    }
}

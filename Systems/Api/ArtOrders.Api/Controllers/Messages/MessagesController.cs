namespace ArtOrders.API.Controllers;

using AutoMapper;
using ArtOrders.API.Controllers.Models;
using ArtOrders.Common.Responses;
//using ArtOrders.Common.Security;
using ArtOrders.Services.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


/// <summary>
/// Messages controller
/// </summary>
/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/messages")]
[Authorize]
[ApiController]
[ApiVersion("1.0")]
public class MessagesController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<MessagesController> logger;
    private readonly IMessageService messageService;

    public MessagesController(IMapper mapper, ILogger<MessagesController> logger, IMessageService messageService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.messageService = messageService;
    }


    /// <summary>
    /// Get messages
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count elements on the page</param>
    /// <response code="200">List of MessageResponses</response>
    [ProducesResponseType(typeof(IEnumerable<MessageResponse>), 200)]
    //[Authorize(Policy = AppScopes.MessagesRead)]
    [HttpGet("all")]
    public async Task<IEnumerable<MessageResponse>> GetMessages([FromQuery] int offset = 0, [FromQuery] int limit = 100)
    {
        var messages = await messageService.GetMessages(offset, limit);
        var response = mapper.Map<IEnumerable<MessageResponse>>(messages);

        return response;
    }

    /// <summary>
    /// Get messages by chat Id
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count elements on the page</param>
    /// <param name="chatId">Chat Id</param>
    /// <response code="200">List of MessageResponses</response>
    [ProducesResponseType(typeof(IEnumerable<MessageResponse>), 200)] //Показывает, что придёт по такому коду
    //[Authorize(Policy = AppScopes.MessagesRead)]
    [HttpGet("")]
    public async Task<IEnumerable<MessageResponse>> GetChatMessages([FromQuery] int chatId, [FromQuery] int offset = 0, [FromQuery] int limit = 100)
    {
        var messages = await messageService.GetMessages(offset, limit);
        messages = messages.Where(m => m.ChatId == chatId);
        var response = mapper.Map<IEnumerable<MessageResponse>>(messages);

        return response;
    }

    /// <summary>
    /// Get message by Id
    /// </summary>
    /// <response code="200">MessageResponse></response>
    [ProducesResponseType(typeof(MessageResponse), 200)]
    //[Authorize(Policy = AppScopes.MessagesRead)]
    [HttpGet("{id}")]
    public async Task<MessageResponse> GetMessageById([FromRoute] int id)
    {
        var message = await messageService.GetMessage(id);
        var response = mapper.Map<MessageResponse>(message);

        return response;
    }

    /// <summary>
    /// Create message
    /// </summary>
    [HttpPost("")]
    //[Authorize(Policy = AppScopes.MessagesWrite)]
    public async Task<MessageResponse> AddMessage([FromBody] AddMessageRequest request)
    {
        var model = mapper.Map<AddMessageModel>(request);
        bool success = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
        if (!success)
        {
            throw new Exception("User not found! Message creation failed!");
        }
        model.UserId = userId;
        model.Date = DateTime.UtcNow;
        var message = await messageService.AddMessage(model);
        var response = mapper.Map<MessageResponse>(message);

        return response;
    }

    [HttpPut("{id}")]
    //[Authorize(Policy = AppScopes.MessagesWrite)]
    public async Task<IActionResult> UpdateMessage([FromRoute] int id, [FromBody] UpdateMessageRequest request)
    {
        var model = mapper.Map<UpdateMessageModel>(request);
        await messageService.UpdateMessage(id, model);

        return Ok();
    }

    [HttpDelete("{id}")]
    //[Authorize(Policy = AppScopes.MessagesWrite)]
    public async Task<IActionResult> DeleteMessage([FromRoute] int id)
    {
        await messageService.DeleteMessage(id);

        return Ok();
    }
}

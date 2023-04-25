namespace ArtOrders.API.Controllers;

using AutoMapper;
using ArtOrders.API.Controllers.Models;
using ArtOrders.Common.Responses;
using ArtOrders.Common.Security;
using ArtOrders.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


/// <summary>
/// Orders controller
/// </summary>
/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/orders")]
[Authorize]
[ApiController]
[ApiVersion("1.0")]
public class OrdersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<OrdersController> logger;
    private readonly IOrderService orderService;

    public OrdersController(IMapper mapper, ILogger<OrdersController> logger, IOrderService orderService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.orderService = orderService;
    }


    /// <summary>
    /// Get orders
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count elements on the page</param>
    /// <response code="200">List of OrderResponses</response>
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), 200)] //Показывает, что придёт по такому коду
    [Authorize(Policy = AppScopes.OrdersRead)]
    [HttpGet("")]
    public async Task<IEnumerable<OrderResponse>> GetOrders([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var orders = await orderService.GetOrders(offset, limit);
        var response = mapper.Map<IEnumerable<OrderResponse>>(orders);

        return response;
    }

    /// <summary>
    /// Get orders by Id
    /// </summary>
    /// <response code="200">OrderResponse></response>
    [ProducesResponseType(typeof(OrderResponse), 200)]
    [Authorize(Policy = AppScopes.OrdersRead)]
    [HttpGet("{id}")]
    public async Task<OrderResponse> GetOrderById([FromRoute] int id)
    {
        var order = await orderService.GetOrder(id);
        var response = mapper.Map<OrderResponse>(order);

        return response;
    }

    /// <summary>
    /// Create order
    /// </summary>
    [HttpPost("")]
    [Authorize(Policy = AppScopes.OrdersWrite)]
    public async Task<OrderResponse> AddOrder([FromBody] AddOrderRequest request)
    {
        var model = mapper.Map<AddOrderModel>(request);
        bool success = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid customerId);
        if (!success)
        {
            throw new Exception("User not found! Order creation failed!");
        }
        model.CustomerId = customerId;
        var order = await orderService.AddOrder(model);
        var response = mapper.Map<OrderResponse>(order);

        return response;
    }

    [HttpPut("{id}")]
    [Authorize(Policy = AppScopes.OrdersWrite)]
    public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderRequest request)
    {
        var model = mapper.Map<UpdateOrderModel>(request);
        await orderService.UpdateOrder(id, model);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = AppScopes.OrdersWrite)]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        await orderService.DeleteOrder(id);

        return Ok();
    }
}

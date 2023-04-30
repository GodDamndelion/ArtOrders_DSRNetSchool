namespace ArtOrders.Services.Messages;

using AutoMapper;
using ArtOrders.Common.Exceptions;
using ArtOrders.Common.Validator;
using ArtOrders.Context;
using ArtOrders.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class MessageService : IMessageService
{

    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddMessageModel> addMessageModelValidator;
    private readonly IModelValidator<UpdateMessageModel> updateMessageModelValidator;
    private readonly ILogger<MessageService> logger;

	public MessageService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddMessageModel> addMessageModelValidator,
        IModelValidator<UpdateMessageModel> updateMessageModelValidator,
        ILogger<MessageService> logger
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addMessageModelValidator = addMessageModelValidator;
        this.updateMessageModelValidator = updateMessageModelValidator;
        this.logger = logger;
    }

    public async Task<IEnumerable<MessageModel>> GetMessages(int offset = 0, int limit = 100)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var messages = context
            .Messages
            .Include(x => x.Image)
            .AsQueryable();

        messages = messages
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await messages.ToListAsync()).Select(message => mapper.Map<MessageModel>(message));

        return data;
    }

    public async Task<MessageModel> GetMessage(int id)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var message = await context.Messages.Include(x => x.Image).FirstOrDefaultAsync(x => x.Id.Equals(id));

        var data = mapper.Map<MessageModel>(message);

        return data;
    }

    public async Task<MessageModel> AddMessage(AddMessageModel model)
    {
        addMessageModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var message = mapper.Map<Message>(model);

        await context.Messages.AddAsync(message);
        context.SaveChanges();

        return mapper.Map<MessageModel>(message);
    }

    public async Task UpdateMessage(int messageId, UpdateMessageModel model)
    {
        updateMessageModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var message = await context.Messages.FirstOrDefaultAsync(x => x.Id.Equals(messageId));

        ProcessException.ThrowIf(() => message is null, $"The message (id: {messageId}) was not found");

        message = mapper.Map(model, message);

        context.Messages.Update(message!);
        context.SaveChanges();
    }

    public async Task DeleteMessage(int messageId)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var message = await context.Messages.FirstOrDefaultAsync(x => x.Id.Equals(messageId))
            ?? throw new ProcessException($"The message (id: {messageId}) was not found");

        context.Remove(message);
        context.SaveChanges();
    }
}

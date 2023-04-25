namespace ArtOrders.Services.Chats;

using AutoMapper;
using ArtOrders.Common.Exceptions;
using ArtOrders.Common.Validator;
using ArtOrders.Context;
using ArtOrders.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ChatService : IChatService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<ChatModel> chatModelValidator;
    private readonly ILogger<ChatService> logger;

    public ChatService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<ChatModel> chatModelValidator,
        ILogger<ChatService> logger
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.chatModelValidator = chatModelValidator;
        this.logger = logger;
    }

    public async Task<IEnumerable<ChatModel>> GetChats(int offset = 0, int limit = 10)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var chats = context
            .Chats
            .AsQueryable();

        chats = chats
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await chats.ToListAsync()).Select(chat => mapper.Map<ChatModel>(chat));

        return data;
    }

    public async Task<ChatModel> GetChat(int id)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var chat = await context.Chats.FirstOrDefaultAsync(x => x.Id.Equals(id));

        var data = mapper.Map<ChatModel>(chat);

        return data;
    }

    public async Task<ChatModel> AddChat(ChatModel model)
    {
        chatModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var chat = mapper.Map<Chat>(model);

        await context.Chats.AddAsync(chat);
        context.SaveChanges();

        return mapper.Map<ChatModel>(chat);
    }

    public async Task UpdateChat(int chatId, ChatModel model)
    {
        chatModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var chat = await context.Chats.FirstOrDefaultAsync(x => x.Id.Equals(chatId));

        ProcessException.ThrowIf(() => chat is null, $"The chat (id: {chatId}) was not found");

        chat = mapper.Map(model, chat);

        context.Chats.Update(chat);
        context.SaveChanges();
    }

    public async Task DeleteChat(int chatId)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var chat = await context.Chats.FirstOrDefaultAsync(x => x.Id.Equals(chatId))
            ?? throw new ProcessException($"The chat (id: {chatId}) was not found");

        context.Remove(chat);
        context.SaveChanges();
    }
}

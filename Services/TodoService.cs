using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Models;
using TodoApi.Settings;

namespace TodoApi.Services;

public class TodoService
{
    private readonly IMongoCollection<TodoItem> _todosCollection;

    public TodoService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _todosCollection = mongoDatabase.GetCollection<TodoItem>("Todos");
    }

    public async Task<List<TodoItem>> GetAllAsync() =>
        await _todosCollection.Find(_ => true).ToListAsync();

    public async Task<TodoItem?> GetByIdAsync(string id) =>
        await _todosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TodoItem newTodo) =>
        await _todosCollection.InsertOneAsync(newTodo);

    public async Task UpdateAsync(string id, TodoItem updatedTodo) =>
        await _todosCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);

    public async Task DeleteAsync(string id) =>
        await _todosCollection.DeleteOneAsync(x => x.Id == id);
}

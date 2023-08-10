using EJournal.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EJournal.MongoPersistence.Context;

public sealed class DataContext
{
    private readonly MongoClient _mongoClient;
    public readonly IMongoDatabase MongoDatabase;
    public IMongoCollection<User> Users { get; set; }
    public IMongoCollection<WorkDay> WorkDays { get; set; }
    public IMongoCollection<WorkTime> WorkTimes { get; set; }

    public DataContext(IOptions<MongoDbSettings> settings)
    {
        _mongoClient = new MongoClient(settings.Value.ConnectionString);
        MongoDatabase = _mongoClient.GetDatabase(settings.Value.DatabaseName);
        
        Users = MongoDatabase.GetCollection<User>(settings.Value.UserCollectionName);
        WorkDays = MongoDatabase.GetCollection<WorkDay>(settings.Value.WorkDaysCollectionName);
        WorkTimes = MongoDatabase.GetCollection<WorkTime>(settings.Value.WorkTimeCollectionName);
    }
}
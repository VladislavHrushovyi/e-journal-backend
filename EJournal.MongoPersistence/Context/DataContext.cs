using EJournal.Domain.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

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
        this._mongoClient = new MongoClient(settings.Value.ConnectionString);
        MongoDatabase = _mongoClient.GetDatabase(settings.Value.DatabaseName);

        this.Users = MongoDatabase.GetCollection<User>(settings.Value.UserCollectionName);
        this.WorkDays = MongoDatabase.GetCollection<WorkDay>(settings.Value.WorkDaysCollectionName);
        this.WorkTimes = MongoDatabase.GetCollection<WorkTime>(settings.Value.WorkTimeCollectionName);
    }
}
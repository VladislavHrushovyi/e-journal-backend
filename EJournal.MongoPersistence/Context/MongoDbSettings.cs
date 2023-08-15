namespace EJournal.MongoPersistence.Context;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string UserCollectionName { get; set; }
    public string WeeklyScheduleCollectionName { get; set; }
    public string WorkDaysCollectionName { get; set; }
}
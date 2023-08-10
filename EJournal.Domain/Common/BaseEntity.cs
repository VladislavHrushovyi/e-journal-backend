using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EJournal.Domain.Common;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly UpdateAt { get; set; }
}
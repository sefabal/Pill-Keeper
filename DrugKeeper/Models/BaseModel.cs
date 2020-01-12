using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace DrugKeeper.Models
{
    public class BaseModel
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

    }
}

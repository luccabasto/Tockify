using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace Tockify.Domain.Models
{
    public class CounterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        [BsonElement("sequence_value")]
        public int SequenceValue { get; set; }
    }
}

// Essa classe representa um contador genérico que pode ser usado para gerar IDs únicos ou sequências numéricas em uma coleção MongoDB.
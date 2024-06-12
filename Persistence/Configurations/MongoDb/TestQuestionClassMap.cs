using Domain.Entities;
using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Persistence.Configurations.MongoDb;

public static class TestQuestionClassMap
{
    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<TestQuestion>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIsRootClass(true);

            classMap
                .MapMember(testQuestion => testQuestion.Type)
                .SetSerializer(new EnumSerializer<TestQuestionType>(BsonType.String));
        });

        BsonClassMap.RegisterClassMap<TestQuestionInput>();

        BsonClassMap.RegisterClassMap<TestQuestionSingleChoice>();

        BsonClassMap.RegisterClassMap<TestQuestionMultipleChoice>();

        BsonClassMap.RegisterClassMap<TestQuestionMatching>();
    }
}

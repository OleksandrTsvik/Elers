using Domain.Entities;
using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Persistence.Configurations.MongoDb;

public static class TestSessionClassMap
{
    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<TestSession>(classMap =>
        {
            classMap.AutoMap();
        });

        BsonClassMap.RegisterClassMap<TestSessionAnswer>(answer =>
        {
            answer.AutoMap();
            answer.SetIsRootClass(true);

            answer
                .MapMember(answer => answer.QuestionType)
                .SetSerializer(new EnumSerializer<TestQuestionType>(BsonType.String));
        });

        BsonClassMap.RegisterClassMap<TestSessionAnswerInput>();

        BsonClassMap.RegisterClassMap<TestSessionAnswerSingleChoice>();

        BsonClassMap.RegisterClassMap<TestSessionAnswerMultipleChoice>();
    }
}

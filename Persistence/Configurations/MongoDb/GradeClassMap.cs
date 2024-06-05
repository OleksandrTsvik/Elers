using Domain.Entities;
using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Persistence.Configurations.MongoDb;

public static class GradeClassMap
{
    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<Grade>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIsRootClass(true);

            classMap
                .MapMember(grade => grade.Type)
                .SetSerializer(new EnumSerializer<GradeType>(BsonType.String));
        });

        BsonClassMap.RegisterClassMap<GradeAssignment>();

        BsonClassMap.RegisterClassMap<GradeTest>();
    }
}

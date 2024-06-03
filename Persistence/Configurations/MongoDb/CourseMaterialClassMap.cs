using Domain.Entities;
using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Persistence.Configurations.MongoDb;

public static class CourseMaterialClassMap
{
    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<CourseMaterial>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIsRootClass(true);

            classMap
                .MapMember(courseMaterial => courseMaterial.Type)
                .SetSerializer(new EnumSerializer<CourseMaterialType>(BsonType.String));
        });

        BsonClassMap.RegisterClassMap<CourseMaterialContent>();

        BsonClassMap.RegisterClassMap<CourseMaterialLink>();

        BsonClassMap.RegisterClassMap<CourseMaterialFile>();

        BsonClassMap.RegisterClassMap<CourseMaterialAssignment>();
    }
}

using Domain.Entities;
using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Persistence.Configurations.MongoDb;

public static class SubmittedAssignmentClassMap
{
    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<SubmittedAssignment>(classMap =>
        {
            classMap.AutoMap();

            classMap
                .MapMember(submittedAssignment => submittedAssignment.Status)
                .SetSerializer(new EnumSerializer<SubmittedAssignmentStatus>(BsonType.String));
        });
    }
}

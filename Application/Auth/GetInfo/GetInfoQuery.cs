using Application.Common.Messaging;

namespace Application.Auth.GetInfo;

public record GetInfoQuery() : IQuery<InfoResponse>;

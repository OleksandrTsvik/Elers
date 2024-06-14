using Application.Common.Interfaces;
using Application.Common.Messaging;

namespace Application.Profile.ChangeAvatar;

public record ChangeAvatarCommand(IFile Avatar) : ICommand<string>;

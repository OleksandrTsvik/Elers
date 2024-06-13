using Application.Common.Messaging;

namespace Application.CourseMaterials.MoveMaterialToAnotherTab;

public record MoveMaterialToAnotherTabCommand(Guid MaterialId, Guid NewCourseTabId) : ICommand;

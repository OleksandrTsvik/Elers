namespace Domain.Rules;

public static class CourseMaterialRules
{
    public const int MinTitleLinkLength = 2;
    public const int MaxTitleLinkLength = 64;

    public const int MaxLinkLength = 2048;

    public const int MinTitleFileLength = 2;
    public const int MaxTitleFileLength = 64;

    public const int MinTitleAssignmentLength = 2;
    public const int MaxTitleAssignmentLength = 64;

    public const int MinTitleTestLength = 2;
    public const int MaxTitleTestLength = 64;
}

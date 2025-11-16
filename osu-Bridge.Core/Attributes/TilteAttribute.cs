namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class TitleAttribute(string title) : Attribute
{
    public string Title { get; } = title;
}

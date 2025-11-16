namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DependsOnAttribute(string propertyName) : Attribute
{
    public string PropertyName { get; } = propertyName;
}

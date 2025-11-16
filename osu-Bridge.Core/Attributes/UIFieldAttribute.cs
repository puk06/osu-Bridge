namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class UIFieldAttribute(string label = "") : Attribute
{
    public string Label { get; } = label;
}

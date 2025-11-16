namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PlaceHolderAttribute(string text) : Attribute
{
    public string PlaceHolderText { get; } = text;
}
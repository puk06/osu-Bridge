namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ChoiceAttribute(string[] choices) : Attribute
{
    public string[] Choices { get; set; } = choices;
}
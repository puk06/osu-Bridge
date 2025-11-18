namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FieldWidthAttribute(int fieldWidth) : Attribute
{
    public int FieldWidth { get; } = fieldWidth;
}

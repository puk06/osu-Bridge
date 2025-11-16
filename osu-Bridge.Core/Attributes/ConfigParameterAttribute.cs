namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ConfigParameterAttribute(string parameterName) : Attribute
{
    public string ParameterName { get; } = parameterName;
}

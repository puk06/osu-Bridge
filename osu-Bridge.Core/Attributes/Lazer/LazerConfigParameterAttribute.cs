using osu_Bridge.Core.Models.Lazer;

namespace osu_Bridge.Core.Attributes.Lazer;

[AttributeUsage(AttributeTargets.Property)]
public class LazerConfigParameterAttribute(string parameterName, LazerConfigurationType lazerConfigurationType, string parameterFormat = "") : Attribute
{
    public string ParameterName { get; set; } = parameterName;
    public LazerConfigurationType LazerConfigurationType { get; set; } = lazerConfigurationType;
    public string ParameterFormat { get; set; } = parameterFormat;
}

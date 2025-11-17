using osu_Bridge.Core.Models.Lazer;

namespace osu_Bridge.Core.Attributes.Lazer;

[AttributeUsage(AttributeTargets.Property)]
public class LazerConfigurationAttribute(string parameterName, LazerConfigurationType lazerConfigurationType, string textFormat = "") : Attribute
{
    public string ParameterName { get; set; } = parameterName;
    public LazerConfigurationType LazerConfigurationType { get; set; } = lazerConfigurationType;
    public string TextFormat { get; set; } = textFormat;
}

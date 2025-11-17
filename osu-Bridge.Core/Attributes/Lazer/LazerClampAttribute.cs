namespace osu_Bridge.Core.Attributes.Lazer;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class LazerValueClampAttribute() : Attribute
{
    // このCOnfigの値は設定されるときに0-1にクランプされます
}

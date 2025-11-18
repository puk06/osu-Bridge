namespace osu_Bridge.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ConfidentialAttribute : Attribute
{
    // 機密情報かどうかを表すものです
    // CUIやGUIに表示されるときに隠されます
}

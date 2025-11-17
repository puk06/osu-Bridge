namespace osu_Bridge.Core.Attributes.Lazer;

[AttributeUsage(AttributeTargets.Property)]
public class LazerNotSupported() : Attribute
{
    // LazerがこのConfigの値の変更に対応していないときに使用されます。
}

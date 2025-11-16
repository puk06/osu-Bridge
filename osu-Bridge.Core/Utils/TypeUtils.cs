namespace osu_Bridge.Core.Utils;

internal static class TypeUtils
{
    internal static bool EditInternal(Type? type, string key, string value)
    {
        try
        {
            if (type == null) return false;

            foreach (var property in type.GetProperties())
            {
                if (property.Name != key) continue;

                property.SetValue(type, Convert.ChangeType(value, property.PropertyType), null);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}

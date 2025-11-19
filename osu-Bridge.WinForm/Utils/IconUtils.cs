using osu_Bridge.Core.Utils;

namespace osu_Bridge.WinForm.Utils;

internal class FormIconUtils
{
    internal static Icon GetSoftwareIcon()
    {
        using var memoryStream = new MemoryStream(IconUtils.SoftwareIcon);
        Icon icon = new(memoryStream);

        return icon;
    }
}

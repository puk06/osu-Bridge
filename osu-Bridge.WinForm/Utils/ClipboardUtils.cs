using System.Runtime.InteropServices;

namespace osu_Bridge.WinForm.Utils;

internal static class ClipboardUtils
{
    internal static void Copy(string text)
    {
        try
        {
            Clipboard.SetText(text);
        }
        catch (Exception ex)
        {
            if (ex is ExternalException) return;
            FormUtils.ShowError($"コピーに失敗しました。\nエラー: {ex.Message}");
        }
    }
}

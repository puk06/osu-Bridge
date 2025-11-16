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
        catch (Exception error)
        {
            if (error is ExternalException) return;
            MessageBox.Show(string.Format("コピーに失敗しました。\n{0}", error), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

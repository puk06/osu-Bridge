namespace osu_Bridge.WinForm.Utils;

internal static class FormUtils
{
    internal static (bool result, string value) OpenFolderDialog(string title)
    {
        FolderBrowserDialog folderBrowserDialog = new()
        {
            UseDescriptionForTitle = true,
            Description = title
        };
        if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return (false, string.Empty);

        return (true, folderBrowserDialog.SelectedPath);
    }

    internal static void AddMenuItem(ToolStripMenuItem parent, string text, int index, string confirmMessage, Action<int> action)
    {
        var item = parent.DropDownItems.Add(text);
        item.Tag = index;

        item.Click += (s, e) =>
        {
            if (s is not ToolStripItem menuItem || menuItem.Tag is not int selectedIndex) return;

            var result = ShowConfirm(confirmMessage);
            if (!result) return;

            action(selectedIndex);
        };
    }

    internal static bool ShowConfirm(string message, string title = "確認")
    {
        DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        return result == DialogResult.Yes;
    }

    internal static void ShowError(string message, string title = "エラー")
        => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

    internal static void ShowInfo(string message, string title = "情報")
        => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
}

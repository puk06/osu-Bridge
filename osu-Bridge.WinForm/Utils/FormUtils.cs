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

            var result = MessageBox.Show(confirmMessage, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result != DialogResult.Yes) return;

            action(selectedIndex);
        };
    }
}

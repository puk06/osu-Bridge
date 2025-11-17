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
}

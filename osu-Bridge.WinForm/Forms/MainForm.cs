using osu_Bridge.Core;
using osu_Bridge.Core.Models;
using osu_Bridge.Core.Utils;
using osu_Bridge.WinForm.Models;
using osu_Bridge.WinForm.Service;
using osu_Bridge.WinForm.Utils;

namespace osu_Bridge.WinForm.Forms;

public partial class MainForm : Form
{
    private const char PATH_CHAR = '*';
    private readonly OsuBridge osuBridge = null!;

    private EditMode _currentEditMode = EditMode.Profile;

    public MainForm()
    {
        InitializeComponent();
        Icon = FormIconUtils.GetSoftwareIcon();
        Text = $"osu! Bridge {UpdateUtils.CurrentVersion} - WinForm Edition";

        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        osuBridge = new(DatabaseUtils.GetDatabasePath(appDataPath));
        osuBridge.Load();

        RefleshData(true);
        _ = CheckUpdate(true);
    }

    #region イベントハンドラー
    private void ServerComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        osuBridge.SelectServer(serverComboBox.SelectedIndex);
        _currentEditMode = EditMode.Server;
        GenerateSettingsPanel();
    }
    private void ProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        osuBridge.SelectProfile(profileComboBox.SelectedIndex);
        _currentEditMode = EditMode.Profile;
        GenerateSettingsPanel();
    }

    private void OsuFolderTextBox_TextChanged(object sender, EventArgs e)
    {
        osuBridge.SetOsuFolder(osuFolderTextBox.Text);
        GenerateSkinsList(LoadSkins());
    }
    private void OsuLazerFolderTextBox_TextChanged(object sender, EventArgs e)
    {
        osuBridge.SetOsuLazerFolder(osuLazerFolderTextBox.Text);
    }
    private void SongsFolderTextBox_TextChanged(object sender, EventArgs e)
    {
        osuBridge.SetSongsFolder(songsFolderTextBox.Text);
    }

    private void ShowOsuFolderPath_CheckedChanged(object sender, EventArgs e)
        => osuFolderTextBox.PasswordChar = showOsuFolderPath.Checked ? '\0' : PATH_CHAR;
    private void ShowOsuLazerFolderPath_CheckedChanged(object sender, EventArgs e)
        => osuLazerFolderTextBox.PasswordChar = showOsuLazerFolderPath.Checked ? '\0' : PATH_CHAR;
    private void ShowSongsFolderPath_CheckedChanged(object sender, EventArgs e)
        => songsFolderTextBox.PasswordChar = showSongsFolderPath.Checked ? '\0' : PATH_CHAR;

    private void LaunchButton_Click(object sender, EventArgs e)
    {
        try
        {
            osuBridge.Launch(beforeLaunch: () => CopyPassword(osuBridge.SelectedProfile));
        }
        catch (Exception ex)
        {
            FormUtils.ShowError($"起動に失敗しました。\nエラー: {ex.Message}");
        }

        osuBridge.Save();
        RefleshData(true);
    }

    private void OpenOsuFolderButton_Click(object sender, EventArgs e)
    {
        var (result, value) = FormUtils.OpenFolderDialog("osu!.exeが入っているフォルダを選択してください。");
        if (!result) return;

        osuFolderTextBox.Text = value;
    }
    private void OpenOsuLazerFolderButton_Click(object sender, EventArgs e)
    {
        var (result, value) = FormUtils.OpenFolderDialog("osu!.exe(Lazer)が入っているフォルダを選択してください。");
        if (!result) return;

        osuLazerFolderTextBox.Text = value;
    }
    private void OpenSongsFolderButton_Click(object sender, EventArgs e)
    {
        var (result, value) = FormUtils.OpenFolderDialog("Songsフォルダを選択してください。");
        if (!result) return;

        songsFolderTextBox.Text = value;
    }

    private void LazerModeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        osuBridge.SetLazerMode(lazerModeCheckbox.Checked);
        GenerateSettingsPanel();

        serverComboBox.Enabled = !osuBridge.LazerMode;
        launchButton.Text = osuBridge.LazerMode ? "Launch - Lazer" : "Launch";
    }

    private void GenerateServerButton_Click(object sender, EventArgs e)
    {
        var result = FormUtils.ShowConfirm("サーバープロファイルを新しく作成しますか？");
        if (!result) return;

        int index = osuBridge.CreateServer();
        if (index != -1) osuBridge.SelectServer(index);
        RefleshData(true);

        _currentEditMode = EditMode.Server;
        GenerateSettingsPanel();
    }
    private void GenerateProfileButton_Click(object sender, EventArgs e)
    {
        var result = FormUtils.ShowConfirm("プロファイルを新しく作成しますか？");
        if (!result) return;

        int index = osuBridge.CreateProfile();
        if (index != -1) osuBridge.SelectProfile(index);
        RefleshData(true);

        _currentEditMode = EditMode.Profile;
        GenerateSettingsPanel();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        osuBridge.Save(); // Auto Save
    }
    #endregion

    private static void CopyPassword(Profile? profile)
    {
        if (profile == null || !profile.PasswordAutoCopy) return;

        if (profile.Password != string.Empty)
            ClipboardUtils.Copy(profile.Password);
    }

    private void RefleshData(bool updateIndex = true)
    {
#pragma warning disable IDE0305 // コレクションの初期化を簡略化します
        int previousSelectedServerIndex = serverComboBox.SelectedIndex;
        serverComboBox.Items.Clear();
        serverComboBox.Items.AddRange(osuBridge.Servers.Select(s => s.Name).ToArray());

        if (updateIndex && ArrayUtils.IsValidIndex(serverComboBox.Items.Count, osuBridge.SelectedServerIndex))
            serverComboBox.SelectedIndex = osuBridge.SelectedServerIndex;
        else if (ArrayUtils.IsValidIndex(serverComboBox.Items.Count, previousSelectedServerIndex))
            serverComboBox.SelectedIndex = previousSelectedServerIndex;

        int previousSelectedProfileIndex = profileComboBox.SelectedIndex;
        profileComboBox.Items.Clear();
        profileComboBox.Items.AddRange(osuBridge.Profiles.Select(p => p.ProfileName).ToArray());

        if (updateIndex && ArrayUtils.IsValidIndex(profileComboBox.Items.Count, osuBridge.SelectedProfileIndex))
            profileComboBox.SelectedIndex = osuBridge.SelectedProfileIndex;
        else if (ArrayUtils.IsValidIndex(profileComboBox.Items.Count, previousSelectedProfileIndex))
            profileComboBox.SelectedIndex = previousSelectedProfileIndex;

        osuFolderTextBox.Text = osuBridge.OsuFolderPath;
        osuLazerFolderTextBox.Text = osuBridge.OsuLazerFolderPath;
        songsFolderTextBox.Text = osuBridge.SongsFolderPath;

        lazerModeCheckbox.Checked = osuBridge.LazerMode;

        GenerateServerProfilesList(
            serverComboBox.Items
                .Cast<object>()
                .Select(x => x.ToString())
                .ToArray()
        );

        GenerateServerToolMenu();
        GenerateProfileToolMenu();
#pragma warning restore IDE0305 // コレクションの初期化を簡略化します
    }

    private void GenerateSettingsPanel()
    {
        var previousScrollValue = profileSettingsPanel.AutoScrollPosition;

        if (_currentEditMode == EditMode.Profile) UIBuilder.BuildUI(profileSettingsPanel, osuBridge.SelectedProfile, osuBridge.LazerMode);
        else if (_currentEditMode == EditMode.Server) UIBuilder.BuildUI(profileSettingsPanel, osuBridge.SelectedServer, osuBridge.LazerMode);

        profileSettingsPanel.AutoScrollPosition = new Point(0, -previousScrollValue.Y);
    }

    private void GenerateSkinsList(string[] skinNames)
    {
        selectSkinMenu.DropDownItems.Clear();

        foreach (var skinName in skinNames)
        {
            var item = selectSkinMenu.DropDownItems.Add(skinName);
            item.Click += (s, e) =>
            {
                if (osuBridge.SelectedProfile == null)
                {
                    FormUtils.ShowError("プロファイルが選択されていないため、設定に失敗しました。");
                    return;
                }

                osuBridge.SelectedProfile.SkinName = skinName;
                _currentEditMode = EditMode.Profile;
                GenerateSettingsPanel();
            };
        }
    }

    private void GenerateServerToolMenu()
    {
        removeServerMenu.DropDownItems.Clear();
        duplicateServerMenu.DropDownItems.Clear();

        for (int i = 0; i < osuBridge.Servers.Count; i++)
        {
            string name = osuBridge.Servers[i].Name;

            FormUtils.AddMenuItem(removeServerMenu, name, i,
                confirmMessage: $"このサーバープロファイルを削除しますか？\nサーバープロファイル名: {name}",
                action: index =>
                {
                    osuBridge.RemoveServer(index);
                    RefleshData(true);
                }
            );

            FormUtils.AddMenuItem(duplicateServerMenu, name, i,
                confirmMessage: $"このサーバープロファイルを複製しますか？\nサーバープロファイル名: {name}",
                action: index =>
                {
                    int newIndex = osuBridge.DuplicateServer(index);
                    if (newIndex != -1) osuBridge.SelectServer(newIndex);
                    RefleshData(true);

                    _currentEditMode = EditMode.Server;
                    GenerateSettingsPanel();
                }
            );
        }
    }

    private void GenerateProfileToolMenu()
    {
        removeProfileMenu.DropDownItems.Clear();
        duplicateProfileMenu.DropDownItems.Clear();

        for (int i = 0; i < osuBridge.Profiles.Count; i++)
        {
            string name = osuBridge.Profiles[i].ProfileName;

            FormUtils.AddMenuItem(removeProfileMenu, name, i,
                confirmMessage: $"このプロファイルを削除しますか？\nプロファイル名: {name}",
                action: index =>
                {
                    osuBridge.RemoveProfile(index);
                    RefleshData(true);
                }
            );

            FormUtils.AddMenuItem(duplicateProfileMenu, name, i,
                confirmMessage: $"このプロファイルを複製しますか？\nプロファイル名: {name}",
                action: index =>
                {
                    int newIndex = osuBridge.DuplicateProfile(index);
                    if (newIndex != -1) osuBridge.SelectProfile(newIndex);
                    RefleshData(true);

                    _currentEditMode = EditMode.Profile;
                    GenerateSettingsPanel();
                }
            );
        }
    }

    private void GenerateServerProfilesList(string?[] serverProfileNames)
    {
        selectServerMenu.DropDownItems.Clear();

        foreach (var serverProfileName in serverProfileNames)
        {
            if (serverProfileName == null) continue;

            var item = selectServerMenu.DropDownItems.Add(serverProfileName);
            item.Click += (s, e) =>
            {
                if (osuBridge.SelectedProfile == null)
                {
                    FormUtils.ShowError("プロファイルが選択されていないため、設定に失敗しました。");
                    return;
                }

                osuBridge.SelectedProfile.ServerProfileName = serverProfileName;
                _currentEditMode = EditMode.Profile;
                GenerateSettingsPanel();
            };
        }
    }

    private string[] LoadSkins()
    {
#pragma warning disable IDE0305 // コレクションの初期化を簡略化します
        var skinPath = Path.Join(osuBridge.OsuFolderPath, "skins");
        if (!Directory.Exists(skinPath)) return [];

        var skins = new List<string>();
        foreach (var skin in Directory.GetDirectories(skinPath))
        {
            skins.Add(Path.GetFileName(skin));
        }

        return skins.ToArray();
#pragma warning restore IDE0305 // コレクションの初期化を簡略化します
    }

    private async Task CheckUpdate(bool silent = false)
    {
        var updateResult = await UpdateUtils.CheckUpdate();
        if (updateResult.result) updateCheck.BackColor = Color.LightGreen;

        if (silent) return;

        if (updateResult.result)
        {
            bool result = FormUtils.ShowConfirm(
                "新しいosu! Bridgeのバージョンが利用可能です！\n\n" +
                "「はい」をクリックすると最新のリリースページを開きます。\n\n" +
                $"現在のバージョン: {UpdateUtils.CurrentVersion}\n" +
                $"最新のバージョン: {updateResult.latestVersion}\n\n" +
                $"以下は最新版（{updateResult.latestVersion}）の変更内容です。\n\n" +
                updateResult.changeLog
            );
            if (!result) return;

            try
            {
                UpdateUtils.OpenGithubURL();
            }
            catch
            {
                FormUtils.ShowError("リンクを開くことができませんでした");
            }
        }
        else
        {
            FormUtils.ShowInfo(
                "ご利用中のosu! Bridgeは最新版です！\n\n" +
                "いつもご利用いただき、ありがとうございます。\n" +
                "今後のアップデートもぜひお楽しみに！\n\n" +
                $"以下は最新版（{updateResult.latestVersion}）の変更内容です。\n\n" +
                updateResult.changeLog
            );
        }
    }

    private async void UpdateCheck_Click(object sender, EventArgs e)
        => await CheckUpdate();
}

using osu_Bridge.Core.Models;
using osu_Bridge.Core.Services;
using osu_Bridge.Core.Utils;
using osu_Bridge.WinForm.Models;
using osu_Bridge.WinForm.Service;
using osu_Bridge.WinForm.Utils;

namespace osu_Bridge.WinForm.Forms;

public partial class MainForm : Form
{
    private readonly OsuBridge osuBridge = null!;

    private EditMode _currentEditMode = EditMode.Profile;

    public MainForm()
    {
        InitializeComponent();

        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var databasePath = Path.Combine(appDataPath, "osu-Bridge", "database.json");

        osuBridge = new(databasePath);
        osuBridge.Load();

        RefleshData(true);
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

    private void SongsFolderTextBox_TextChanged(object sender, EventArgs e)
    {
        osuBridge.SetSongsFolder(songsFolderTextBox.Text);
    }

    private void LaunchButton_Click(object sender, EventArgs e)
    {
        try
        {
            osuBridge.Launch(beforeLaunch: () => CopyPassword(osuBridge.SelectedProfile));
        }
        catch
        {
            MessageBox.Show("起動に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    private void OpenSongsFolderButton_Click(object sender, EventArgs e)
    {
        var (result, value) = FormUtils.OpenFolderDialog("Songsフォルダを選択してください。");
        if (!result) return;

        songsFolderTextBox.Text = value;
    }

    private void GenerateServer_Click(object sender, EventArgs e)
    {
        int index = osuBridge.CreateServer();
        if (index != -1) osuBridge.SelectServer(index);
        RefleshData(true);
    }

    private void GenerateProfile_Click(object sender, EventArgs e)
    {
        int index = osuBridge.CreateProfile();
        if (index != -1) osuBridge.SelectProfile(index);
        RefleshData(true);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        if (_currentEditMode == EditMode.Profile)
        {
            if (osuBridge.SelectedProfile == null) return;

            var result = MessageBox.Show(string.Format("このプロファイルを削除しますか？\nプロファイル名: {0}", osuBridge.SelectedProfile.ProfileName), "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes) osuBridge.RemoveProfile(osuBridge.SelectedProfileIndex);
        }
        else if (_currentEditMode == EditMode.Server)
        {
            if (osuBridge.SelectedServer == null) return;

            var result = MessageBox.Show(string.Format("このサーバーを削除しますか？\nサーバープロファイル名: {0}", osuBridge.SelectedServer.Name), "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes) osuBridge.RemoveServer(osuBridge.SelectedServerIndex);
        }

        RefleshData(true);
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
        songsFolderTextBox.Text = osuBridge.SongsFolderPath;

        GenerateServerProfilesList(
            serverComboBox.Items
                .Cast<object>()
                .Select(x => x.ToString())
                .ToArray()
        );
#pragma warning restore IDE0305 // コレクションの初期化を簡略化します
    }

    private void GenerateSettingsPanel()
    {
        if (_currentEditMode == EditMode.Profile) UIBuilder.BuildUI(settingsPanels, osuBridge.SelectedProfile);
        else if (_currentEditMode == EditMode.Server) UIBuilder.BuildUI(settingsPanels, osuBridge.SelectedServer);
    }

    private void GenerateSkinsList(string[] skinNames)
    {
        selectSkinMenu.DropDownItems.Clear();
        foreach (var skinName in skinNames)
        {
            var item = selectSkinMenu.DropDownItems.Add(skinName);
            item.Click += (s, e) => ClipboardUtils.Copy(skinName);
        }
    }

    private void GenerateServerProfilesList(string?[] serverProfileNames)
    {
        selectServerMenu.DropDownItems.Clear();
        foreach (var serverProfileName in serverProfileNames)
        {
            if (serverProfileName == null) continue;

            var item = selectServerMenu.DropDownItems.Add(serverProfileName);
            item.Click += (s, e) => ClipboardUtils.Copy(serverProfileName);
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
}

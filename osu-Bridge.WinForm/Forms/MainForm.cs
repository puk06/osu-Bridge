using osu_Bridge.Core.Models;
using osu_Bridge.Core.Services;
using osu_Bridge.Core.Utils;
using osu_Bridge.WinForm.Models;
using osu_Bridge.WinForm.Service;
using System.Runtime.InteropServices;

namespace osu_Bridge.WinForm.Forms;

public partial class MainForm : Form
{
    private readonly OsuBridge osuBridge = null!;

    private EditMode _currentEditMode = EditMode.Profile;

    public MainForm()
    {
        InitializeComponent();

        osuBridge = new("./database.json");
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
    }

    private void LaunchButton_Click(object sender, EventArgs e)
    {
        osuBridge.Launch(beforeLaunch: () => CopyPassword(osuBridge.SelectedProfile));
        osuBridge.Save();
        RefleshData(true);
    }

    private void OpenFolderButton_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
        {
            UseDescriptionForTitle = true,
            Description = "osu!.exeが入っているフォルダを選択してください。"
        };
        if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

        osuFolderTextBox.Text = folderBrowserDialog.SelectedPath;
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
    #endregion

    private static void CopyPassword(Profile? profile)
    {
        if (profile == null || !profile.PasswordAutoCopy) return;

        if (profile.Password != string.Empty)
        {
            try
            {
                Clipboard.SetText(profile.Password);
            }
            catch (Exception error)
            {
                if (error is ExternalException) return;
                MessageBox.Show(string.Format("パスワードのコピーに失敗しました。\n{0}", error), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void RefleshData(bool updateIndex = true)
    {
        int previousSelectedServerIndex = serverComboBox.SelectedIndex;
        serverComboBox.Items.Clear();
        serverComboBox.Items.AddRange(osuBridge.Servers.Select(s => s.Name).ToArray());
        if (ArrayUtils.IsValidIndex(serverComboBox.Items.Count, osuBridge.SelectedServerIndex)) serverComboBox.SelectedIndex = previousSelectedServerIndex;
        if (updateIndex) serverComboBox.SelectedIndex = osuBridge.SelectedServerIndex;

        int previousSelectedProfileIndex = profileComboBox.SelectedIndex;
        profileComboBox.Items.Clear();
        profileComboBox.Items.AddRange(osuBridge.Profiles.Select(p => p.ProfileName).ToArray());
        if (ArrayUtils.IsValidIndex(profileComboBox.Items.Count, osuBridge.SelectedProfileIndex)) profileComboBox.SelectedIndex = previousSelectedProfileIndex;
        if (updateIndex) profileComboBox.SelectedIndex = osuBridge.SelectedProfileIndex;

        osuFolderTextBox.Text = osuBridge.OsuFolderPath;
    }

    private void GenerateSettingsPanel()
    {
        if (_currentEditMode == EditMode.Profile) UIBuilder.BuildUI(settingsPanels, osuBridge.SelectedProfile);
        else if (_currentEditMode == EditMode.Server) UIBuilder.BuildUI(settingsPanels, osuBridge.SelectedServer);
    }
}

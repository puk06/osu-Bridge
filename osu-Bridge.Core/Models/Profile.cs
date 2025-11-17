using osu_Bridge.Core.Attributes;

namespace osu_Bridge.Core.Models;

public class Profile
{
    [Title("Profile")]
    [UIField("プロファイル名")]
    public string ProfileName { get; set; } = "新しいプロファイル";

    #region Credential
    [Title("Credential")]
    [UIField("ユーザー名")]
    [ConfigParameter("Username")]
    public string Username { get; set; } = string.Empty;

    [Confidential]
    [UIField("パスワード")]
    public string Password { get; set; } = string.Empty;

    [UIField("起動時自動コピー")]
    public bool PasswordAutoCopy { get; set; } = false;
    #endregion

    #region Volume
    [Title("Volume")]
    [UIField("音量を変更する")]
    public bool ChangeAudioVolume { get; set; } = false;

    [ConfigParameter("VolumeUniversal")]
    [UIField("音量(全体）")]
    [DependsOn(nameof(ChangeAudioVolume))]
    public int MasterAudioVolume { get; set; } = 100;

    [ConfigParameter("VolumeEffect")]
    [UIField("音量(効果音）")]
    [DependsOn(nameof(ChangeAudioVolume))]
    public int EffectAudioVolume { get; set; } = 100;

    [ConfigParameter("VolumeMusic")]
    [UIField("音量(音楽）")]
    [DependsOn(nameof(ChangeAudioVolume))]
    public int MusicAudioVolume { get; set; } = 100;
    #endregion

    #region Offset
    [Title("Offset")]
    [UIField("オフセットを変更する")]
    public bool ChangeOffset { get; set; } = false;

    [ConfigParameter("Offset")]
    [UIField("全体オフセット")]
    [DependsOn(nameof(ChangeOffset))]
    public int MasterOffset { get; set; }
    #endregion

    #region Resolution
    [Title("Resolution")]
    [UIField("通常を変更する")]
    public bool ChangeNormalResolution { get; set; } = false;

    [ConfigParameter("Width")]
    [UIField("横")]
    [DependsOn(nameof(ChangeNormalResolution))]
    public int NormalWidth { get; set; } = 1920;

    [ConfigParameter("Height")]
    [UIField("高さ")]
    [DependsOn(nameof(ChangeNormalResolution))]
    public int NormalHeight { get; set; } = 1080;

    [UIField("フルスクリーンを変更する")]
    public bool ChangeFullScreenResolution { get; set; } = false;

    [ConfigParameter("FullScreenWidth")]
    [UIField("横")]
    [DependsOn(nameof(ChangeFullScreenResolution))]
    public int FullScreenWidth { get; set; } = 1920;

    [ConfigParameter("FullScreenHeight")]
    [UIField("高さ")]
    [DependsOn(nameof(ChangeFullScreenResolution))]
    public int FullScreenHeight { get; set; } = 1080;
    #endregion

    #region Skin
    [Title("Skin")]
    [UIField("スキンを変更する")]
    public bool ChangeSkin { get; set; } = false;

    [ConfigParameter("Skin")]
    [UIField("スキン名")]
    [DependsOn(nameof(ChangeSkin))]
    public string SkinName { get; set; } = string.Empty;
    #endregion

    #region Server
    [Title("Server")]
    [UIField("サーバーを変更する")]
    public bool ChangeServer { get; set; } = false;
    
    [UIField("サーバープロファイル")]
    public string ServerProfileName { get; set; } = string.Empty;
    #endregion

    #region Songs Folder
    [Title("Songs Folder")]
    [UIField("フォルダを変更する")]
    public bool ChangeSongsFolder { get; set; } = false;

    [UIField("フォルダパス")]
    [DependsOn(nameof(ChangeSongsFolder))]
    [ConfigParameter("BeatmapDirectory")]
    public string SongsFolderPath { get; set; } = string.Empty;
    #endregion
}

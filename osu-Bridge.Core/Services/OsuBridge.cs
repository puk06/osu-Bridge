using osu_Bridge.Core.Attributes;
using osu_Bridge.Core.Attributes.Lazer;
using osu_Bridge.Core.Models;
using osu_Bridge.Core.Models.Lazer;
using osu_Bridge.Core.Utils;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace osu_Bridge.Core.Services;

public partial class OsuBridge(string databasePath)
{
    [GeneratedRegex(@"\{(\w+)\}")]
    private static partial Regex LazerConfigFormatString();

    public bool LazerMode => _lazerMode;

    public string OsuFolderPath => _osuFolderPath;
    public string OsuLazerFolderPath => _osuLazerFolderPath;
    public string SongsFolderPath => _songsFolderPath;

    public List<Profile> Profiles => _profiles;
    public List<Server> Servers => _servers;

    public Profile? SelectedProfile => _selectedProfileIndex != -1 && ArrayUtils.IsValidIndex(_profiles.Count, _selectedProfileIndex) ? _profiles[_selectedProfileIndex] : null;
    public Server? SelectedServer => _selectedServerIndex != -1 && ArrayUtils.IsValidIndex(_servers.Count, _selectedServerIndex) ? _servers[_selectedServerIndex] : null;

    public int SelectedProfileIndex => _selectedProfileIndex;
    public int SelectedServerIndex => _selectedServerIndex;

    private const string OSU_PROCESS_NAME = "osu!.exe";
    private const string SONGS_DIRECTORY_KEY = "BeatmapDirectory";

    private readonly List<Profile> _profiles = [];
    private readonly List<Server> _servers = [];

    private ProcessStartInfo? _processStartInfo;
    private readonly string _databasePath = databasePath;

    private bool _lazerMode = false;
    private string _osuFolderPath = string.Empty;
    private string _osuLazerFolderPath = string.Empty;
    private string _songsFolderPath = string.Empty;
    private int _selectedProfileIndex = -1;
    private int _selectedServerIndex = -1;

    #region Lazer Mode
    public void SetLazerMode(bool lazerMode)
    {
        _lazerMode = lazerMode;
        GenerateProcessStartInfo();
    }
    #endregion

    #region Folder
    public void SetOsuFolder(string folder)
    {
        _osuFolderPath = folder;
        GenerateProcessStartInfo();
    }
    public void SetOsuLazerFolder(string folder)
    {
        _osuLazerFolderPath = folder;
        GenerateProcessStartInfo();
    }
    public void SetSongsFolder(string folder)
    {
        _songsFolderPath = folder;
    }
    
    private void GenerateProcessStartInfo()
    {
        var osuFolder = _lazerMode ? _osuLazerFolderPath : _osuFolderPath;
        
        _processStartInfo = new ProcessStartInfo
        {
            FileName = Path.Combine(osuFolder, OSU_PROCESS_NAME),
            WorkingDirectory = osuFolder
        };
    }
    #endregion

    #region Profile
    public int CreateProfile()
    {
        var newProfile = new Profile();
        _profiles.Add(newProfile);

        return _profiles.IndexOf(newProfile);
    }
    public Profile? SelectProfile(int index)
    {
        if (!ArrayUtils.IsValidIndex(_profiles.Count, index)) return null;
        _selectedProfileIndex = index;

        return SelectedProfile;
    }
    public bool RemoveProfile(int index)
    {
        if (!ArrayUtils.IsValidIndex(_profiles.Count, index)) return false;

        _profiles.RemoveAt(index);
        if (!ArrayUtils.IsValidIndex(index, _selectedProfileIndex)) _selectedProfileIndex = -1;

        return true;
    }
    public bool EditProfile(string key, string value)
        => TypeUtils.EditInternal(SelectedProfile?.GetType(), key, value);
    #endregion

    #region Server
    public int CreateServer()
    {
        var newServer = new Server();
        _servers.Add(newServer);

        return _servers.IndexOf(newServer);
    }
    public Server? SelectServer(int index)
    {
        if (!ArrayUtils.IsValidIndex(_servers.Count, index)) return null;
        _selectedServerIndex = index;

        return SelectedServer;
    }
    public bool RemoveServer(int index)
    {
        if (!ArrayUtils.IsValidIndex(_servers.Count, index)) return false;

        _servers.RemoveAt(index);
        if (!ArrayUtils.IsValidIndex(index, _selectedServerIndex)) _selectedServerIndex = -1;

        return true;
    }
    public bool EditServer(string key, string value)
        => TypeUtils.EditInternal(SelectedServer?.GetType(), key, value);
    #endregion

    #region Configuration File
    private void SetConfig(Profile? profile, Server? server)
    {
        if (profile == null && server == null) return;

        Dictionary<string, string> parameters = [];

        if (profile != null)
            ApplyToParameter(profile, parameters);

        if (server != null)
            ApplyToParameter(server, parameters);

        if (parameters.ContainsKey(SONGS_DIRECTORY_KEY))
        {
            parameters.Add(SONGS_DIRECTORY_KEY, _songsFolderPath);
        }

        WriteConfigToFile(parameters);
    }
    private static void ApplyToParameter(object target, Dictionary<string, string> parameters)
    {
        var type = target.GetType();

        foreach (var property in type.GetProperties())
        {
            if (property.GetCustomAttributes(typeof(ConfigParameterAttribute), false).FirstOrDefault() is ConfigParameterAttribute configParameterAttribute)
            {
                if (property.GetCustomAttributes(typeof(DependsOnAttribute), false).FirstOrDefault() is DependsOnAttribute dependsOnAttribute)
                {
                    var dependsProp = type.GetProperty(dependsOnAttribute.PropertyName);
                    if (dependsProp == null || !(bool)dependsProp.GetValue(target)!) continue;
                }

                var value = property.GetValue(target);
                if (value == null) continue;

                parameters.Add(configParameterAttribute.ParameterName, value.ToString()!);
            }
        }
    }
    private void WriteConfigToFile(Dictionary<string, string> parameters)
    {
        string username = Environment.UserName;
        string path = Path.Combine(_osuFolderPath, $"osu!.{username}.cfg");

        string[] lines = File.ReadAllLines(path);
        ConfigUtils.WriteParameterValue(lines, parameters);
        File.WriteAllLines(path, lines);
    }
    #endregion

    #region Configuration File (Lazer)
    private static void SetConfigLazer(Profile? profile, Server? server)
    {
        if (profile == null && server == null) return;

        Dictionary<string, string> frameworkParameters = [];
        Dictionary<string, string> gameParameters = [];

        if (profile != null)
            ApplyToParameterLazer(profile, frameworkParameters, gameParameters);

        WriteConfigToFileLazer(frameworkParameters, gameParameters);
    }
    private static void ApplyToParameterLazer(object target, Dictionary<string, string> frameworkParameters, Dictionary<string, string> gameParameters)
    {
        var type = target.GetType();

        foreach (var property in type.GetProperties())
        {
            if (property.GetCustomAttributes(typeof(LazerConfigParameterAttribute), false).FirstOrDefault() is LazerConfigParameterAttribute lazerConfigurationAttribute)
            {
                if (property.GetCustomAttributes(typeof(DependsOnAttribute), false).FirstOrDefault() is DependsOnAttribute dependsOnAttribute)
                {
                    var dependsProp = type.GetProperty(dependsOnAttribute.PropertyName);
                    if (dependsProp == null || !(bool)dependsProp.GetValue(target)!) continue;
                }

                var value = property.GetValue(target);
                if (value == null) continue;

                var valueText = value.ToString();

                if (lazerConfigurationAttribute.ParameterFormat != string.Empty)
                {
                    valueText = LazerConfigFormatString().Replace(lazerConfigurationAttribute.ParameterFormat, match =>
                    {
                        string propertyName = match.Groups[1].Value;
                        var prop = type.GetProperty(propertyName);
                        if (prop == null) return match.Value;

                        var value = prop.GetValue(target);
                        return value?.ToString() ?? "";
                    });
                }

                if (Attribute.IsDefined(property, typeof(LazerValueClampAttribute)))
                {
                    var parsedValue = double.Parse(valueText!);
                    parsedValue /= 100;

                    valueText = parsedValue.ToString("F1");
                }

                if (lazerConfigurationAttribute.LazerConfigurationType == LazerConfigurationType.Framework)
                {
                    frameworkParameters.Add(lazerConfigurationAttribute.ParameterName, valueText!);
                }
                else if(lazerConfigurationAttribute.LazerConfigurationType == LazerConfigurationType.Game)
                {
                    gameParameters.Add(lazerConfigurationAttribute.ParameterName, valueText!);
                }
            }
        }
    }
    private static void WriteConfigToFileLazer(Dictionary<string, string> frameworkParameters, Dictionary<string, string> gameParameters)
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        string frameworkConfigPath = Path.Combine(appDataPath, "osu", "framework.ini");
        string gameConfigPath = Path.Combine(appDataPath, "osu", "game.ini");

        string[] frameworkLines = File.ReadAllLines(frameworkConfigPath);
        ConfigUtils.WriteParameterValue(frameworkLines, frameworkParameters);
        File.WriteAllLines(frameworkConfigPath, frameworkLines);

        string[] gameLines = File.ReadAllLines(gameConfigPath);
        ConfigUtils.WriteParameterValue(gameLines, gameParameters);
        File.WriteAllLines(gameConfigPath, gameLines);
    }
    #endregion
    
    #region Launch
    public void Launch(Action? beforeLaunch = null, Action? afterLaunch = null)
    {
        if (_processStartInfo == null) return;

        if (SelectedProfile != null)
        {
            if (SelectedProfile.ChangeServer && SelectedProfile.ServerProfileName != string.Empty)
            {
                var server = _servers.FirstOrDefault(s => s.Name == SelectedProfile.ServerProfileName);
                if (server != null)
                {
                    var serverIndex = _servers.IndexOf(server);
                    if (serverIndex != -1) _selectedServerIndex = serverIndex;
                }
            }
            
            if (_lazerMode) SetConfigLazer(SelectedProfile, SelectedServer); // Config Setup
            else SetConfig(SelectedProfile, SelectedServer);
        }

        _processStartInfo.Arguments = string.Empty;
        if (SelectedServer != null && SelectedServer.ServerEndpoint != string.Empty)
        {
            _processStartInfo.Arguments = "-devserver " + SelectedServer.ServerEndpoint;
        }

        beforeLaunch?.Invoke();

        Process.Start(_processStartInfo);

        afterLaunch?.Invoke();
    }
    #endregion

    #region Save
    public bool Save()
    {
        try
        {
            var database = new Database()
            {
                LazerMode = _lazerMode,
                OsuFolderPath = _osuFolderPath,
                OsuLazerFolderPath = _osuLazerFolderPath,
                SongsFolderPath = _songsFolderPath,
                Profiles = _profiles,
                Servers = _servers,
                LastSelectedProfileIndex = _selectedProfileIndex,
                LastSelectedServerIndex = _selectedServerIndex,
            };

            var dir = Path.GetDirectoryName(_databasePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonSerializer.Serialize(database);
            File.WriteAllText(_databasePath, json);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region Load
    public void Load()
    {
        var database = DatabaseUtils.LoadDatabase(_databasePath);

        _profiles.Clear();
        _profiles.AddRange(database.Profiles);

        _servers.Clear();
        _servers.AddRange(database.Servers);

        _lazerMode = database.LazerMode;
        _osuFolderPath = database.OsuFolderPath;
        _osuLazerFolderPath = database.OsuLazerFolderPath;
        _songsFolderPath = database.SongsFolderPath;

        GenerateProcessStartInfo();

        _selectedProfileIndex = database.LastSelectedProfileIndex;
        _selectedServerIndex = database.LastSelectedServerIndex;
    }
    #endregion
}

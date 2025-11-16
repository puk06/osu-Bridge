using osu_Bridge.Core.Attributes;
using osu_Bridge.Core.Models;
using osu_Bridge.Core.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace osu_Bridge.Core.Services;

public class OsuBridge(string databasePath)
{
    public string OsuFolderPath => _osuFolderPath;

    public List<Profile> Profiles => _profiles;
    public List<Server> Servers => _servers;

    public Profile? SelectedProfile => _selectedProfileIndex != -1 && ArrayUtils.IsValidIndex(_profiles.Count, _selectedProfileIndex) ? _profiles[_selectedProfileIndex] : null;
    public Server? SelectedServer => _selectedServerIndex != -1 && ArrayUtils.IsValidIndex(_servers.Count, _selectedServerIndex) ? _servers[_selectedServerIndex] : null;

    public int SelectedProfileIndex => _selectedProfileIndex;
    public int SelectedServerIndex => _selectedServerIndex;

    private const string OSU_PROCESS_NAME = "osu!.exe";

    private readonly List<Profile> _profiles = new();
    private readonly List<Server> _servers = new();

    private ProcessStartInfo? _processStartInfo;
    private readonly string _databasePath = databasePath;
    private string _osuFolderPath = string.Empty;
    private int _selectedProfileIndex = -1;
    private int _selectedServerIndex = -1;

    #region osu! Folder
    public void SetOsuFolder(string folder)
    {
        _osuFolderPath = folder;

        _processStartInfo = new ProcessStartInfo
        {
            FileName = Path.Combine(_osuFolderPath, OSU_PROCESS_NAME),
            WorkingDirectory = _osuFolderPath
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

        Dictionary<string, string> parameters = new();

        if (profile != null)
            ApplyToParameter(profile, parameters);

        if (server != null)
            ApplyToParameter(server, parameters);

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

        for (int i = 0; i < lines.Length; i++)
        {
            string key = lines[i].Split('=')[0].Trim();
            
            for (int j = 0; j < parameters.Count; j++)
            {
                if (key != parameters.ElementAt(j).Key) continue;

                Debug.WriteLine($"{parameters.ElementAt(j).Key} = {parameters.ElementAt(j).Value}");
                lines[i] = $"{parameters.ElementAt(j).Key} = {parameters.ElementAt(j).Value}";
                break;
            }
        }

        File.WriteAllLines(path, lines);
    }
    #endregion

    #region Launch
    public void Launch(Action? beforeLaunch = null, Action? afterLaunch = null)
    {
        if (_processStartInfo == null) return;

        if (SelectedProfile != null)
        {
            if (SelectedProfile.ChangeServer && !string.IsNullOrEmpty(SelectedProfile.ServerProfileName))
            {
                var server = _servers.FirstOrDefault(s => s.Name == SelectedProfile.ServerProfileName);
                if (server != null)
                {
                    var serverIndex = _servers.IndexOf(server);
                    if (serverIndex != -1) _selectedServerIndex = serverIndex;
                }
            }

            SetConfig(SelectedProfile, SelectedServer); // Config Setup
        }

        _processStartInfo.Arguments = string.Empty;
        if (SelectedServer != null && SelectedServer.ServerEndpoint != string.Empty)
        {
            _processStartInfo.Arguments = "-devserver " + _servers[_selectedServerIndex].ServerEndpoint;
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
                OsuFolderPath = _osuFolderPath,
                Profiles = _profiles,
                Servers = _servers,
                LastSelectedProfileIndex = _selectedProfileIndex,
                LastSelectedServerIndex = _selectedServerIndex,
            };

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

        _osuFolderPath = database.OsuFolderPath;

        _processStartInfo = new ProcessStartInfo
        {
            FileName = Path.Combine(_osuFolderPath, "osu!.exe"),
            WorkingDirectory = _osuFolderPath
        };

        _selectedProfileIndex = database.LastSelectedProfileIndex;
        _selectedServerIndex = database.LastSelectedServerIndex;
    }
    #endregion
}

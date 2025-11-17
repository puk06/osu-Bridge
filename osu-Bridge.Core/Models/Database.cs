namespace osu_Bridge.Core.Models;

public class Database
{
    public bool LazerMode { get; set; } = false;
    public string OsuFolderPath { get; set; } = string.Empty;
    public string OsuLazerFolderPath { get; set; } = string.Empty;
    public string SongsFolderPath { get; set; } = "Songs"; // Default
    public List<Profile> Profiles { get; set; } = [];
    public List<Server> Servers { get; set; } = [];
    public int LastSelectedProfileIndex { get; set; } = -1;
    public int LastSelectedServerIndex { get; set; } = -1;
}

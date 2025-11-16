namespace osu_Bridge.Core.Models;

public class Database
{
    public string OsuFolderPath { get; set; } = string.Empty;
    public List<Profile> Profiles { get; set; } = [];
    public List<Server> Servers { get; set; } = [];
    public int LastSelectedProfileIndex { get; set; } = -1;
    public int LastSelectedServerIndex { get; set; } = -1;
}

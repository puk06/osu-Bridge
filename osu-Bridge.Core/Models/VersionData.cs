namespace osu_Bridge.Core.Models;

public class VersionData
{
    public string LatestVersion { get; set; } = string.Empty;
    public string[] ChangeLog { get; set; } = [];
}

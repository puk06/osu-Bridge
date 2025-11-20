using osu_Bridge.Core.Models;
using System.Diagnostics;
using System.Text.Json;

namespace osu_Bridge.Core.Utils;

public class UpdateUtils
{
    private const string CURRENT_VERSION = "v1.0.0";
    private static readonly string GITHUB_URL = Properties.Resources.GithubURL;
    private static readonly string GITHUB_LATEST_URL = Properties.Resources.GithubURL + "/releases/latest";
    private static readonly string UPDATE_CHECK_URL = Properties.Resources.UpdateCheckURL;
    private static readonly HttpClient _httpClient = new();
    private static readonly ProcessStartInfo UrlProcessStartInfo = new()
    {
        FileName = GITHUB_LATEST_URL,
        UseShellExecute = true
    };

    public static string CurrentVersion => CURRENT_VERSION;
    public static string GithubURL => GITHUB_URL;
    public static string GithubReleaseURL => GITHUB_LATEST_URL;
    public static string UpdateCheckURL => UPDATE_CHECK_URL;

    public async static Task<(bool result, string latestVersion, string changeLog)> CheckUpdate()
    {
        try
        {
            string response = await _httpClient.GetStringAsync(UPDATE_CHECK_URL);
            VersionData? versionData = JsonSerializer.Deserialize<VersionData>(response);
            if (versionData == null) return (false, "", "");

            return (versionData.LatestVersion != CURRENT_VERSION, versionData.LatestVersion, string.Join("\n", versionData.ChangeLog.Select(log => $"・{log}")));
        }
        catch
        {
            return (false, "", "");
        }
    }

    public static void OpenGithubURL()
        => Process.Start(UrlProcessStartInfo);
}

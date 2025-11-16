using osu_Bridge.Core.Models;
using System.Text.Json;

namespace osu_Bridge.Core.Utils;

internal class DatabaseUtils
{
    internal static Database LoadDatabase(string databasePath)
    {
        try
        {
            string json = File.ReadAllText(databasePath);
            var database = JsonSerializer.Deserialize<Database>(json) ?? new Database();

            return database;
        }
        catch
        {
            return new Database();
        }
    }
}

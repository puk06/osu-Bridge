namespace osu_Bridge.Core.Utils;

public static class ConfigUtils
{
    public static void WriteParameterValue(string[] lines, Dictionary<string, string> parameters)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            string key = lines[i].Split('=')[0].Trim();

            for (int j = 0; j < parameters.Count; j++)
            {
                if (key != parameters.ElementAt(j).Key) continue;

                lines[i] = $"{parameters.ElementAt(j).Key} = {parameters.ElementAt(j).Value}";
                break;
            }
        }
    }
}

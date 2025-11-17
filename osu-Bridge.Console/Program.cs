using osu_Bridge.Core.Services;
using osu_Bridge.Core.Utils;

class Program
{
    static void Main()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var databasePath = Path.Combine(appDataPath, "osu-Bridge", "database.json");

        OsuBridge osuBridge = new(databasePath);
        osuBridge.Load();
        
        Console.WriteLine("osu! Bridge - CUI Edition v1.0");
        Console.WriteLine("Type 'exit' to quit.\n");

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            if (input == null) continue;

            if (input.Trim().Equals("exit", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Bye!");
                break;
            }

            HandleCommand(input, osuBridge);
        }
    }

    private static void HandleCommand(string command, OsuBridge osuBridge)
    {
        try
        {
            var parts = ArgsParserUtils.Parse(command);

            switch (parts[0].ToLower())
            {
                case "load": LoadCommand(osuBridge); break;
                case "create": CreateCommand(osuBridge, parts); break;
                case "select": SelectCommand(osuBridge, parts); break;
                case "setfolder": SetFolderCommand(osuBridge, parts); break;
                case "launch": LaunchCommand(osuBridge); break;
                case "save": SaveCommand(osuBridge); break;
                case "edit": EditCommand(osuBridge, parts); break;
                case "remove": RemoveCommand(osuBridge, parts); break;
                default: Console.WriteLine("Unknown command: " + parts[0]); break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Command Error!\nError: {0}", ex);
        }
    }

    #region Commands
    private static void LoadCommand(OsuBridge osuBridge)
    {
        osuBridge.Load();
    }
    private static void CreateCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].StartsWith('p'))
        {
            var profileIndex = osuBridge.CreateProfile();
            Console.WriteLine("Profile Successfully Created!\nProfile Index: {0}\n", profileIndex);
            osuBridge.SelectProfile(profileIndex);
        }
        else if (args[1].StartsWith('s'))
        {
            var serverIndex = osuBridge.CreateProfile();
            Console.WriteLine("Server Successfully Created!\nServer Index: {0}\n", serverIndex);
            osuBridge.SelectServer(serverIndex);
        }
    }
    private static void SelectCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].StartsWith('p'))
        {
            try
            {
                var profileIndex = int.Parse(args[2]);
                var profile = osuBridge.SelectProfile(profileIndex);
                if (profile == null)
                {
                    Console.WriteLine("Profile Selection Failed...");
                    return;
                }

                Console.WriteLine("Profile Successfully Selected!\nProfile Name: {0}\n", profile.ProfileName);
            }
            catch
            {
                Console.WriteLine("Command Error!");
            }
        }
        else if (args[1].StartsWith('s'))
        {
            try
            {
                var serverIndex = int.Parse(args[2]);
                var server = osuBridge.SelectServer(serverIndex);
                if (server == null)
                {
                    Console.WriteLine("Server Selection Failed...");
                    return;
                }

                Console.WriteLine("server Successfully Selected!\nserver Name: {0}\n", server.Name);
            }
            catch
            {
                Console.WriteLine("Command Error!");
            }
        }
    }
    private static void SetFolderCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].StartsWith('o'))
        {
            osuBridge.SetOsuFolder(args[2]);
            Console.WriteLine("osu! Folder Successfully Set!\nFolder Path: {0}\n", osuBridge.OsuFolderPath);
        }
        else if (args[1].StartsWith('s'))
        {
            osuBridge.SetSongsFolder(args[2]);
            Console.WriteLine("Songs Folder Successfully Set!\nFolder Path: {0}\n", osuBridge.SongsFolderPath);
        }
    }
    private static void LaunchCommand(OsuBridge osuBridge)
    {
        Console.WriteLine("Launching osu!...");
        osuBridge.Launch();
    }
    private static void SaveCommand(OsuBridge osuBridge)
    {
        osuBridge.Save();
        Console.WriteLine("Configuration Saved!");
    }
    private static void EditCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].StartsWith('p'))
        {
            bool result = osuBridge.EditProfile(args[2], args[3]);

            if (result)
            {
                Console.WriteLine("Profile Successfully Edited!\n");
            }
            else
            {
                Console.WriteLine("Profile Edit Failed...!\n");
            }
        }
        else if (args[1].StartsWith('s'))
        {
            bool result = osuBridge.EditServer(args[2], args[3]);

            if (result)
            {
                Console.WriteLine("Server Successfully Edited!\n");
            }
            else
            {
                Console.WriteLine("Server Edit Failed...!\n");
            }
        }
    }
    private static void RemoveCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].StartsWith('p'))
        {
            bool result = osuBridge.RemoveProfile(int.Parse(args[2]));

            if (result)
            {
                Console.WriteLine("Profile Successfully Removed!\n");
            }
            else
            {
                Console.WriteLine("Profile Remove Failed...!\n");
            }
        }
        else if (args[1].StartsWith('s'))
        {
            bool result = osuBridge.RemoveServer(int.Parse(args[2]));

            if (result)
            {
                Console.WriteLine("Server Successfully Removed!\n");
            }
            else
            {
                Console.WriteLine("Server Remove Failed...!\n");
            }
        }
    }
    #endregion
}

using osu_Bridge.Core;
using osu_Bridge.Core.Utils;

class Program
{
    static void Main()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        OsuBridge osuBridge = new(DatabaseUtils.GetDatabasePath(appDataPath));
        osuBridge.Load();
        
        Console.WriteLine($"osu! Bridge {UpdateUtils.CurrentVersion} - Console Edition");
        Console.WriteLine("Type 'exit' to quit.");
        Console.WriteLine("Type 'help' to view the list of available commands.\n");

        CheckUpdate().Wait();

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

    private static async Task CheckUpdate()
    {
        var (result, latestVersion, changeLog) = await UpdateUtils.CheckUpdate();

        if (result)
        {
            Console.WriteLine("A new version of osu! Bridge is available.");
            Console.WriteLine("Type 'update' to open the latest release page.");
            Console.WriteLine("");
            Console.WriteLine($"Version: {UpdateUtils.CurrentVersion} → {latestVersion}");
            Console.WriteLine("");
            Console.WriteLine($"ChangeLog:\n{changeLog}");
            Console.WriteLine("");
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
                case "duplicate": DuplicateCommand(osuBridge, parts); break;
                case "select": SelectCommand(osuBridge, parts); break;
                case "setfolder": SetFolderCommand(osuBridge, parts); break;
                case "launch": LaunchCommand(osuBridge); break;
                case "save": SaveCommand(osuBridge); break;
                case "edit": EditCommand(osuBridge, parts); break;
                case "remove": RemoveCommand(osuBridge, parts); break;
                case "lazermode": LazerModeCommand(osuBridge); break;
                case "help": HelpCommand(); break;
                case "update": UpdateCommand(); break;
                default: Console.WriteLine($"Unknown command: {parts[0]}\n"); break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Command Error!\nError: {0}\n", ex);
        }
    }

    #region Commands
    private static void LoadCommand(OsuBridge osuBridge)
    {
        osuBridge.Load();
    }
    private static void CreateCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].ToLower().StartsWith('p'))
        {
            var profileIndex = osuBridge.CreateProfile();
            Console.WriteLine("Profile Created!\nProfile Index: {0}\n", profileIndex);
            osuBridge.SelectProfile(profileIndex);
        }
        else if (args[1].ToLower().StartsWith('s'))
        {
            var serverIndex = osuBridge.CreateProfile();
            Console.WriteLine("Server Created!\nServer Index: {0}\n", serverIndex);
            osuBridge.SelectServer(serverIndex);
        }
    }
    private static void DuplicateCommand(OsuBridge osuBridge, string[] args)
    {
        var targetProfileIndex = int.Parse(args[2]);

        if (args[1].ToLower().StartsWith('p'))
        {
            var profileIndex = osuBridge.DuplicateProfile(targetProfileIndex);
            if (profileIndex != -1)
            {
                Console.WriteLine("Profile Duplicated!\nProfile Index: {0}\n", profileIndex);
                osuBridge.SelectProfile(profileIndex);
            }
            else
            {
                Console.WriteLine("Profile Duplication Failed...\n");
            }
        }
        else if (args[1].ToLower().StartsWith('s'))
        {
            var serverIndex = osuBridge.DuplicateServer(targetProfileIndex);
            if (serverIndex != -1)
            {
                Console.WriteLine("Server Duplicated!\nServer Index: {0}\n", serverIndex);
                osuBridge.SelectServer(serverIndex);
            }
            else
            {
                Console.WriteLine("Server Duplication Failed...\n");
            }
        }
    }
    private static void SelectCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].ToLower().StartsWith('p'))
        {
            try
            {
                var profileIndex = int.Parse(args[2]);
                var profile = osuBridge.SelectProfile(profileIndex);
                if (profile == null)
                {
                    Console.WriteLine("Profile Selection Failed...\n");
                    return;
                }

                Console.WriteLine("Profile Selected!\nProfile Name: {0}\n", profile.ProfileName);
            }
            catch
            {
                Console.WriteLine("Command Error!\n");
            }
        }
        else if (args[1].ToLower().StartsWith('s'))
        {
            try
            {
                var serverIndex = int.Parse(args[2]);
                var server = osuBridge.SelectServer(serverIndex);
                if (server == null)
                {
                    Console.WriteLine("Server Selection Failed...\n");
                    return;
                }

                Console.WriteLine("Server Selected!\nserver Name: {0}\n", server.Name);
            }
            catch
            {
                Console.WriteLine("Command Error!\n");
            }
        }
    }
    private static void SetFolderCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].ToLower().StartsWith('o'))
        {
            osuBridge.SetOsuFolder(args[2]);
            Console.WriteLine("osu! Folder Set!\nFolder Path: {0}\n", osuBridge.OsuFolderPath);
        }
        else if (args[1].ToLower().StartsWith('l'))
        {
            osuBridge.SetOsuLazerFolder(args[2]);
            Console.WriteLine("osu! Lazer Folder Set!\nFolder Path: {0}\n", osuBridge.OsuLazerFolderPath);
        }
        else if (args[1].ToLower().StartsWith('s'))
        {
            osuBridge.SetSongsFolder(args[2]);
            Console.WriteLine("Songs Folder Set!\nFolder Path: {0}\n", osuBridge.SongsFolderPath);
        }
    }
    private static void LaunchCommand(OsuBridge osuBridge)
    {
        Console.WriteLine("Launching osu!...\n");
        osuBridge.Launch();
    }
    private static void SaveCommand(OsuBridge osuBridge)
    {
        osuBridge.Save();
        Console.WriteLine("Configuration Saved!\n");
    }
    private static void EditCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].ToLower().StartsWith('p'))
        {
            bool result = osuBridge.EditProfile(args[2], args[3]);

            if (result)
            {
                Console.WriteLine("Profile Edited!\n");
            }
            else
            {
                Console.WriteLine("Profile Edit Failed...\n");
            }
        }
        else if (args[1].ToLower().StartsWith('s'))
        {
            bool result = osuBridge.EditServer(args[2], args[3]);

            if (result)
            {
                Console.WriteLine("Server Edited!\n");
            }
            else
            {
                Console.WriteLine("Server Edit Failed...\n");
            }
        }
    }
    private static void RemoveCommand(OsuBridge osuBridge, string[] args)
    {
        if (args[1].ToLower().StartsWith('p'))
        {
            bool result = osuBridge.RemoveProfile(int.Parse(args[2]));

            if (result)
            {
                Console.WriteLine("Profile Removed!\n");
            }
            else
            {
                Console.WriteLine("Profile Remove Failed...!\n");
            }
        }
        else if (args[1].ToLower().StartsWith('s'))
        {
            bool result = osuBridge.RemoveServer(int.Parse(args[2]));

            if (result)
            {
                Console.WriteLine("Server Removed!\n");
            }
            else
            {
                Console.WriteLine("Server Remove Failed...!\n");
            }
        }
    }
    private static void HelpCommand()
    {
        Console.WriteLine("\nCommand List:\n");

        Dictionary<string, string> commandDictionary = new()
        {
            { "Load", "Load the database configuration from the file" },
            { "Create [p/s]", "Create a new Profile (p) or Server (s) data entry" },
            { "Duplicate [p/s] [index]", "Duplicate the Profile (p) or Server (s) data at the specified index" },
            { "Select [p/s] [index]", "Select the Profile (p) or Server (s) data at the specified index and set it as the current active entry" },
            { "SetFolder [p/l/s] [path]", "Set the folder path for osu! (p), Lazer (l), or Songs (s) to the specified path" },
            { "Launch", "Launch the osu! game application" },
            { "Save", "Save the current settings and database to the file" },
            { "Edit [p/s] [key] [value]", "Modify the value of the property key for the currently selected Profile (p) or Server (s)" },
            { "Remove [p/s] [index]", "Delete the Profile (p) or Server (s) data at the specified index" },
            { "LazerMode", "Toggle the usage mode for the osu! Lazer version (ON/OFF)" },
            { "Update", "Open the latest release page" }
        };

        string format = "{0,-25} | {1}";

        Console.WriteLine(string.Format(format, "Command", "Description"));
        Console.WriteLine("--------------------------+-------------------------------------------------------------");

        foreach (var keyValuePair in commandDictionary)
        {
            Console.WriteLine(string.Format(format, keyValuePair.Key, keyValuePair.Value));
            Console.WriteLine("--------------------------+-------------------------------------------------------------");
        }

        Console.WriteLine("");
    }

    private static void LazerModeCommand(OsuBridge osuBridge)
    {
        osuBridge.SetLazerMode(!osuBridge.LazerMode);
        Console.WriteLine("Lazer Mode is now {0}!\n", osuBridge.LazerMode);
    }

    private static void UpdateCommand()
    {
        UpdateUtils.OpenGithubURL();
    }
    #endregion
}

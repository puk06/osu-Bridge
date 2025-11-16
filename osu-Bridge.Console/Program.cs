using osu_Bridge.Core.Services;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // 相対パスを取得し、カレントディレクトリを設定
        var currentDirectory = Path.GetDirectoryName(Process.GetCurrentProcess()?.MainModule?.FileName);
        if (currentDirectory != null) Directory.SetCurrentDirectory(currentDirectory);

        OsuBridge osuBridge = new("database.json");
        osuBridge.Load();
        
        Console.WriteLine("osu! Bridge - CUI Edition v1.0");
        Console.WriteLine("Type 'exit' to quit.\n");

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            if (input == null) continue;

            if (input.Trim().ToLower() == "exit")
            {
                Console.WriteLine("Bye!");
                break;
            }

            HandleCommand(input, osuBridge);
        }
    }

    static void HandleCommand(string command, OsuBridge osuBridge)
    {
        try
        {
            var parts = command.Split(' ');
            switch (parts[0].ToLower())
            {
                case "load":
                    {
                        osuBridge.Load();
                        break;
                    }
                case "create":
                    {
                        if (parts[1].StartsWith('p'))
                        {
                            var profileIndex = osuBridge.CreateProfile();
                            Console.WriteLine("Profile Successfully Created!\nProfile Index: {0}\n", profileIndex);
                            osuBridge.SelectProfile(profileIndex);
                        }
                        else if (parts[1].StartsWith('s'))
                        {
                            var serverIndex = osuBridge.CreateProfile();
                            Console.WriteLine("Server Successfully Created!\nServer Index: {0}\n", serverIndex);
                            osuBridge.SelectServer(serverIndex);
                        }

                        break;
                    }

                case "select":
                    {
                        if (parts[1].StartsWith('p'))
                        {
                            try
                            {
                                var profileIndex = int.Parse(parts[2]);
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
                        else if (parts[1].StartsWith('s'))
                        {
                            try
                            {
                                var serverIndex = int.Parse(parts[2]);
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

                        break;
                    }

                case "setfolder":
                    {
                        osuBridge.SetOsuFolder(parts[1]);
                        Console.WriteLine("osu! Folder Successfully Set!\nFolder Path: {0}\n", osuBridge.OsuFolderPath);
                        break;
                    }

                case "launch":
                    {
                        Console.WriteLine("Launching osu!...");
                        osuBridge.Launch();
                        break;
                    }
                case "save":
                    {
                        osuBridge.Save();
                        Console.WriteLine("Configuration Saved!");
                    }
                    break;

                case "edit":
                    {
                        if (parts[1].StartsWith('p'))
                        {
                            bool result = osuBridge.EditProfile(parts[2], parts[3]);

                            if (result)
                            {
                                Console.WriteLine("Profile Successfully Edited!\n");
                            }
                            else
                            {
                                Console.WriteLine("Profile Edit Failed...!\n");
                            }
                        }
                        else if (parts[1].StartsWith('s'))
                        {
                            bool result = osuBridge.EditServer(parts[2], parts[3]);

                            if (result)
                            {
                                Console.WriteLine("Server Successfully Edited!\n");
                            }
                            else
                            {
                                Console.WriteLine("Server Edit Failed...!\n");
                            }
                        }

                        break;
                    }

                case "remove":
                    {
                        if (parts[1].StartsWith('p'))
                        {
                            bool result = osuBridge.RemoveProfile(int.Parse(parts[2]));

                            if (result)
                            {
                                Console.WriteLine("Profile Successfully Removed!\n");
                            }
                            else
                            {
                                Console.WriteLine("Profile Remove Failed...!\n");
                            }
                        }
                        else if (parts[1].StartsWith('s'))
                        {
                            bool result = osuBridge.RemoveServer(int.Parse(parts[2]));

                            if (result)
                            {
                                Console.WriteLine("Server Successfully Removed!\n");
                            }
                            else
                            {
                                Console.WriteLine("Server Remove Failed...!\n");
                            }
                        }

                        break;
                    }

                default:
                    Console.WriteLine("Unknown command: " + command);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Command Error!" + ex);
        }
    }
}

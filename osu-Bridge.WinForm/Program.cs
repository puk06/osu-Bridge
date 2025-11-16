using osu_Bridge.WinForm.Forms;
using System.Diagnostics;

namespace osu_Bridge.WinForm;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // 相対パスを取得し、カレントディレクトリを設定
        var currentDirectory = Path.GetDirectoryName(Process.GetCurrentProcess()?.MainModule?.FileName);
        if (currentDirectory != null) Directory.SetCurrentDirectory(currentDirectory);

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LauncherLib.Login;
using LauncherLib.Net;

namespace LauncherLib.Play
{
    public class Game : IGame
    {
        public Game(IAccount account, string name, string path, string gameServerUri)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Name = name;
            Path = path;
            GameServer = gameServerUri;
        }

        public IAccount Account { get; }

        public string Name { get; }

        public string Path { get; }

        public string GameServer { get; }

        /// <summary>
        ///     Runs the game
        /// </summary>
        /// <returns>True if successfully ran; otherwise false.</returns>
        public async Task<bool> Run()
        {
            Environment.SetEnvironmentVariable("TT_USERNAME", Account.Username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", Account.Password);
            Environment.SetEnvironmentVariable("TT_GAMESERVER", await GetGameserver());

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden, // Hide the console window
                FileName = Path
            };

            Process p = Process.Start(startInfo);
            if (p != null)
            {
                p.WaitForExit();
                Console.WriteLine(p.ExitCode);
                return p.ExitCode == 3; // 3 is proper exit in python
            }
            return false;
        }

        private async Task<string> GetGameserver()
        {
            return await Http.GetGameServer(new Uri(GameServer));
        }
    }
}

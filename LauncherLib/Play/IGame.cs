using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LauncherLib.Login;

namespace LauncherLib.Play
{
    public interface IGame
    {
        IAccount Account { get; }
        string Name { get; }
        string Path { get; }
        string GameServer { get; }

        Task<bool> Run();
    }
}

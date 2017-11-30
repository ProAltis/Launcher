using System.Threading.Tasks;

namespace LauncherLib.Login
{
    public interface IAccount
    {
        string Username { get; }
        string Password { get; }
        Task<LoginAPIResponse> Login();
    }
}
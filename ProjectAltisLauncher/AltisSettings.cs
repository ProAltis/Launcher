using System.ComponentModel;
using Config.Net;

namespace ProjectAltis
{
    public class Settings
    {
        private static IAltisSettings _settings;

        public static IAltisSettings Instance => _settings ?? (_settings = new ConfigurationBuilder<IAltisSettings>().UseJsonFile("launchersettings.json").Build());
    }
    public interface IAltisSettings
    {
        string Username { get; set; }

        string Background { get; set; }

        [Option(DefaultValue = false)]
        bool WantCursor { get; set; }

        [Option(DefaultValue = true)]
        bool WantClickSound { get; set; }

        [Option(DefaultValue = false)]
        bool WantRandomBackgrounds { get; set; }

        [Option(DefaultValue = false)]
        bool WantSavePassword { get; set; }

        [Option(DefaultValue = true)]
        bool FirstRun { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAltisLauncher.Core
{
    public static class Audio
    {
        public static void PlaySoundFile(string filename)
        {
            if (!Properties.Settings.Default.wantsClickSounds) return;
            System.Media.SoundPlayer player;
            switch (filename.ToLower())
            {
                case "sndclick":
                    {
                        player = new System.Media.SoundPlayer(Properties.Resources.sndclick);
                        player.Load();
                        player.Play();
                        break;
                    }
            }
        }
    }
}

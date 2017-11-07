using System;
using System.Media;

namespace ProjectAltis.Core
{
	public static class Audio
	{
		public static void PlaySoundFile(string filename)
		{
			try
			{
				if (!Settings.Instance.WantClickSound) return;
				switch (filename.ToLower())
				{
					case "sndclick":
						{
							Log.Info("Playing sound click");
							new SoundPlayer(Properties.Resources.sndclick).Play();
							break;
						}
				}
			}
			catch (Exception ex)
			{
				Log.Error("UNABLE TO PLAY SOUND");
				Log.Error(ex);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAltisLauncher.Core
{
    public static class Background
    {
        private static Random _rand = new Random();
        public static Image ReturnRandomBackground()
        {
            int val = _rand.Next(1, 7); // Generates a random number 1-6
            Image BackgroundImage = Properties.Resources.TTC;
            switch (val)
            {
                case 1:
                    BackgroundImage = Properties.Resources.TTC;
                    break;
                case 2:
                    BackgroundImage = Properties.Resources.DD;
                    break;
                case 3:
                    BackgroundImage = Properties.Resources.DG;
                    break;
                case 4:
                    BackgroundImage = Properties.Resources.MML;
                    break;
                case 5:
                    BackgroundImage = Properties.Resources.Brrrgh;
                    break;
                case 6:
                    BackgroundImage = Properties.Resources.DDL;
                    break;
            }
            return BackgroundImage;
        }
        public static Image ReturnBackground(string bg)
        {
            Image BackgroundImage = Properties.Resources.TTC;
            switch (bg)
            {
                case "TTC":
                    BackgroundImage = Properties.Resources.TTC;
                    break;
                case "DD":
                    BackgroundImage = Properties.Resources.DD;
                    break;
                case "DG":
                    BackgroundImage = Properties.Resources.DG;
                    break;
                case "MML":
                    BackgroundImage = Properties.Resources.MML;
                    break;
                case "Brrrgh":
                    BackgroundImage = Properties.Resources.Brrrgh;
                    break;
                case "DDL":
                    BackgroundImage = Properties.Resources.DDL;
                    break;
            }
            return BackgroundImage;
        }
    }
}

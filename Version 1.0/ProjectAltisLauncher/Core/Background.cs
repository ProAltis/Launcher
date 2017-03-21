using System;
using System.Drawing;

namespace ProjectAltisLauncher.Core
{
    public static class Background
    {
        private static readonly Random _rand = new Random();
        public static Image ReturnRandomBackground()
        {
            int val = _rand.Next(1, 7); // Generates a random number 1-6
            Image backgroundImage = Properties.Resources.TTC;
            switch (val)
            {
                case 1:
                    backgroundImage = Properties.Resources.TTC;
                    break;
                case 2:
                    backgroundImage = Properties.Resources.DD;
                    break;
                case 3:
                    backgroundImage = Properties.Resources.DG;
                    break;
                case 4:
                    backgroundImage = Properties.Resources.MML;
                    break;
                case 5:
                    backgroundImage = Properties.Resources.Brrrgh;
                    break;
                case 6:
                    backgroundImage = Properties.Resources.DDL;
                    break;
            }
            return backgroundImage;
        }
        public static Image ReturnBackground(string bg)
        {
            Image backgroundImage = Properties.Resources.TTC;
            switch (bg)
            {
                case "TTC":
                    backgroundImage = Properties.Resources.TTC;
                    break;
                case "DD":
                    backgroundImage = Properties.Resources.DD;
                    break;
                case "DG":
                    backgroundImage = Properties.Resources.DG;
                    break;
                case "MML":
                    backgroundImage = Properties.Resources.MML;
                    break;
                case "Brrrgh":
                    backgroundImage = Properties.Resources.Brrrgh;
                    break;
                case "DDL":
                    backgroundImage = Properties.Resources.DDL;
                    break;
            }
            return backgroundImage;
        }
        public static Image ImageChooser(string name, string method)
        {
            method = method.ToLower();
            Image img = null;
            switch (name)
            {
                case "website":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.website_h;
                        case "mouseleave":
                            return Properties.Resources.website;
                        case "mousedown":
                            return Properties.Resources.website_d;
                        case "mouseup":
                            return Properties.Resources.website;
                    }
                    break;
                case "discord":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.discord_h;
                        case "mouseleave":
                            return Properties.Resources.discord;
                        case "mousedown":
                            return Properties.Resources.discord_d;
                        case "mouseup":
                            return Properties.Resources.discord;
                    }
                    break;
                case "group":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.group_h;
                        case "mouseleave":
                            return Properties.Resources.group;
                        case "mousedown":
                            return Properties.Resources.group_d;
                        case "mouseup":
                            return Properties.Resources.group;
                    }
                    break;
                case "theme":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.theme_h;
                        case "mouseleave":
                            return Properties.Resources.theme;
                        case "mousedown":
                            return Properties.Resources.theme_d;
                        case "mouseup":
                            return Properties.Resources.theme;
                    }
                    break;
                case "options":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.options_h;
                        case "mouseleave":
                            return Properties.Resources.options;
                        case "mousedown":
                            return Properties.Resources.options_d;
                        case "mouseup":
                            return Properties.Resources.options;
                    }
                    break;
                case "credits":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.credits_h;
                        case "mouseleave":
                            return Properties.Resources.credits;
                        case "mousedown":
                            return Properties.Resources.credits_d;
                        case "mouseup":
                            return Properties.Resources.credits;
                    }
                    break;
                case "play":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.play_h;
                        case "mouseleave":
                            return Properties.Resources.play;
                        case "mousedown":
                            return Properties.Resources.play_d;
                        case "mouseup":
                            return Properties.Resources.play;
                    }
                    break;
                case "exit":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.cancel_h;
                        case "mouseleave":
                            return Properties.Resources.cancel;
                    }
                    break;
                case "min":
                    switch (method)
                    {
                        case "mouseenter":
                            return Properties.Resources.minus_h;
                        case "mouseleave":
                            return Properties.Resources.minus;
                    }
                    break;
                case "contentpacks":
                    switch(method)
                    {
                        case "mouseenter":
                            return Properties.Resources.contentpacks_h;
                        case "mouseleave":
                            return Properties.Resources.contentpacks;
                        case "mousedown":
                            return Properties.Resources.contentpacks_d;
                        case "mouseup":
                            return Properties.Resources.contentpacks;
                    }
                    break;
            }
            return img;
        }
    }
}

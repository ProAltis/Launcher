#region License

// The MIT License
// 
// Copyright (c) 2017 Project Altis
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Drawing;
using ProjectAltisLauncher.Properties;

namespace ProjectAltisLauncher.Core
{
    public static class Graphics
    {
        private static readonly Random _rand = new Random();

        public static Image ReturnRandomBackground()
        {
            int val = _rand.Next(1, 7); // Generates a random number 1-6
            Image backgroundImage = Resources.TTC;
            switch (val)
            {
                case 1:
                    backgroundImage = Resources.TTC;
                    break;
                case 2:
                    backgroundImage = Resources.DD;
                    break;
                case 3:
                    backgroundImage = Resources.DG;
                    break;
                case 4:
                    backgroundImage = Resources.MML;
                    break;
                case 5:
                    backgroundImage = Resources.Brrrgh;
                    break;
                case 6:
                    backgroundImage = Resources.DDL;
                    break;
            }
            return backgroundImage;
        }

        public static Image ReturnBackground(string bg)
        {
            Image backgroundImage = Resources.TTC;
            switch (bg)
            {
                case "TTC":
                    backgroundImage = Resources.TTC;
                    break;
                case "DD":
                    backgroundImage = Resources.DD;
                    break;
                case "DG":
                    backgroundImage = Resources.DG;
                    break;
                case "MML":
                    backgroundImage = Resources.MML;
                    break;
                case "Brrrgh":
                    backgroundImage = Resources.Brrrgh;
                    break;
                case "DDL":
                    backgroundImage = Resources.DDL;
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
                            return Resources.website_h;
                        case "mouseleave":
                            return Resources.website;
                        case "mousedown":
                            return Resources.website_d;
                        case "mouseup":
                            return Resources.website;
                    }
                    break;
                case "discord":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.discord_h;
                        case "mouseleave":
                            return Resources.discord;
                        case "mousedown":
                            return Resources.discord_d;
                        case "mouseup":
                            return Resources.discord;
                    }
                    break;
                case "group":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.group_h;
                        case "mouseleave":
                            return Resources.group;
                        case "mousedown":
                            return Resources.group_d;
                        case "mouseup":
                            return Resources.group;
                    }
                    break;
                case "theme":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.theme_h;
                        case "mouseleave":
                            return Resources.theme;
                        case "mousedown":
                            return Resources.theme_d;
                        case "mouseup":
                            return Resources.theme;
                    }
                    break;
                case "options":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.options_h;
                        case "mouseleave":
                            return Resources.options;
                        case "mousedown":
                            return Resources.options_d;
                        case "mouseup":
                            return Resources.options;
                    }
                    break;
                case "credits":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.credits_h;
                        case "mouseleave":
                            return Resources.credits;
                        case "mousedown":
                            return Resources.credits_d;
                        case "mouseup":
                            return Resources.credits;
                    }
                    break;
                case "play":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.play_h;
                        case "mouseleave":
                            return Resources.play;
                        case "mousedown":
                            return Resources.play_d;
                        case "mouseup":
                            return Resources.play;
                    }
                    break;
                case "exit":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.cancel_h;
                        case "mouseleave":
                            return Resources.cancel;
                    }
                    break;
                case "min":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.minus_h;
                        case "mouseleave":
                            return Resources.minus;
                    }
                    break;
                case "contentpacks":
                    switch (method)
                    {
                        case "mouseenter":
                            return Resources.contentpacks_h;
                        case "mouseleave":
                            return Resources.contentpacks;
                        case "mousedown":
                            return Resources.contentpacks_d;
                        case "mouseup":
                            return Resources.contentpacks;
                    }
                    break;
            }
            return img;
        }
    }
}
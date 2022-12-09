using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Packaging;
using System.Reflection;
using Fair_Trade.GameClasses.GameBase.BasicCardMechanics;

namespace Fair_Trade.GameClasses
{
    public static class GameMode
    {
        public static string gameMode;
        private static List<Card> avaliableForDeckCards;

        private static int[] _screenResolution = new int[2] { 800, 480 };
        private static string _botsDifficulty = "Normal";
        private static bool _fullScreen = true;
        private static int _musicLevel = 100;
        private static int _SFXLevel = 100;
        private static bool _animationsOn = true;
        private static bool _chatIsBlocked = false;

        private static List<int[]> _possibleResolutions = new List<int[]> {
            new int[2] { 800, 480 }, new int[2] { 800, 600 }, new int[2] { 960, 640 },
            new int[2] { 1024, 768 }, new int[2] { 1152, 864 }, new int[2] { 1280, 720},
            new int[2] { 1280, 768 }, new int[2] { 1280, 800 }, new int[2] { 1280, 960 },
            new int[2] { 1280, 1024 }, new int[2] { 1400, 1050 }, new int[2] { 1440, 900 },
            new int[2] { 1600, 900 }, new int[2] { 1680, 1050 }, new int[2] { 1600, 1200 },
            new int[2] { 1920, 1080 }, new int[2] { 1920, 1200 }, new int[2] { 1920, 1280 },
            new int[2] { 1920, 1400 }, new int[2] { 2048, 1080 }, new int[2] { 2048, 1152 },
            new int[2] { 2048, 1536 }, new int[2] { 2560, 1080 }, new int[2] { 2560, 1440 },
            new int[2] { 2560, 1600 }, new int[2] { 2560, 2048 }, new int[2] { 2880, 900 },
            new int[2] { 2960, 1440 }, new int[2] { 3440, 1440 }, new int[2] { 2880, 900 },
            new int[2] { 2800, 2100 }, new int[2] { 3200, 1080 }, new int[2] { 3200, 2048 },
            new int[2] { 3200, 2400 }, new int[2] { 3840, 2160 }};
        private static List<string> _difficultyModes = new List<string> { "Easy", "Normal", "Hard", "Scam" };

        public static Dictionary<List<string>, Type> GetSettingsDictionary()
        {
            Dictionary<List<string>, Type> settingsNameValueType = new Dictionary<List<string>, Type>();
            StringBuilder resolutionString = new StringBuilder(); resolutionString.Append(_screenResolution[0].ToString());
            resolutionString.Append(" x "); resolutionString.Append(_screenResolution[1].ToString());
            settingsNameValueType.Add(new List<string> { "Resolution", resolutionString.ToString() }, _screenResolution.GetType());
            settingsNameValueType.Add(new List<string> { "Full screen", _fullScreen.ToString() }, _fullScreen.GetType());
            settingsNameValueType.Add(new List<string> { "Bots difficulty", _botsDifficulty.ToString() }, _botsDifficulty.GetType());
            settingsNameValueType.Add(new List<string> { "Music", _musicLevel.ToString() }, _musicLevel.GetType());
            settingsNameValueType.Add(new List<string> { "SFX", _SFXLevel.ToString() }, _SFXLevel.GetType());
            settingsNameValueType.Add(new List<string> { "Animations", _animationsOn.ToString() }, _animationsOn.GetType());
            settingsNameValueType.Add(new List<string> { "Chat is blocked", _chatIsBlocked.ToString() }, _chatIsBlocked.GetType());
            return settingsNameValueType;
        }

        public static List<int[]> GetPossibleResolutions()
        {
            List<int[]> pR = new List<int[]>();
            int[] fullscreen = new int[2];
            fullscreen[0] = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            fullscreen[1] = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            foreach (int[] i in _possibleResolutions)
            {
                if (i[0] < fullscreen[0] && i[1] < fullscreen[1]) pR.Add(i);
            }
            return pR;
        }

        public static void SetBotDifficulty(string difficulty) => _botsDifficulty = difficulty;
        public static string GetBotDifficulty() => _botsDifficulty;

        public static void SetResolution(int width, int height) { _screenResolution[0] = width; _screenResolution[1] = height; }
        public static int[] GetResolution() => _screenResolution;

        public static void SetMusicLevel(int level) => _musicLevel = (level >= 0 && level <= 100 ? level : 0);
        public static int GetMusicLevel() => _musicLevel;

        public static void SetSFXLevel(int level) => _SFXLevel = (level >= 0 && level <= 100 ? level : 0);
        public static int GetSFXLevel() => _SFXLevel;

        public static void SetChatMode(bool isBlocked) => _chatIsBlocked = isBlocked;
        public static bool GetChatMode() => _chatIsBlocked;

        public static void SetFullScreenMode(bool mode) => _fullScreen = mode;
        public static bool GetFullScreenMode() => _fullScreen;

        public static void SetAnimationsOn(bool to) => _animationsOn = to;
        public static bool GetAnimationsOn() => _animationsOn;
        public static List<string> GetAllDifficultyModes() => _difficultyModes;

        public static void FormatWindow(Window w)
        {
            if (_fullScreen == true)
            {
                w.WindowState = WindowState.Maximized;
                return;
            }
            w.WindowState = WindowState.Normal;
            w.Width = _screenResolution[0];
            w.Height = _screenResolution[1];
        }
        

        public static void UploadSettings()
        {
            FileInfo fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory);
            string[] s = File.ReadAllText(fileInfo.Directory.Parent.Parent.Parent.FullName + @"\Data\Settings_File.txt").Split("\n");
            string[] res = s[0].Split("x");
            _screenResolution[0] = Int32.Parse(res[0]); _screenResolution[1] = Int32.Parse(res[1]);
            _botsDifficulty = (s[1].TrimEnd());
            if (s[2] == "True") _fullScreen = true;
            else _fullScreen = false;
            _musicLevel = Int32.Parse(s[3]);
            _SFXLevel = Int32.Parse(s[4]);
            if (s[5] == "True") _animationsOn = true;
            else _animationsOn = false;
            if (s[6] == "True") _chatIsBlocked = true;
            else _chatIsBlocked = false;
        }
        public static async void SaveSettings()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_screenResolution[0].ToString()); sb.Append("x"); sb.Append(_screenResolution[1].ToString()); sb.Append("\n");
            sb.AppendLine(_botsDifficulty);
            sb.AppendLine(_fullScreen.ToString());
            sb.AppendLine(_musicLevel.ToString());
            sb.AppendLine(_SFXLevel.ToString());
            sb.AppendLine(_animationsOn.ToString());
            sb.AppendLine(_chatIsBlocked.ToString());
            FileInfo fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory);
            await File.WriteAllTextAsync(fileInfo.Directory.Parent.Parent.Parent.FullName + @"\Data\Settings_File.txt", sb.ToString());
        }
    }
}

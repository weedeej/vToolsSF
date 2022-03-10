using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vTools_Session_Fetch.Constants
{
    public static class Strings
    {
        public static String SettingsYamlFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Riot Games/Riot Client/Data/RiotGamesPrivateSettings.yaml";
        public static String LogFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/VTools/vToolsSession/log.txt";
        public static String AppDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/VTools/vToolsSession";
    }
}

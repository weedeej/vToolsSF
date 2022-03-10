using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using vTools_Session_Fetch.Constants;
using vTools_Session_Fetch.Objects;
using vTools_Session_Fetch.Logger;
using System.Diagnostics;

namespace vTools_Session_Fetch.Listener
{
    public static class Observer
    {
        public static async Task<SettingsYaml> ReadSettingsYAML(Object sender)
        {
            var deserializer = new DeserializerBuilder().Build();
            SettingsYaml yamlContents = null;
            StackPanel outputPanel = (StackPanel)sender;
            InfoLogger.Log(outputPanel, "Looping Started");
            while (yamlContents == null)
            {
                await Task.Delay(1000);
                try
                {
                    Logger.InfoLogger.Log(outputPanel, "Opening Yaml file");
                    using (FileStream fs = File.Open(Strings.SettingsYamlFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        Logger.InfoLogger.Log(outputPanel, "Reading Yaml file");
                        using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                        {
                            if (!sr.ReadToEnd().Contains("ssid"))
                            {
                                InfoLogger.Log(outputPanel, "Session can't be found! Login on Riot Client with \"Remember\" checkbox checked. Looping back.");
                                continue;
                            }
                            sr.BaseStream.Position = 0;
                            InfoLogger.Log(outputPanel, "Saved Session found. Deserializing.");
                            yamlContents = deserializer.Deserialize<SettingsYaml>(sr);
                        }
                    }
                    File.Delete(Strings.SettingsYamlFile);
                    var ritoClient = Process.GetProcessesByName("RiotClientServices").FirstOrDefault();
                    if (ritoClient != null) ritoClient.Kill();
                }
                catch (FileNotFoundException)
                {
                    InfoLogger.Log(outputPanel, "YAML File does not exist. Login on Riot Client with \"Remember\" checkbox checked. Looping back.");
                    continue;
                }
                catch (Exception ex)
                {
                    InfoLogger.Log(outputPanel, $"Unknown Exception Occured - {ex.Message}:{ex.StackTrace}. Looping back.");
                    continue;
                }
            }
            return yamlContents;
        }
    }
}
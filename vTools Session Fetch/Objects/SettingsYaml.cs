using System;
using System.Collections.Generic;
using System.Globalization;
using YamlDotNet.Serialization;

namespace vTools_Session_Fetch.Objects
{
    public partial class SettingsYaml
    {

        [YamlMember(Alias = "riot-login")]
        public RiotLogin RiotLogin { get; set; }

        public string Serialize()
        {
            string Cookies = String.Empty;
            for (int i = 0; i < this.RiotLogin.Persist.Session.Cookies.Length; i++)
            {
                var cookie = this.RiotLogin.Persist.Session.Cookies[i];
                Cookies += $"{cookie.Name}={cookie.Value};";
            }
            return Cookies;
        }
    }

    public partial class RiotLogin
    {
        [YamlMember(Alias = "persist")]
        public Persist Persist { get; set; }
    }

    public partial class Persist
    {
        [YamlMember(Alias = "region")]
        public string Region { get; set; }

        [YamlMember(Alias = "session")]
        public Session Session { get; set; }
    }

    public partial class Session
    {
        [YamlMember(Alias = "cookies")]
        public Cooky[] Cookies { get; set; }
    }

    public partial class Cooky
    {
        [YamlMember(Alias = "domain")]
        public string Domain { get; set; }

        [YamlMember(Alias = "hostOnly")]
        public bool HostOnly { get; set; }

        [YamlMember(Alias = "httpOnly")]
        public bool HttpOnly { get; set; }

        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "path")]
        public string Path { get; set; }

        [YamlMember(Alias = "persistent")]
        public bool Persistent { get; set; }

        [YamlMember(Alias = "secureOnly")]
        public bool SecureOnly { get; set; }

        [YamlMember(Alias = "value")]
        public string Value { get; set; }
    }
    public class SerializedCookies
    {
        public string Cookies = String.Empty;
        public SerializedCookies(SettingsYaml self)
        {
            for (int i = 0;i < self.RiotLogin.Persist.Session.Cookies.Length;i++)
            {
                var cookie = self.RiotLogin.Persist.Session.Cookies[i];
                Cookies += $"{cookie.Name}= {cookie.Value};";
            }
        }
    }
}

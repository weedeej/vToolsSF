using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using vTools_Session_Fetch.Constants;

namespace vTools_Session_Fetch.Logger
{
    public static class InfoLogger
    {
        public static void Log(StackPanel panel = null, String message = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DateTimeOffset.Now} | {message}\n");
            try
            {
                File.AppendAllText(Strings.LogFile, sb.ToString());
            } catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Strings.AppDir);
                File.AppendAllText(Strings.LogFile, sb.ToString());
            }
            if (panel == null)
            {
                sb.Clear();
                return;
            }
            var fontConverter = new FontFamilyConverter();
            var TextLog = new TextBlock()
            {
                Text = sb.ToString(),
                FontFamily = (FontFamily)fontConverter.ConvertFromString("Century Gothic"),
                Foreground = Brushes.White,
                FontSize = 16,
                Padding = new Thickness(0),
                TextWrapping = System.Windows.TextWrapping.Wrap,
            };
            TextLog.MouseLeftButtonUp += TextBlock_MouseUp;
            panel.Children.Add(TextLog);
            sb.Clear();
        }

        private static void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var textblock = (TextBlock)sender;
            if (textblock.Text.Contains("Auth using this Port Number"))
            {
                Clipboard.SetText(textblock.Text.Split(": ")[1].Split('.')[0].Trim());
                return;
            }
            Clipboard.SetText(textblock.Text);
        }
    }
}

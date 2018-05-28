using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UTM_Changer.Parser;

namespace UTM_Changer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseFunctions baseFunctions;
        private UserPrefs prefs;
        Settings settingsWindow;

        ParserWorker<string[]> parser;

        public MainWindow()
        {
            baseFunctions = new BaseFunctions(this);
            prefs = baseFunctions.loadSettings();
            InitializeComponent();
            applyPrefs();
            parser = new ParserWorker<string[]>(
                new HabraParser());
            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            Console.WriteLine("________________");
            foreach (var item in arg2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("________________");

        }

        private void Parser_OnCompleted(object obj)
        {
            MessageBox.Show("OK!");
        }

        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow = new Settings(prefs);
            settingsWindow.Owner = this;
            settingsWindow.Show();
            this.Hide();
        }
        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (source != null & medium != null)
            {
                StringToRichTextBox(outputText, baseFunctions.createURL(source.Text, medium.Text, StringFromRichTextBox(inputText)));
            }

        }

        private string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }
        private void StringToRichTextBox(RichTextBox rtb, string text)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            textRange.Text = text;
        }

        private void applyPrefs()
        {
            source.Text = prefs.Utm_source;
            medium.Text = prefs.Utm_medium;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            prefs.Utm_medium = medium.Text;
            prefs.Utm_source = source.Text;
            baseFunctions.saveSettings(prefs);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new HabraSettings(1, 3);
            parser.Start();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            parser.Abort();
        }
    }
}

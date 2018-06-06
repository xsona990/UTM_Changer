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

        public MainWindow()
        {
            baseFunctions = new BaseFunctions(this);
            prefs = baseFunctions.loadSettings();
            InitializeComponent();
            applyPrefs();
           
        }
        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow = new Settings(prefs);
            settingsWindow.Owner = this;
            settingsWindow.Show();
            this.Hide();
        }
        //TODO
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
            refillParsers();
        }
        public void refillParsers()
        {
            parsers.Items.Clear();
            parsers.ItemsSource = prefs.Parsers.Keys;
            parsers.SelectedIndex = 0;
        }
        public void refreshParsersCb()
        {
            parsers.Items.Refresh();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            prefs.Utm_medium = medium.Text;
            prefs.Utm_source = source.Text;
            baseFunctions.saveSettings(prefs);
        }

        private void linkCount_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            //  Console.WriteLine("siteCount_Scroll" + siteCount.Value);
            linkCount.Value = (int)linkCount.Value;
            linkCountIndicator.Content = linkCount.Value;
           
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                int depObjCount = VisualTreeHelper.GetChildrenCount(depObj);
                for (int i = 0; i < depObjCount; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    if (child is GroupBox)
                    {
                        GroupBox gb = child as GroupBox;
                        Object gpchild = gb.Content;
                        if (gpchild is T)
                        {
                            yield return (T)child;
                            child = gpchild as T;
                        }
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void parseBtn_Click(object sender, RoutedEventArgs e)
        {
            parsedContent.Items.Clear();

           ParserCreator parser;
            prefs.Parsers.TryGetValue(parsers.SelectedValue.ToString(), out parser);
            parser.EndPoint = 1;
            try
            {
                    parser.Parse();               
                
            }
            catch (NullReferenceException)
            {

                throw;
            }
            if (parser.ResultList!=null)
            {
                foreach (var item in parser.ResultList)
                {
                    parsedContent.Items.Add(new ListViewItemsStructure { PostTitle = item.getText(), PostLink = item.getHrefLink(), UTMLable = "null" });
                   
                }
            }
        }

    }

    public class ListViewItemsStructure
    {
        public string PostTitle { get; set; }

        public string PostLink { get; set; }

        public string UTMLable { get; set; }
    }
}

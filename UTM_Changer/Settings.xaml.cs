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
using System.Windows.Shapes;

namespace UTM_Changer
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {

        private UserPrefs userPrefs;
        public Settings()
        {
            InitializeComponent();
        }
        public Settings(UserPrefs prefs)
        {
            this.userPrefs = prefs;
            InitializeComponent();
            shouldSaveData.IsChecked = prefs.ShouldSaveData;

        }

        private void SettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();
        }

        private void shouldSaveData_Checked(object sender, RoutedEventArgs e)
        {
            userPrefs.ShouldSaveData = true;
        }

        private void shouldSaveData_Unchecked(object sender, RoutedEventArgs e)
        {
            userPrefs.ShouldSaveData = false;
        }
    }
}

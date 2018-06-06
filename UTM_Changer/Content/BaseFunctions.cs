using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UTM_Changer.Parser;

namespace UTM_Changer
{
    public class BaseFunctions
    {

        private MainWindow mainWindow;
        private static string appData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        private static string path = appData + "\\UTMChangerData" + "\\config.dat";
        public BaseFunctions() { }

        public BaseFunctions(MainWindow main)
        {
            mainWindow = main;
        }


        public string createURL(string utm_source, string utm_medium, string inputUrl)
        {
            inputUrl = inputUrl.Replace("\n", "");
            inputUrl = inputUrl.Replace("\r", "");
            string outputUrl = inputUrl + "?utm_source=" + utm_source + "&utm_medium=" + utm_medium + "&utm_campaign=";
            string[] tempInputedText = inputUrl.Split('-');
            for (int i = 1; i < tempInputedText.Length; i++)
            {
                if (i != tempInputedText.Length - 1)
                {
                    outputUrl += tempInputedText[i] + "-";
                }
                else
                {
                    outputUrl += tempInputedText[i];
                }
            }

            return outputUrl;
        }


        //Десериализует обьект настроек и создает класс настроек
        internal UserPrefs loadSettings()
        {
            UserPrefs userPrefs = null;
            if (Directory.Exists(appData + "\\UTMChangerData"))
            {
                if (File.Exists(appData + "\\UTMChangerData\\config.dat"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        try
                        {
                            userPrefs = (UserPrefs)formatter.Deserialize(fs);
                        }
                        catch (Exception)
                        {
                            //throw;
                           
                        }
                      
                    }
                }
                else
                {
                    userPrefs = new UserPrefs();
                    if (userPrefs.Parsers.Keys.Count == 0)
                    {
                        userPrefs.addParser("habr", new ParserCreator("post__title_link", "a", "https://habr.com/", "page{CurrentId}"));
                    }
                }
            }
            else
            {
                userPrefs = new UserPrefs();
                if (userPrefs.Parsers.Keys.Count == 0)
                {
                    userPrefs.addParser("habr", new ParserCreator("post__title_link", "a", "https://habr.com/", "page{CurrentId}"));
                }
            }

            // Console.WriteLine("Объект десериализован");
            return userPrefs;

        }
        //Сериализирует класс настроек и сохраняет его в файл
        internal void saveSettings(UserPrefs userPrefs)
        {
            if (userPrefs.ShouldSaveData)
            {
                // создаем объект BinaryFormatter
                BinaryFormatter formatter = new BinaryFormatter();
                // получаем поток, куда будем записывать сериализованный объект

                if (!Directory.Exists(appData + "\\UTMChangerData"))
                {
                      Directory.CreateDirectory(appData + "\\UTMChangerData");
                }

                
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    try
                    {
                        formatter.Serialize(stream, userPrefs);
                    }
                    catch (Exception)
                    {
                       // throw;
                    }
                }
            }
        }


    }
}

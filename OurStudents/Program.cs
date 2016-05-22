using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace OurStudents
{
    static class Program
    {
        public static string DBName
        {
            get { return ConfigurationManager.AppSettings["Connection"]; }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["Connection"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private const string APPGuid = "2a4ff4b2-6194-47b4-a9a7-81d090ac9ab2";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var mutex = new Mutex(false, "Global\\" + APPGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Экземпляр программы уже запущен.");
                    return;
                }

                Application.Run(new MainForm());
            }
        }
    }
}

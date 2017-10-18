using FrequencyAnalyzer.Controller;
using FrequencyAnalyzer.Model;
using FrequencyAnalyzer.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FrequencyAnalyzer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
        }

        [STAThread]
        public static void Main()
        {
            MainWindow mainWindow = new MainWindow();
            FileController controller = new FileController(new Vocabulary(), mainWindow);

            App app = new App { MainWindow = mainWindow };
            mainWindow.Show();
            app.Run();
        }
    }
}

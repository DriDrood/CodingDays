using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string programPath = Environment.GetCommandLineArgs()[0];
            string programName = System.IO.Path.GetFileNameWithoutExtension(programPath);
            bool correctFileName = programName == "featureNotBug";

            TextBlock text = new TextBlock();
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.Text = correctFileName
                ? "mysql: "
                : "Wrong file name!";
            text.FontSize = 20;
            main.Children.Add(text);

            if (!correctFileName)
            {
                Task.Run(() => {
                    Process notepad = new Process();
                    notepad.StartInfo.FileName = programPath;
                    notepad.Start();

                    int i = 0;
                    while (true)
                    {
                        i = (i + 1) % 1000;
                    }
                });
            }
        }
    }
}

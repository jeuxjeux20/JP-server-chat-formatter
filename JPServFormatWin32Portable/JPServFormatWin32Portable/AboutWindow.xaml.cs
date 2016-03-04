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

namespace JPServFormatWin32Portable
{
    /// <summary>
    /// Logique d'interaction pour AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public string version { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public AboutWindow()
        {
            InitializeComponent();
            textBlock1.Text = version;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

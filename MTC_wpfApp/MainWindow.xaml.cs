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

namespace MTC_wpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            clientGrid.ItemsSource = Models.MTCEntities.GetContext().Clients.ToList();
        }

        private void openClientwindowButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.CreateClient window = new Windows.CreateClient();
            window.Show();
            this.Hide();
        }
    }
}

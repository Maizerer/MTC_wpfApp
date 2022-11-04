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

namespace MTC_wpfApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для CitiesTable.xaml
    /// </summary>
    public partial class CitiesTable : Window
    {
        public CitiesTable()
        {
            InitializeComponent();
            updateDataGrid();
        }

        private void updateDataGrid()
        {
            cityGrid.ItemsSource = Models.MTCEntities.GetContext().Cities.ToList();
        }

        private void toHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}

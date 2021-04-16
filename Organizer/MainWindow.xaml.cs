using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Organizer.ViewModels;

namespace Organizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void FileOrganizer_Clicked(object sender, RoutedEventArgs e)
        {
            if(DataContext == null || DataContext.ToString() != "Organizer.ViewModels.FileOrganizer_Model")
            {
                DataContext = new FileOrganizer_Model();
            }
            else
            {
                DataContext = null;
            }
        }

        private void MovieSorter_Clicked(object sender, RoutedEventArgs e)
        {
            if (DataContext == null || DataContext.ToString() != "Organizer.ViewModels.MovieSorter_Model")
            {
                DataContext = new MovieSorter_Model();
            }
            else
            {
                DataContext = null;
            }
        }
    }
}

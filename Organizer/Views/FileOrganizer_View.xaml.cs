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
using System.Windows.Forms;
using System.IO;
using Organizer.ViewModels;

namespace Organizer.Views
{
    /// <summary>
    /// Interaction logic for FileOrganizer_View.xaml
    /// </summary>
    public partial class FileOrganizer_View
    {
        int counter = 0;
        public string path;
        public FileOrganizer_View()
        {
            InitializeComponent();
        }

        public void Rename(FileInfo fi)
        {
            if(fi.Name == System.AppDomain.CurrentDomain.FriendlyName)
            {
                return;
            }
            fi.MoveTo(fi.DirectoryName + "\\" + counter++.ToString() + fi.Extension);

            ////----
            //yes im stoopid :> well, thats what happens when you learn, you realize old you knew shit

            //string oldName = fi.Name;
            //string appName = System.AppDomain.CurrentDomain.FriendlyName;
            //if (oldName == appName)
            //{
            //    return 1;
            //}
            //oldName = fi.FullName;
            //int index = oldName.LastIndexOf('.');
            //string extenstion = oldName.Substring(index, oldName.Length - index);

            //index = oldName.LastIndexOf('\\');
            //string newName = oldName.Substring(0, index);
            //newName = newName + "\\" + counter.ToString() + extenstion;

            //File.Move(oldName, newName);
            //return 0;

        }

        private void ButtonPath_Clicked(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openFileDig = new FolderBrowserDialog();
            var result = openFileDig.ShowDialog();
            path = "." ;

            if (result.ToString() != string.Empty)
            {
                TextBoxPath.Text = openFileDig.SelectedPath;
                path = openFileDig.SelectedPath;
            }
            else
            {
                TextBoxPath.Text = "error_path";
            }

            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] rgFiles = di.GetFiles("*.*");
                TextBoxFiles.Clear();
                foreach (FileInfo fi in rgFiles)
                {
                    TextBoxFiles.Text += fi.Name + "\n";
                }
                LabelWarning.Content = "WARNING:\nThis action cannot be undone,\nand will result in all the files \nin a given directory to be renamed!";
                ButtonRename.Visibility = Visibility.Visible;
                ButtonRename.IsEnabled = true;
            }
            else 
            {
                TextBoxFiles.Clear();
                TextBoxPath.Text = "...";
                LabelWarning.Content = "";
                ButtonRename.Visibility = Visibility.Hidden;
            }
            
        }

        private void ButtonRename_Clicked(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(path))
            {
                if (System.Windows.Forms.MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ButtonRename.IsEnabled = false;
                    DirectoryInfo di = new DirectoryInfo(path);
                    FileInfo[] rgFiles = di.GetFiles("*.*");

                    foreach (FileInfo fi in rgFiles)
                    {
                        Rename(fi);
                    }
                    TextBoxFiles.Text += "\n---------RENAMED---------\n";

                    rgFiles = di.GetFiles("*.*");
                    foreach (FileInfo fi in rgFiles)
                    {
                        TextBoxFiles.Text += fi.Name + "\n";
                    }
                    System.Windows.Forms.MessageBox.Show("Done.", "Done", MessageBoxButtons.OK);
                }
            }
            else 
            {
                System.Windows.Forms.MessageBox.Show("Invalid path.", "Invalid", MessageBoxButtons.OK);
            }

        }
    }
}

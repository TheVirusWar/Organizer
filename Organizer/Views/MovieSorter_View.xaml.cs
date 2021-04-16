using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Organizer.Views
{
    /// <summary>
    /// Interaction logic for MovieSorter_View.xaml
    /// </summary>
    public partial class MovieSorter_View
    {
        string path;
        string upperChoice = "";
        string lowerChoice = "";
        FileInfo[] rgFiles;
        int counter = 0;

        public void RenameAndPush(FileInfo fi1, FileInfo fi2, string name = "")
        {
            if (name == "")
            {
                name = counter.ToString();
            }

            if (!Directory.Exists(fi1.DirectoryName + "\\" + name))
            {
                    Directory.CreateDirectory(fi1.DirectoryName + "\\" + name);
            }

            if(!File.Exists(fi1.DirectoryName + "\\" + name + "\\" + name + fi1.Extension) && !File.Exists(fi2.DirectoryName + "\\" + name + "\\" + name + fi2.Extension))
            {
                fi1.MoveTo(fi1.DirectoryName + "\\" + name + "\\" + name + fi1.Extension);
                fi2.MoveTo(fi2.DirectoryName + "\\" + name + "\\" + name + fi2.Extension);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("File already exists.\n", "Exists", MessageBoxButtons.OK);
                return;
            }
            //fi1.MoveTo(fi1.DirectoryName + "\\" + name + "\\" + name + fi1.Extension);
            //fi2.MoveTo(fi2.DirectoryName + "\\" + name + "\\" + name + fi2.Extension);

            if (name == counter.ToString())
            {
                counter++;
            }
            
        }
        public void Load(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                rgFiles = di.GetFiles("*.*");
                Regex rxVid = new Regex(@"^.*\.(webm|mkv|flv|avi|wmv|mp4|m4p|m4v|mpg|mpeg)$"); //mostly good
                Regex rxSubt = new Regex(@"^.*\.(srt)$");

                List<string> videoFiles = new List<string>();
                List<string> subtFiles = new List<string>();
                foreach (FileInfo fi in rgFiles)
                {
                    if (rxVid.IsMatch(fi.Name))
                    {
                        videoFiles.Add(fi.Name);
                    }
                    if (rxSubt.IsMatch(fi.Name))
                    {
                        subtFiles.Add(fi.Name);
                    }
                }
                ListBoxUpper.ItemsSource = videoFiles;
                ListBoxLower.ItemsSource = subtFiles;
                LabelUpper.Content = "";
                LabelLower.Content = "";
            }
        }

        public MovieSorter_View()
        {
            InitializeComponent();
        }

        private void ButtonPath_Clicked(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openFileDig = new FolderBrowserDialog();
            var result = openFileDig.ShowDialog();
            path = ".";

            if (result.ToString() != string.Empty)
            {
                TextBoxPath.Text = openFileDig.SelectedPath;
                path = openFileDig.SelectedPath;
            }
            else
            {
                TextBoxPath.Text = "error_path";
            }

            Load(path);
        }

        private void ButtonPair_Clicked(object sender, RoutedEventArgs e)
        {
            FileInfo upper = null;
            FileInfo lower = null;

            if(rgFiles != null)
            {
                foreach (FileInfo fi in rgFiles)
                {
                    if(upperChoice == fi.Name)
                    {
                        upper = fi;
                    }
                    else if (lowerChoice == fi.Name)
                    {
                        lower = fi;
                    }
                }
                if (upper != null && lower != null)
                {
                    RenameAndPush(upper, lower, TextBoxName.Text);
                }

            }
            Load(path);
        }

        private void ListBoxUpper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxUpper.SelectedItem != null)
            {
                LabelUpper.Content = ListBoxUpper.SelectedItem.ToString();
                upperChoice = ListBoxUpper.SelectedItem.ToString();
            }
        }

        private void ListBoxLower_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxLower.SelectedItem != null)
            {
                LabelLower.Content = ListBoxLower.SelectedItem.ToString();
                lowerChoice = ListBoxLower.SelectedItem.ToString();
            }
        }

        private void ButtonInfo_Clicked(object sender, RoutedEventArgs e)
        {
            string message = "Click on \"path\" and specify the folder. In it, you should have video format files and .srt subtitles. " +
                "Pair the ones that fit, and click \"Pair\". \nIf the name isnt specified, " +
                "the program will use numbers starting from 0 to create a folder and move the selected files to that folder. If the name is specified, " +
                "the given name will be used instead.";

            System.Windows.Forms.MessageBox.Show(message, "Info" , MessageBoxButtons.OK);

        }
    }
}

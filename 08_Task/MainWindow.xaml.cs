using Microsoft.Win32;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace _08_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string FilePath = "";
        WebClient client;
        string currentFile = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Select_Folder(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dialog = new OpenFolderDialog();

            if(dialog.ShowDialog() != true)
            {
                MessageBox.Show("File not found");
                return;
            }
            FilePath = dialog.FolderName;

            MessageBox.Show($"Folder --> {FilePath}");
        }

        private async void Button_Loading(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(FilePath))
            {
                MessageBox.Show("choose folder!!!");
                return;
            }

            if(string.IsNullOrWhiteSpace(MyTextBox.Text))
            {
                MessageBox.Show("write URL:!!!");
                return;
            }
            try
            {

                client = new WebClient();

                client.DownloadFileCompleted += Client_DownloadComplated;
                client.DownloadProgressChanged += Client_DownloadProgresschanged;

                Uri uri = new Uri(MyTextBox.Text);

                string fileName = System.IO.Path.Combine(FilePath, System.IO.Path.GetFileName(uri.LocalPath));

                currentFile = fileName;

                MuListBox.Items.Add($"{System.IO.Path.GetFileName(fileName)} : Downloading 0%");

                await client.DownloadFileTaskAsync(MyTextBox.Text, fileName);

                MessageBox.Show("url loaded");

                MyTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void Client_DownloadComplated(object? sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("File downloads was cancelled");
            else
                MessageBox.Show("File downloaded succesfully!");
        }
        private void Client_DownloadProgresschanged(object sender, DownloadProgressChangedEventArgs e)
        {
            /*MessageBox.Show($"Downloading ..... \t {(float)e.BytesReceived / 1024 / 1024}Mb of {(float)e.TotalBytesToReceive / 1024 / 1024}Mb \t {e.ProgressPercentage}%");*/

            string info = $"{(float)e.BytesReceived / 1024 / 1024} MB / " + $"{(float)e.TotalBytesToReceive / 1024 / 1024} MB " + $"{e.ProgressPercentage}%";

            int index = MuListBox.Items.Count - 1;
           
            if (index >= 0)
            {
                MuListBox.Items[index] = $"{System.IO.Path.GetFileName(currentFile)} : Downloading {info}";
            }
        }
        private void Button_stop(object sender, RoutedEventArgs e)
        {
            if (client != null)
            {
                client.CancelAsync();
            }
        }

        private void MuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MuListBox.SelectedItem == null)
                return;

            string item = MuListBox.SelectedItem.ToString();

            string fileName = item.Split(':')[0];

            string filePath = System.IO.Path.Combine(FilePath, fileName);

            if (File.Exists(filePath))
            {
                Process.Start(filePath);
            }
        }
    }
}
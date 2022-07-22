using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using HighlightToolGUI;
using Microsoft.Win32;

namespace HighlighToolGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String filename;
        public MainWindow()
        {
            InitializeComponent();
            infoBlock.Text = "Welcome to the Faust Highlight Tool";
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.filename = openFileDialog.FileName;
                textBox.Text = filename;
            }
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            infoBlock.Text = "working...";
            FFMPEG2 ff = new FFMPEG2(filename, videoNameBox.Text);
            ff.CutVideo();
            ff.ConcatenateVideos();
            ff.DeleteTempFiles();
            infoBlock.Text = "done";
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace dz4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileName = "";
        public MainWindow()
        {
            InitializeComponent();
            textBox.ContextMenu = contextMenu;

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
                if(!string.IsNullOrEmpty(fileName))
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Message", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveFile(fileName);
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            textBox.Clear();
            fileName = "";
        }
       
        private void Open_Click(object sender, RoutedEventArgs e)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        fileName = openFileDialog.FileName;

                        using (StreamReader streamReader = new StreamReader(fileName))
                        {
                            textBox.Text = streamReader.ReadToEnd();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }

        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                SaveFile(fileName);
            }
            else SaveFileAs();

        }

        public void SaveFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
                writer.Write(textBox.Text);

        }
        public void SaveFileAs()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text Files(*.txt)|*.txt";
            saveFile.DefaultExt = "txt";
            if (saveFile.ShowDialog() == true)
            {
               fileName = saveFile.FileName;
                SaveFile(saveFile.FileName);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAs();
        }


        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Cut_Click(object sender, RoutedEventArgs e)
        {
            textBox.Cut();
        }
        public void Copy_Click(object sender, RoutedEventArgs e)
        {
            textBox.Copy();
        }
        public void Paste_Click(object sender, RoutedEventArgs e)
        {
            textBox.Paste();
        }

        
    }
}

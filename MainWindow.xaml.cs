using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using FrequencyAnalyzer.Model;
using FrequencyAnalyzer.Exceptions;
using FrequencyAnalyzer.View;
using System.Reflection;
using System.Resources;

namespace FrequencyAnalyzer
{
    public partial class MainWindow : Window
    {
        private VocabularyInfo file;

        public event EventHandler NewFileEvent;
        public event EventHandler<FileOperationEventArgs> OpenFileEvent;
        public event EventHandler<FileOperationEventArgs> SaveFileAsEvent;
        public event EventHandler<FileOperationEventArgs> AddNewTextEvent;

        public MainWindow()
        {
            InitializeComponent();

            SubscrideEvents();
        }

        public void Show(VocabularyInfo file)
        {
            this.file = file;

            DataGrid.ItemsSource = new ObservableCollection<WordInfo>(file.Vocabulary.Words);
            TotalAmountLabel.Content = file.Vocabulary.WordCount;
            FilenameLabel.Content = file.Filename.Split('\\').Last();
        }

        private void SubscrideEvents()
        {
            buttonNewFile.Click += ButtonNewFile_Click;
            buttonOpenFile.Click += ButtonOpenFile_Click;
            buttonSaveFile.Click += ButtonSaveFile_Click;
            buttonSaveFileAs.Click += ButtonSaveFileAs_Click;
            buttonAddText.Click += ButtonAddText_Click;
        }

        private void ButtonNewFile_Click(object sender, RoutedEventArgs e)
        {
            NewFileEvent?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            string filename = ShowDialog(DialogBox.OpenFile, ".bin");
            if (filename != null)
                OpenFileEvent?.Invoke(this, new FileOperationEventArgs
                {
                    OldFilename = file.Filename,
                    SelectedFilename = filename
                });
        }

        private void ButtonSaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            string filename = ShowDialog(DialogBox.SaveFile, ".bin");
            if (filename != null)
                SaveFileAsEvent?.Invoke(this, new FileOperationEventArgs
                {
                    OldFilename = file.Filename,
                    SelectedFilename = filename
                });
        }

        private void ButtonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAsEvent?.Invoke(this, new FileOperationEventArgs
            {
                OldFilename = file.Filename,
                SelectedFilename = file.Filename
            });
        }

        private void ButtonAddText_Click(object sender, RoutedEventArgs e)
        {
            string filename = ShowDialog(DialogBox.OpenFile, ".txt");
            if (filename != null)
                AddNewTextEvent?.Invoke(this, new FileOperationEventArgs
                {
                    OldFilename = file.Filename,
                    SelectedFilename = filename
                });
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void About_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show(Properties.Resources.ResourceManager.GetString("Description"), "About");
        }

        private string ShowDialog(DialogBox dialogKind, string fileExtFilter)
        {
            Microsoft.Win32.FileDialog dlg;

            if (dialogKind == DialogBox.OpenFile)
                dlg = new Microsoft.Win32.OpenFileDialog();
            else if (dialogKind == DialogBox.SaveFile)
                dlg = new Microsoft.Win32.SaveFileDialog();
            else
                throw new UnknownDialogException("Unknow dialog");

            dlg.DefaultExt = fileExtFilter;

            bool? result = dlg.ShowDialog();

            return result == true ? dlg.FileName : null;
        }
    }
}

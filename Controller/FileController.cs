using FrequencyAnalyzer.Model;
using FrequencyAnalyzer.Serializer;
using FrequencyAnalyzer.Exceptions;
using FrequencyAnalyzer.View;
using System.IO;

namespace FrequencyAnalyzer.Controller
{
    class FileController
    {
        private Vocabulary vocabulary;
        private MainWindow mainWindow;

        public FileController(Vocabulary vocabulary, MainWindow mainWindow)
        {
            this.vocabulary = vocabulary;
            this.mainWindow = mainWindow;

            SubscridbeEvents();

            mainWindow.Show(new VocabularyInfo { Vocabulary = vocabulary, Filename = "NewFile.bin" });
        }

        private void SubscridbeEvents()
        {
            mainWindow.NewFileEvent += MainWindow_NewFileEvent;
            mainWindow.OpenFileEvent += MainWindow_OpenFileEvent;
            mainWindow.SaveFileAsEvent += MainWindow_SaveFileAsEvent;
            mainWindow.AddNewTextEvent += MainWindow_AddNewTextEvent;
        }

        private void MainWindow_NewFileEvent(object sender, System.EventArgs e)
        {
            vocabulary.ClearWords();

            mainWindow.Show(new VocabularyInfo { Vocabulary = vocabulary, Filename = "NewFile.bin" });
        }

        private void MainWindow_OpenFileEvent(object sender, FileOperationEventArgs e)
        {
            string path = e.SelectedFilename;
            vocabulary.ClearWords();

            try
            {
                Vocabulary restoredVocabulary = BinarySerializer.ReadFromBinaryFile<Vocabulary>(path);
                vocabulary = restoredVocabulary;

                mainWindow.Show(new VocabularyInfo { Vocabulary = vocabulary, Filename = path });
            }
            catch (System.IO.IOException ex)
            {
                throw new NotVocabularyException("File access denied", ex);
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                throw new NotVocabularyException("File is not a vocabulary", ex);
            }
        }

        private void MainWindow_SaveFileAsEvent(object sender, FileOperationEventArgs e)
        {
            string path = e.SelectedFilename;
            BinarySerializer.WriteToBinaryFile(path, vocabulary);
        }

        private void MainWindow_AddNewTextEvent(object sender, FileOperationEventArgs e)
        {
            string path = e.SelectedFilename;
            vocabulary.LoadFromFile(path);

            mainWindow.Show(new VocabularyInfo { Vocabulary = vocabulary, Filename = e.OldFilename });
        }
    }
}

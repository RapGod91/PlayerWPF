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
using Microsoft.Win32;

namespace PlayerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MediaPlayerModel MediaPlayerModel { get; set; }
        private TimeSpan currentPosition;

        public MainWindow()
        {
            InitializeComponent();

            MediaPlayerModel = new MediaPlayerModel();
            DataContext = MediaPlayerModel;
        }

        private void SelectMediaButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos de Mídia|*.mp3;*.mp4;*.avi";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                MediaPlayerModel.MediaPath = selectedFilePath;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (MediaPlayerModel.IsPlaying)
            {
                // Retomar a reprodução a partir da posição congelada
                mediaElement.Position = currentPosition;
                mediaElement.Play();
            }
            else
            {
                // Iniciar a reprodução do início
                mediaElement.Play();
            }

            MediaPlayerModel.IsPlaying = true;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            currentPosition = mediaElement.Position;
            MediaPlayerModel.IsPlaying = false;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            MediaPlayerModel.MediaPath = null;
            MediaPlayerModel.IsPlaying = false;
        }
    }
}
//Quando pausa ou Stopa o vídeo está fechando o programa.
//Tem que tirar o IsPlaying = false;
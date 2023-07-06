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

            MessageBox.Show("Bem-vindo ao Media Player!\n\nPara usar o player, siga as seguintes instruções:\n\n1. Clique no botão 'Selecionar Mídia' para escolher um arquivo de mídia.\n2. Clique no botão 'Play' para reproduzir a mídia selecionada.\n3. Clique no botão 'Pause' para pausar a reprodução da mídia.\n4. Clique no botão 'Parar' para interromper a reprodução da mídia.\n\nAproveite a experiência!",
                        "Instruções",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

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
            try
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
                    if (string.IsNullOrEmpty(MediaPlayerModel.MediaPath))
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "Arquivos de Mídia|*.mp3;*.mp4;*.avi";

                        if (openFileDialog.ShowDialog() == true)
                        {
                            string selectedFilePath = openFileDialog.FileName;
                            MediaPlayerModel.MediaPath = selectedFilePath;
                        }
                        else
                        {
                            return; // Se o usuário cancelar a seleção de mídia, retornar sem iniciar a reprodução
                        }
                    }

                    mediaElement.Play();
                }

                MediaPlayerModel.IsPlaying = true;
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se necessário
                //MessageBox.Show($"Erro ao reproduzir o vídeo: {ex.Message}");
            }
        }


        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mediaElement.Source != null && mediaElement.CanPause)
                {
                    mediaElement.Pause();
                    currentPosition = mediaElement.Position;
                    MediaPlayerModel.IsPlaying = false;
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se necessário
                MessageBox.Show($"Erro ao pausar o vídeo: {ex.Message}");
            }
        }


        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mediaElement.Stop();
                MediaPlayerModel.MediaPath = null;
                MediaPlayerModel.IsPlaying = false;
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se necessário
                MessageBox.Show($"Erro ao parar o vídeo: {ex.Message}");
            }
        }

    }
}

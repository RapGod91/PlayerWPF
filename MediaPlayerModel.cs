using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace PlayerWPF
{
    public class MediaPlayerModel : INotifyPropertyChanged
    {
        private string mediaPath;  // Caminho da mídia
        private bool isPlaying;  // Indica se a mídia está em reprodução
        private TimeSpan currentPosition;  // Posição atual da mídia
        private MediaElement mediaElement;  // Elemento MediaElement para reprodução da mídia

        public string MediaPath
        {
            get { return mediaPath; }
            set
            {
                mediaPath = value;
                OnPropertyChanged(nameof(MediaPath));
                UpdateMedia();
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
                if (isPlaying)
                    Play();
                else
                    Pause();
            }
        }

        public TimeSpan CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }

        public MediaPlayerModel()
        {
            mediaElement = new MediaElement();
            mediaElement.MediaOpened += MediaElement_MediaOpened;
            mediaElement.MediaEnded += MediaElement_MediaEnded;
        }

        public void Play()
        {
            if (mediaElement.Source != null && !mediaElement.IsLoaded)
                mediaElement.Play();
        }

        public void Pause()
        {
            if (mediaElement.Source != null && mediaElement.IsLoaded)
                mediaElement.Pause();
        }

        public void Stop()
        {
            if (mediaElement.Source != null && mediaElement.IsLoaded)
            {
                mediaElement.Stop();
                MediaPath = null;
            }
        }

        private void MediaElement_MediaOpened(object sender, EventArgs e)
        {
            CurrentPosition = mediaElement.NaturalDuration.TimeSpan;
        }

        private void MediaElement_MediaEnded(object sender, EventArgs e)
        {
            IsPlaying = false;
            mediaElement.Stop();
        }

        private void UpdateMedia()
        {
            if (!string.IsNullOrEmpty(MediaPath))
                mediaElement.Source = new Uri(MediaPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

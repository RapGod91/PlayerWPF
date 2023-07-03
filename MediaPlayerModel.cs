using System;
using System.ComponentModel;

namespace PlayerWPF;

public class MediaPlayerModel : INotifyPropertyChanged
{
    private string mediaPath;
    private bool isPlaying;
    private TimeSpan currentPosition;
    // outras propriedades

    public string MediaPath
    {
        get { return mediaPath; }
        set
        {
            mediaPath = value;
            OnPropertyChanged(nameof(MediaPath));
        }
    }

    public bool IsPlaying
    {
        get { return isPlaying; }
        set
        {
            isPlaying = value;
            OnPropertyChanged(nameof(IsPlaying));
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

    // outras propriedades e métodos necessários

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

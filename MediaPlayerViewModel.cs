using System;
using System.ComponentModel;

namespace PlayerWPF;

public class MediaPlayerViewModel : INotifyPropertyChanged
{
    private MediaPlayerModel model;  // Instância do modelo MediaPlayerModel
    private RelayCommand playCommand;  // Comando para reproduzir a mídia
    private RelayCommand pauseCommand;  // Comando para pausar a reprodução da mídia
    private RelayCommand stopCommand;  // Comando para parar a reprodução da mídia
    // outros comandos

    public MediaPlayerModel Model
    {
        get { return model; }
        set
        {
            model = value;
            OnPropertyChanged(nameof(Model));
        }
    }

    public RelayCommand PlayCommand
    {
        get
        {
            return playCommand ?? (playCommand = new RelayCommand(() =>
            {
                // Lógica para reproduzir a mídia
                Model.IsPlaying = true;
            }));
        }
    }

    public RelayCommand PauseCommand
    {
        get
        {
            return pauseCommand ?? (pauseCommand = new RelayCommand(() =>
            {
                // Lógica para pausar a reprodução da mídia
                Model.IsPlaying = false;
            }));
        }
    }

    public RelayCommand StopCommand
    {
        get
        {
            return stopCommand ?? (stopCommand = new RelayCommand(() =>
            {
                // Lógica para parar a reprodução da mídia
                Model.IsPlaying = false;
                Model.CurrentPosition = TimeSpan.Zero;
            }));
        }
    }

    // outros comandos e métodos necessários

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

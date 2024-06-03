using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _6002CEM_Maui_App.ViewModels;

// Base view model, other vms to inherit OnPropertyChanged
public class PageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // For ui element updates 
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public PageViewModel()
    {
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace InfiniteAxisUtility.Editor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ObservableCollection<string> PresetCurves => new()
    {
        "Linear",
        "Inverse Linear"
    };

    public int SelectedPresetCurve { get; set; }

    public void ExitCommand()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
}

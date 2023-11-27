using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace InfiniteAxisUtility.Editor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        ResponseCurveEditor = new ResponseCurveEditorViewModel();

        // Initialize empty project
        Project ??= new Project();
    }

    public Project Project { get; }

    public ResponseCurveEditorViewModel ResponseCurveEditor { get; }

    public void ExitCommand()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
}

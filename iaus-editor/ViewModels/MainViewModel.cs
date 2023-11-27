using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace InfiniteAxisUtility.Editor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        ResponseCurveEditor = new ResponseCurveEditorViewModel();
        BehaviorEditor = new BehaviorEditorViewModel();
        BehaviorSetEditor = new BehaviorSetEditorViewModel();
    }

    public ResponseCurveEditorViewModel ResponseCurveEditor { get; }
    public BehaviorEditorViewModel BehaviorEditor { get; }
    public BehaviorSetEditorViewModel BehaviorSetEditor { get; }

    public void ExitCommand()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace InfiniteAxisUtility.Editor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private ObservableCollection<string> _curves;

    public ObservableCollection<string> PresetCurves
    {
        get => _curves;
        set => SetProperty(ref _curves, value);
    }

    public ResponseCurve.CurveType SelectedPresetCurve { get; set; }

    public MainViewModel()
    {
        _curves = new ObservableCollection<string>();

        var vals = Enum.GetNames(typeof(ResponseCurve.CurveType));

        // Skip unknown value
        for (var i = 1; i < vals.Length; i++)
        {
            PresetCurves.Add(vals[i]);
        }

        SelectedPresetCurve = 0;
    }

    public ISeries[] Series { get; set; } = {
        new LineSeries<double>
        {
            Values = new double[] { 5, 0, 5, 0, 5, 0 },
            Fill = null,
            GeometrySize = 0,
            // use the line smoothness property to control the curve
            // it goes from 0 to 1
            // where 0 is a straight line and 1 the most curved
            LineSmoothness = 0
        },
        new LineSeries<double>
        {
            Values = new double[] { 7, 2, 7, 2, 7, 2 },
            Fill = null,
            GeometrySize = 0,
            LineSmoothness = 1
        }
    };

    public void ExitCommand()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }
}

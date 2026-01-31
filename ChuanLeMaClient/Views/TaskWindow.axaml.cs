using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ChuanLeMaClient.ViewModels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ChuanLeMaClient;

public partial class TaskWindow : ActiveTopLevelWindow //AtomUI.Desktop.Controls.Window
{
    public TaskWindow()
    {
        if (Design.IsDesignMode)
        {
            InitializeComponent();
            Loaded += TaskWindow_Loaded;
        } 
    }

    public TaskWindow(ILogger<ActiveTopLevelWindow> logger) : base(logger)
    {
        InitializeComponent();
        Loaded += TaskWindow_Loaded;
    }

    private async void TaskWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is TaskWindowViewModel vm)
        {
            await vm.LoadedAsync();
        }
    }
}
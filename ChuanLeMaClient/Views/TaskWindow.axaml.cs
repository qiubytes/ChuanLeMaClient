using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ChuanLeMaClient.ViewModels;
using System.Threading.Tasks;

namespace ChuanLeMaClient;

public partial class TaskWindow : Window
{
    public TaskWindow()
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
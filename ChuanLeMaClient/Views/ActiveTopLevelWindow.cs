using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;

namespace ChuanLeMaClient;

/// <summary>
/// 窗口激活事件里面自动设置主窗口
/// AtomUI的静态对话框 会根据主窗口来显示
/// </summary>
public class ActiveTopLevelWindow : AtomUI.Desktop.Controls.Window
{
    private ILogger<ActiveTopLevelWindow> _logger;

    public ActiveTopLevelWindow()
    {

    }

    public ActiveTopLevelWindow(ILogger<ActiveTopLevelWindow> logger)
    {
        _logger = logger;
        Activated += OnActivated;
    }

    private void OnActivated(object sender, EventArgs e)
    {
        _logger.LogInformation("窗口激活: {0}", this.GetType().Name);
        //窗口激活时 设置为主窗口
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // 如果这个窗口不是当前主窗口，设置为临时主窗口
            if (desktop.MainWindow != this)
            {
                desktop.MainWindow = this;
                _logger.LogInformation("主窗口设置: {0}", this.GetType().Name);
            }
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        // 清理事件
        this.Activated -= OnActivated;
        _logger.LogInformation("窗口关闭: {0}", this.GetType().Name);
        // 恢复原来的主窗口
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            IReadOnlyList<Window> windows = desktop.Windows;
            Window w = windows.Where(o => o != this).FirstOrDefault();
            if (w != null)
            {
                desktop.MainWindow = w;
                // desktop.MainWindow.Activate();  
                _logger.LogInformation("主窗口设置: {0}", w.GetType().Name);

            }
        }

        base.OnClosed(e);
    }
}
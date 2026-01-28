using AtomUI.Desktop.Controls;
using AtomUI.Theme;
using AtomUI.Theme.Language;
using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ChuanLeMaClient.Common;
using ChuanLeMaClient.Repository;
using ChuanLeMaClient.Services.Implement;
using ChuanLeMaClient.ViewModels;
using ChuanLeMaClient.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChuanLeMaClient
{
    public partial class App : Application
    {
        /// <summary>
        /// Autofac 容器
        /// </summary>
        private IContainer _container;

        public override void Initialize()
        {
            // 配置 Serilog
            ConfigureSerilog();
            AvaloniaXamlLoader.Load(this);
            //第一步  配置AtomUI  
            this.UseAtomUI(builder =>
            {
                builder.WithDefaultLanguageVariant(LanguageVariant.zh_CN);
                builder.WithDefaultTheme(IThemeManager.DEFAULT_THEME_ID);
                builder.UseAlibabaSansFont(); // 配置字体
                builder.UseDesktopControls();
                builder.UseDesktopDataGrid();
                builder.UseDesktopColorPicker();
            });
            // 构建 Autofac 容器
            var builder = new ContainerBuilder();
            ConfigureContainer(builder);
            _container = builder.Build();
        }
        private void ConfigureSerilog()
        {
            var logPath = Path.Combine(
                AppContext.BaseDirectory,
                "logs",
                "log-.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                //.Enrich.WithThreadId()
                //.Enrich.WithMachineName()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    logPath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                //.WriteTo.Debug()
                .CreateLogger();

            Log.Information("Serilog 配置完成");
        }
        /// <summary>
        /// 配置 Autofac 容器
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureContainer(ContainerBuilder builder)
        {
            #region 依赖注入注册日志组件
            // 1. 注册 Serilog 的 ILogger（如果需要）  把SeriLog的单例对象注册进去Serilog.ILogger
            builder.Register(c => Log.Logger).As<Serilog.ILogger>().SingleInstance();

            // 2. 注册 Microsoft.Extensions.Logging 的 ILoggerFactory 用于调用 微软ILogger接口使用 Serilog
            builder.Register(c =>
            {
                var factory = new SerilogLoggerFactory(Log.Logger);
                return factory;
            }).As<ILoggerFactory>().SingleInstance();

            // 3. 注册泛型 ILogger<T>（推荐）
            builder.RegisterGeneric(typeof(Logger<>))
                   .As(typeof(ILogger<>))
                   .SingleInstance();
            // 4. 注册非泛型 ILogger（用于需要非泛型 ILogger 的构造函数）
            builder.Register(c =>
            {
                var factory = c.Resolve<ILoggerFactory>();
                return factory.CreateLogger("Application");
            }).As<Microsoft.Extensions.Logging.ILogger>().SingleInstance();

            #endregion

            var assembly = Assembly.GetExecutingAssembly();
            //Console.WriteLine("=== 开始扫描程序集 ===");
            //Console.WriteLine($"程序集: {assembly.FullName}");

            //// 获取所有类型
            //var allTypes = assembly.GetTypes();
            //Console.WriteLine($"总类型数: {allTypes.Length}");

            //// 检查是否有 TestServiceImpl
            //var testServiceType = allTypes.FirstOrDefault(t => t.Name == "TestServiceImpl");
            //Console.WriteLine($"找到 TestServiceImpl: {testServiceType != null}");
            //if (testServiceType != null)
            //{
            //    Console.WriteLine($"命名空间: {testServiceType.Namespace}");
            //    Console.WriteLine($"完整名称: {testServiceType.FullName}");
            //}

            //// 检查所有 ServiceImpl
            //var serviceImplTypes = allTypes.Where(t => t.Name.EndsWith("ServiceImpl")).ToList();
            //Console.WriteLine($"找到 ServiceImpl 数量: {serviceImplTypes.Count}");
            //foreach (var type in serviceImplTypes)
            //{
            //    Console.WriteLine($"  - {type.FullName}");
            //}

            // 1. 注册所有 ServiceImpl 和 RepositoryImpl 类型
            builder.RegisterAssemblyTypes(assembly)
             .Where(t => t.Name.EndsWith("RepositoryImpl"))
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope(); //在同一个生命周期作用域内是单例 
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("ServiceImpl"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope(); //在同一个生命周期作用域内是单例 
            //2、注册单例服务
            builder.RegisterType<UploadServiceImplSingleInstance>()
                   .As<Services.Inteface.IUploadService>()
                   .SingleInstance(); // 整个应用程序生命周期内是单例
            builder.RegisterType<DownloadServiceImplSingleInstance>()
                .As<Services.Inteface.IDownloadService>()
                .SingleInstance();
            builder.RegisterType<ApplicationGlobalVarServiceImplSingleInstance>()//应用程序全局变量服务单例
                .As<Services.Inteface.IApplicationGlobalVarService>()
                .SingleInstance();
            builder.RegisterType<SQLiteHelper>().AsSelf().SingleInstance();//注册数据库操作类 单例
            #region 注册IConfiguartion
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
               ?? "Production";
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            //.AddEnvironmentVariables()
            .Build();
            builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();
            #endregion

            // 注册窗口
            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();

            // 注册 ViewModel
            builder.RegisterType<TaskWindowViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<MainWindowViewModel>().AsSelf().InstancePerDependency();

            // 注册Http工具类
            builder.RegisterType<HttpClientUtil>().AsSelf().InstancePerLifetimeScope();
            // 注册其他服务
            //builder.RegisterType<ApiService>().As<IApiService>().SingleInstance();
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                //注释之前的 手工实例化
                //desktop.MainWindow = new MainWindow
                //{
                //    DataContext = new MainWindowViewModel(),
                //}; 
                // 从 Autofac 容器中解析 MainWindow 和 MainWindowViewModel
                desktop.MainWindow = _container.Resolve<MainWindow>();
                desktop.MainWindow.DataContext = _container.Resolve<MainWindowViewModel>();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}
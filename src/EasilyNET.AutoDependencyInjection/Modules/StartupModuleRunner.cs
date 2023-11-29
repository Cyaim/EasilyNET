﻿using EasilyNET.AutoDependencyInjection.Abstractions;
using EasilyNET.AutoDependencyInjection.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EasilyNET.AutoDependencyInjection.Modules;

/// <summary>
/// 启动模块运行器
/// </summary>
internal class StartupModuleRunner : ModuleApplicationBase, IStartupModuleRunner
{
    /// <summary>
    /// 程序启动运行时
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="services"></param>
    internal StartupModuleRunner(Type startupModuleType, IServiceCollection services) : base(startupModuleType, services)
    {
        _ = services.AddSingleton<IStartupModuleRunner>(this);
    }

    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        var context = new ConfigureServicesContext(Services);
        _ = Services.AddSingleton(context);
        foreach (var config in Modules)
        {
            _ = Services.AddSingleton(config);
            config.ConfigureServices(context);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="service"></param>
    public void Initialize(IServiceProvider service)
    {
        SetServiceProvider(service);
        InitializeModules();
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public new void Dispose()
    {
        base.Dispose();
        if (ServiceProvider is IDisposable disposableServiceProvider) disposableServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }
}
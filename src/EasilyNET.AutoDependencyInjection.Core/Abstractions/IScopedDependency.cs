using EasilyNET.AutoDependencyInjection.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable UnusedMember.Global

namespace EasilyNET.AutoDependencyInjection.Core.Abstractions;

/// <summary>
/// 实现此接口的类型将自动注册为<see cref="ServiceLifetime.Scoped" />模式
/// </summary>
[IgnoreDependency]
public interface IScopedDependency;
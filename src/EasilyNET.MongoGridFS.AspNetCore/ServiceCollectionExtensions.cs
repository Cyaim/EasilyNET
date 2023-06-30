using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global

namespace EasilyNET.MongoGridFS.AspNetCore;

/// <summary>
/// ������չ
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="db"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddMongoGridFS(this IServiceCollection services, IMongoDatabase db, Action<GridFSBucketOptions>? configure = null)
    {
        services.AddMongoGridFS(db, Constant.ConfigName, configure);
        return services;
    }

    /// <summary>
    /// ͨ�� <see cref="IMongoDatabase" /> ע�� <see cref="IGridFSBucket" />
    /// </summary>
    /// <param name="services"></param>
    /// <param name="db"></param>
    /// <param name="name">ConfigureName</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddMongoGridFS(this IServiceCollection services, IMongoDatabase db, string name, Action<GridFSBucketOptions>? configure = null)
    {
        services.Configure(name, configure);
        services.TryAddSingleton<IGridFSBucketFactory, GridFSBucketFactory>();
        services.TryAddSingleton(sp => sp.GetRequiredService<IGridFSBucketFactory>().CreateBucket(db));
        return services;
    }
}
﻿using EasilyNET.AutoDependencyInjection.Contexts;
using EasilyNET.AutoDependencyInjection.Modules;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace WebApi.Test.Unit;

/// <summary>
/// 响应压缩
/// </summary>
internal sealed class ResponseCompressionModule : AppModule
{
    /// <inheritdoc />
    public override void ConfigureServices(ConfigureServicesContext context)
    {
        context.Services.AddResponseCompression(c =>
        {
            //c.EnableForHttps = true;
            c.Providers.Add<BrotliCompressionProvider>();
            c.Providers.Add<GzipCompressionProvider>();
            c.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat([
                "image/svg+xml",
                "text/plain",
                "text/css",
                "application/json",
                "application/x-javascript",
                "text/xml",
                "application/xml",
                "application/xml+rss",
                "text/javascript"
            ]);
        });
        context.Services.Configure<BrotliCompressionProviderOptions>(c => c.Level = CompressionLevel.SmallestSize);
        context.Services.Configure<GzipCompressionProviderOptions>(c => c.Level = CompressionLevel.SmallestSize);
    }

    /// <inheritdoc />
    public override void ApplicationInitialization(ApplicationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseResponseCompression();
        app.Use(async (c, next) =>
        {
            c.Response.Headers.Add(new("Vary", "Accept-Encoding"));
            await next();
        });
    }
}
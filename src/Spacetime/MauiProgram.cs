﻿using Spacetime.Core;
using Spacetime.Core.gRPC;
using Spacetime.Core.Services;
using Spacetime.Core.Infrastructure;
using Spacetime.Blazor.Sortable;
using Spacetime.Helpers;
using MudBlazor.Services;
using Serilog;

namespace Spacetime;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
          .Enrich.FromLogContext()
          .WriteTo.Debug()
          .CreateLogger();

        builder.Services.AddLogging(options =>
        {
            options.AddSerilog(dispose: true);
        });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Services.AddBlazorWebView();

        builder.Services.AddSingleton<RequestService>();
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<SpacetimeRestService>();
        builder.Services.AddSingleton<UrlBuilder>();
        builder.Services.AddSingleton<IGrpcExplorer, GrpcExplorer>();
        builder.Services.AddSingleton<IProtobufService, LiteDbProtobufService>();
        builder.Services.AddHttpClient<ISpacetimeService, SpacetimeRestService>();

        builder.Services.AddScoped<ScriptUtils>();
        builder.Services.AddBlazorSortable();
        builder.Services.AddMudServices();
        return builder.Build();
    }
}

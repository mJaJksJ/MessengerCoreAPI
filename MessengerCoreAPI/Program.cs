using MessengerCoreAPI;
using MessengerCoreAPI.Models.RGDialogsClients;
using MessengerCoreAPI.Models.RGDialogsClients.RGDialogsClientsFactory;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var log = Log.ForContext<Program>();

#region serilog configuration

var logTemplateConsole = "[{Level:u3}] <{ThreadId}> :: {Message:lj}{NewLine}{Exception}";
var logTemplateFile = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] <{ThreadId}> :: {Message:lj}{NewLine}{Exception}";

if (!Directory.Exists(DirectoryPaths.LogsDirectory))
{
    try
    {
        Directory.CreateDirectory(DirectoryPaths.LogsDirectory);
        log.Information($"Create directory {DirectoryPaths.LogsDirectory} for logs");
    }
    catch
    {
        log.Error($"Can't find or create directory {DirectoryPaths.LogsDirectory} for logs");
        return;
    }
}

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .WriteTo.Console(outputTemplate: logTemplateConsole)
    .WriteTo.File(
        outputTemplate: logTemplateFile,
        path: Path.Combine(DirectoryPaths.LogsDirectory, "MessengerCoreApi.log"),
        shared: true,
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 128 * 1024 * 1024
    )
);

#endregion serilog configuration

builder.Services.AddControllers();

#region swagger settings

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MessengerCoreAPI",
        Version = "v1"
    });

    var xmlName = $"{Path.GetFileNameWithoutExtension(DirectoryPaths.WorkingDirectory)}.xml";
    var xmlPath = Path.Combine(Path.GetDirectoryName(DirectoryPaths.WorkingDirectory), xmlName);

    c.IncludeXmlComments(xmlPath);
});

#endregion swagger settings

#region services

IRGDialogsClientsFactory dialogsClientsFactory = new DefaultRGDialogsClientsFactory();
var dialogs = new RGDialogsClientsCollection { RGDialogsClients = dialogsClientsFactory.CreateRGDialogsClients() };
builder.Services.AddSingleton(dialogs);

#endregion services

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessengerCoreAPI"); });

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();

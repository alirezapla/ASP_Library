using BookLibraryAPIDemo.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddlewares();

app.Run();
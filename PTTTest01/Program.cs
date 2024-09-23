using PTTTest01;
using PTTTest01.DataAccess;
using PTTTest01.Helpers;
using PTTTest01.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddHttpClient();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

var dbConnectionString = builder.Configuration.GetConnectionString("AvatarDatabaseConnectionString");

builder.Services.AddTransient<IAvatarRepository, AvatarRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<AvatarRepository>>();
    return new AvatarRepository(dbConnectionString, logger);
});

builder.Services.AddTransient<IAvatarJsonHelper, AvatarJsonHelper>();

builder.Services.AddTransient<IAvatarService>(provider =>
{    
    var repository = provider.GetRequiredService<IAvatarRepository>();
    var jsonHelper = provider.GetRequiredService<IAvatarJsonHelper>();
    var logger = provider.GetRequiredService<ILogger<AvatarService>>();
    return new AvatarService(repository, jsonHelper, logger);
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

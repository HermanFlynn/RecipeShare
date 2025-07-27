using RecipeShare.Utils.Config;

var builder = WebApplication.CreateBuilder(args);



var databaseConfig = builder.Configuration.GetSection("DatabaseConfig").Get<DatabaseConfig>();
var seedConfig = builder.Configuration.GetSection("SeedConfig").Get<SeedConfig>();






var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

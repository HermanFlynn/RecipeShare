using Microsoft.EntityFrameworkCore;
using RecipeShare.Util.Data;
using RecipeShare.Util.Repository;
using RecipeShare.Util.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
//builder.Services.AddControllers();

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	});

builder.Services.AddDbContext<RecipeShareDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRecipeService, RecipeService>();

var AllowSpecificOrigins = "allowedSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: AllowSpecificOrigins,
					  policy =>
					  {
						  policy.AllowAnyOrigin();
						  policy.AllowAnyHeader();
						  policy.AllowAnyMethod();
					  });
});

var app = builder.Build();
app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
using BenchmarkDotNet.Attributes;
using System.Net.Http;
using System.Threading.Tasks;

public class RecipeApiBenchmark
{
	private readonly HttpClient _httpClient;

	public RecipeApiBenchmark()
	{
		_httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5154") }; // Update with your API URL
	}

	[Benchmark]
	public async Task GetRecipes()
	{
		for (int i = 0; i < 500; i++)
		{
			var response = await _httpClient.PostAsync("recipe/readAll", null);
			response.EnsureSuccessStatusCode();
		}
	}
}
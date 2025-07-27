using System.Diagnostics.CodeAnalysis;

namespace RecipeShare.Util.Config
{
	public class Config
	{
		public DatabaseConfig DatabaseConfig { get; set; }

		[ExcludeFromCodeCoverage]
		public SeedConfig SeedConfig { get; set; }
	}
}
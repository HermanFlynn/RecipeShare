using System.Diagnostics.CodeAnalysis;

namespace RecipeShare.Utils.Config
{
	public class Config
	{
		public DatabaseConfig DatabaseConfig { get; set; }

		[ExcludeFromCodeCoverage]
		public SeedConfig SeedConfig { get; set; }
	}
}
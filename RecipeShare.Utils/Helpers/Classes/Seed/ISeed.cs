using RecipeShare.Utils.Config;

namespace RecipeShare.Utils.Helpers.Classes.Seed
{
	public interface ISeed
	{
		void SeedAll(DatabaseConfig databaseConfig, SeedConfig seedConfig);
	}
}
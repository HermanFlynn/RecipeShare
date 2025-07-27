using RecipeShare.Util.Config;

namespace RecipeShare.Util.Helpers.Classes.Seed
{
	public interface ISeed
	{
		void SeedAll(DatabaseConfig databaseConfig, SeedConfig seedConfig);
	}
}
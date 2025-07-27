using RecipeShare.Utils.Models.Database;

namespace RecipeShare.Utils.Config
{
	public class SeedConfig
	{
		public List<Role> Roles { get; set; }
		public List<User> Users { get; set; }
		public List<Recipe> Recipes { get; set; }
	}
}
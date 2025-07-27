using RecipeShare.Util.Models.Database;

namespace RecipeShare.Util.Config
{
	public class SeedConfig
	{
		public List<Role> Roles { get; set; }
		public List<User> Users { get; set; }
		public List<Recipe> Recipes { get; set; }
	}
}
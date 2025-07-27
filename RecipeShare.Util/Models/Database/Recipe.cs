using RecipeShare.Util.Helpers.Enums;

namespace RecipeShare.Util.Models.Database
{
	public class Recipe : BaseModel
	{
		public string Title { get; set; }
		public List<string> Ingredients { get; set; }
		public int CookingTimeMinutes { get; set; }
		public List<DietaryTagEnums> DietaryTags { get; set; } = new List<DietaryTagEnums> { DietaryTagEnums.None };
	}
}
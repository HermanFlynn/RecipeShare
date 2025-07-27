using RecipeShare.Utils.Helpers.Enums;

namespace RecipeShare.Utils.Models.Database
{
	public class Recipe : BaseModel
	{
		public Guid ID { get; set; }
		public string Title { get; set; }
		public List<string> Ingredients { get; set; }
		public int CookingTimeMinutes { get; set; }
		public DietaryTagEnums DietaryTags { get; set; }
	}
}
namespace RecipeShare.Util.Models
{
	public class BaseModel : IModel
	{
		public string? Id { get; set; }

		public bool? IsDeleted { get; set; } = false;

		public DateTime? CreatedDateTime { get; set; }

		public DateTime? ModifiedDateTime { get; set; }
	}
}
namespace RecipeShare.Utils.Models
{
	public class BaseModel : IModel
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public bool IsDeleted { get; set; } = false;

		public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

		public DateTime ModifiedDateTime { get; set; } = DateTime.UtcNow;
	}
}
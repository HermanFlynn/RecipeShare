namespace RecipeShare.Utils.Models
{
	public interface IModel
	{
		Guid Id { get; set; }
		bool IsDeleted { get; set; }
		DateTime CreatedDateTime { get; set; }
		DateTime ModifiedDateTime { get; set; }
	}
}

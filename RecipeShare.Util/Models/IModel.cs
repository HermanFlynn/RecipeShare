namespace RecipeShare.Util.Models
{
	public interface IModel
	{
		string? Id { get; set; }
		bool? IsDeleted { get; set; }
		DateTime? CreatedDateTime { get; set; }
		DateTime? ModifiedDateTime { get; set; }
	}
}

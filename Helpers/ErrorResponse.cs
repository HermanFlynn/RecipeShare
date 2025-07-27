using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RecipeShare.API.Helpers
{
	[ExcludeFromCodeCoverage]
	public class ErrorResponse
	{
		/// <summary>
		/// The error message when an error occurred
		/// </summary>
		[JsonPropertyName("message")]
		public string Message { get; set; }

		/// <summary>
		/// Additional error info, if any
		/// </summary>
		[JsonPropertyName("internalExceptionMessage")]
		public string? InternalExceptionMessage { get; set; }
	}
}

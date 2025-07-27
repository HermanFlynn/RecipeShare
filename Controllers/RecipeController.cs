using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RecipeShare.API.Helpers;
using RecipeShare.Util.Models;
using RecipeShare.Util.Models.Database;
using RecipeShare.Util.Services;
using System.Diagnostics.CodeAnalysis;

namespace RecipeShare.API.Controllers
{
	[ExcludeFromCodeCoverage]
	[ApiController]
	[EnableCors(policyName: "allowedSpecificOrigins")]
	[Route("recipe")]
	public class RecipeController : ControllerBase
	{
		private readonly IRecipeService _recipeService;

		public RecipeController(IRecipeService recipeService)
		{
			_recipeService = recipeService;
		}

		[HttpPost("create")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesErrorResponseType(typeof(ErrorResponse))]
		[AllowAnonymous]
		public async Task<IActionResult> Create([FromBody] Recipe recipe)
		{
			if (recipe == null)
			{
				return BadRequest(new ErrorResponse
				{
					Message = "Recipe data cannot be null.",
					InternalExceptionMessage = "The request body did not contain valid recipe data."
				});
			}

			try
			{
				var createdRecipe = await _recipeService.CreateAsync(recipe, Guid.NewGuid().ToString());

				return Ok(createdRecipe);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
				{
					Message = "An unexpected error occurred while creating the recipe.",
					InternalExceptionMessage = e.Message
				});
			}
		}

		[HttpPost("read")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesErrorResponseType(typeof(ErrorResponse))]
		[AllowAnonymous]
		public async Task<IActionResult> Read(string id)
		{
			try
			{
				var result = await _recipeService.GetAsync(id);
				if (result == null)
				{ return Ok("Item not found"); }

				return Ok(result);
			}
			catch (Exception e)
			{
				return StatusCode(500, new ErrorResponse
				{
					Message = e.Message,
					InternalExceptionMessage = e.InnerException?.Message
				});
			}
		}

		[HttpPost("filterAsync")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesErrorResponseType(typeof(ErrorResponse))]
		[AllowAnonymous]
		public async Task<IActionResult> FilterAsync(List<string> tags)
		{
			try
			{
				var result = await _recipeService.FilterAsync(tags);

				return Ok(result);
			}
			catch (Exception e)
			{
				return StatusCode(500, new ErrorResponse
				{
					Message = e.Message,
					InternalExceptionMessage = e.InnerException?.Message
				});
			}
		}

		[HttpPost("readAll")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesErrorResponseType(typeof(ErrorResponse))]
		[AllowAnonymous]
		public async Task<IActionResult> ReadAll()
		{
			try
			{
				var result = await _recipeService.GetAllAsync();

				return Ok(result);
			}
			catch (Exception e)
			{
				return StatusCode(500, new ErrorResponse
				{
					Message = e.Message,
					InternalExceptionMessage = e.InnerException?.Message
				});
			}
		}

		[HttpPost("update")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesErrorResponseType(typeof(ErrorResponse))]
		[AllowAnonymous]
		public async Task<IActionResult> Update(string id, [FromBody] Recipe recipe)
		{
			if (recipe == null)
			{
				return BadRequest(new ErrorResponse
				{
					Message = "Recipe data cannot be null.",
					InternalExceptionMessage = "The request body did not contain valid recipe data."
				});
			}

			if (!string.IsNullOrEmpty(recipe.Id) && id != recipe.Id)
			{
				return BadRequest(new ErrorResponse
				{
					Message = "Mismatched IDs.",
					InternalExceptionMessage = $"The ID '{id}' in the route does not match the ID '{recipe.Id}' in the request body."
				});
			}

			recipe.Id = id;

			try
			{
				var result = await _recipeService.UpdateAsync(recipe, id);

				if (result == null)
				{
					return NotFound(new ErrorResponse
					{
						Message = $"Recipe with ID '{id}' not found.",
						InternalExceptionMessage = $"Attempted to update a non-existent recipe with ID: {id}"
					});
				}

				return Ok(result);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
				{
					Message = "An unexpected error occurred while updating the recipe.",
					InternalExceptionMessage = e.Message
				});
			}
		}

		[HttpPost("delete")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesErrorResponseType(typeof(ErrorResponse))]
		[AllowAnonymous]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				var result = await _recipeService.DeleteAsync(id);

				if (result == null)
				{ return Ok("Item not found or already deleted"); }

				return Ok("Item Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, new ErrorResponse
				{
					Message = e.Message,
					InternalExceptionMessage = e.InnerException?.Message
				});
			}
		}
	}
}
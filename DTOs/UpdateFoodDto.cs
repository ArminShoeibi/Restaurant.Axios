using Restaurant.Axios.Domain;
using System;

namespace Restaurant.Axios.DTOs
{
    public record UpdateFoodDto : CreateFoodDto
    {
        public int FoodId { get; init; }
        public string PhotoPath { get; init; }

        public static UpdateFoodDto MapFoodEntityToUpdateFoodDto(Food food)
        {
            return new UpdateFoodDto
            {
                Description = food.Description,
                Nationality = food.Nationality,
                Ingredients = food.Ingredients,
                Name = food.Name,
                PhotoPath = food.PhotoPath,
                FoodId = food.FoodId,
            };
        }

        public void MapUpdateFoodDtoToFoodEntity(Food food)
        {
            food.DateModified = DateTimeOffset.Now;
            food.Description = Description;
            food.Name = Name;
            food.Ingredients = Ingredients;
            food.Nationality = Nationality;
        }
    }
}

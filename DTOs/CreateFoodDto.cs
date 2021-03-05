using Microsoft.AspNetCore.Http;
using Restaurant.Axios.Domain;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Axios.DTOs
{
    public record CreateFoodDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(40)]
        public string Name { get; init; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string Description { get; init; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Ingredients { get; init; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(30)]
        public string Nationality { get; init; }

        public IFormFile Photo { get; init; }


        public Food MapToFoodEntity()
        {
            return new Food
            {
                Description = Description,
                Ingredients = Ingredients,
                Name = Name,
                Nationality = Nationality,
            };
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Axios.Data;
using Restaurant.Axios.Domain;
using Restaurant.Axios.DTOs;
using Restaurant.Axios.Utility;
using Restaurant.Axios.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Axios.Controllers
{
    public class FoodsController : Controller
    {
        private readonly RestaurantContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FoodsController(RestaurantContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> GetAllFoods()
        {
            List<FoodViewModel> foods =
                 await _db.Foods
                          .Select(f => new FoodViewModel
                          {
                              FoodId = f.FoodId,
                              DateCreated = f.DateCreated.DateTime.ToString(),
                              DateModified = f.DateModified.DateTime.ToString(),
                              Description = f.Description,
                              Ingredients = f.Ingredients,
                              Name = f.Name,
                              Nationality = f.Nationality,
                              PhotoPath = f.PhotoPath,
                          })
                          .ToListAsync();

            return PartialView(foods);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateFood() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFood(CreateFoodDto createFoodDto, [FromServices] IWebHostEnvironment webHostEnvironment)
        {

            if (ModelState.IsValid)
            {
                Food newFood = createFoodDto.MapToFoodEntity();
                if (createFoodDto.Photo is not null && createFoodDto.Photo.Length > 0)
                {
                    newFood.PhotoPath = await createFoodDto.Photo.UploadAsync(webHostEnvironment);
                }

                await _db.Foods.AddAsync(newFood);
                await _db.SaveChangesAsync();

                return Json(new AxiosResponseDto { Success = true, Message = "the food was successfully added", Title = "Added" });

            }
            return Json(new AxiosResponseDto { Success = false, Message = "please enter right values for inputs.", Title = "Error" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFood(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "wrong Food Id, Please refresh the page." });
            }

            Food foodToUpdate = await _db.Foods.FindAsync(id);

            if (foodToUpdate is null)
            {

                return Json(new { success = false, message = "Food doesn't exist." });
            }

            UpdateFoodDto updateFoodDto = UpdateFoodDto.MapFoodEntityToUpdateFoodDto(foodToUpdate);
            return PartialView(updateFoodDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFood(UpdateFoodDto updateFoodDto)
        {
            if (ModelState.IsValid)
            {
                Food foodToUpdate = await _db.Foods.FindAsync(updateFoodDto.FoodId);
                if (foodToUpdate is null)
                {
                    return Json(new AxiosResponseDto { Success = false, Message = "this food doesn't exist.", Title = "Error" });
                }

                updateFoodDto.MapUpdateFoodDtoToFoodEntity(foodToUpdate);
                if (updateFoodDto.PhotoPath is not null && updateFoodDto.Photo is not null)
                {
                    foodToUpdate.PhotoPath = await updateFoodDto.Photo.UploadAsync(_webHostEnvironment);
                }

                await _db.SaveChangesAsync();
                return Json(new AxiosResponseDto { Success = true, Message = "the food was updated successfully.", Title = "Updated" });


            }
            return Json(new AxiosResponseDto { Success = false, Message = "please enter right values for inputs.", Title = "Error" });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteFood(int id)
        {
            if (id <= 0)
            {
                return Json(new AxiosResponseDto { Success = false, Message = "wrong food id, please refresh the page.", Title = "Error" });
            }

            Food foodToDelete = await _db.Foods.FindAsync(id);
            if (foodToDelete is null)
            {
                return Json(new AxiosResponseDto { Success = false, Message = "this food doesn't exist.", Title = "Error" });
            }

            _db.Foods.Remove(foodToDelete);
            await _db.SaveChangesAsync();
            return Json(true);
        }

    }


}

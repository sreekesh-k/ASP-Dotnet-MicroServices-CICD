using FoodApi.Controllers;
using FoodApi.Data;
using FoodApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Tests.Controllers
{
    public class FoodControllerTests
    {
        // Helper method to create an in-memory database context
        private FoodDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<FoodDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // New DB for each test
                .Options;

            return new FoodDbContext(options);
        }

        // Test for GetFoods
        [Fact]
        public async Task GetFoods_ReturnsFoods_WhenFoodsExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.foods.Add(new Food {  Name = "Apple", Quantity = 10, Description = "Fresh apples" });
            context.foods.Add(new Food {Name = "Banana", Quantity = 5, Description = "Ripe bananas" });
            await context.SaveChangesAsync();

            var controller = new FoodController(context);

            // Act
            var result = await controller.GetFoods();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var foods = Assert.IsType<List<Food>>(okResult.Value);
            Assert.Equal(2, foods.Count);
        }

        // Test for GetFood by ID
        [Fact]
        public async Task GetFood_ReturnsFood_WhenFoodExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var food = new Food { Id=10, Name = "Grapes", Quantity = 10, Description = "Fresh Grapes" };
            context.foods.Add(food);
            await context.SaveChangesAsync();

            var controller = new FoodController(context);

            // Act
            var result = await controller.GetFood(10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFood = Assert.IsType<Food>(okResult.Value);
            Assert.Equal(food.Id, returnedFood.Id);
        }

        [Fact]
        public async Task GetFood_ReturnsNotFound_WhenFoodDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new FoodController(context);

            // Act
            var result = await controller.GetFood(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        // Test for CreateFood
        [Fact]
        public async Task CreateFood_AddsFoodToDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new FoodController(context);
            var food = new Food { Name = "Orange", Quantity = 15, Description = "Juicy oranges" };

            // Act
            var result = await controller.CreateFood(food);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdFood = Assert.IsType<Food>(createdAtActionResult.Value);
            Assert.Equal(food.Name, createdFood.Name);
        }

        // Test for UpdateFood
        [Fact]
        public async Task UpdateFood_UpdatesExistingFood()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var food = new Food { Id = 12, Name = "Mango", Quantity = 10, Description = "Fresh Mango" };
            context.foods.Add(food);
            await context.SaveChangesAsync();

            var controller = new FoodController(context);
            var updatedFood = new Food { Name = "Updated Mango", Quantity = 20, Description = "Updated description" };

            // Act
            var result = await controller.UpdateFood(12,updatedFood);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var savedFood = await context.foods.FindAsync(12);
            Assert.Equal("Updated Mango", savedFood.Name);
            Assert.Equal(20, savedFood.Quantity);
            Assert.Equal("Updated description", savedFood.Description);
        }

        [Fact]
        public async Task UpdateFood_ReturnsNotFound_WhenFoodDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new FoodController(context);
            var updatedFood = new Food { Id = 99, Name = "Nonexistent", Quantity = 0, Description = "Nonexistent food" };

            // Act
            var result = await controller.UpdateFood(99, updatedFood);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        // Test for DeleteFood
        [Fact]
        public async Task DeleteFood_RemovesFoodFromDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var food = new Food { Id = 15, Name = "Jack Fruit", Quantity = 10, Description = "Fresh Jack Fruit" };
            context.foods.Add(food);
            await context.SaveChangesAsync();

            var controller = new FoodController(context);

            // Act
            var result = await controller.DeleteFood(15);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var deletedFood = await context.foods.FindAsync(15);
            Assert.Null(deletedFood);
        }

        [Fact]
        public async Task DeleteFood_ReturnsNotFound_WhenFoodDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new FoodController(context);

            // Act
            var result = await controller.DeleteFood(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}

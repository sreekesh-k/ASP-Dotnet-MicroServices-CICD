using FoodApi.Data;
using FoodApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodDbContext _context;

        public FoodController(FoodDbContext context)
        {
            _context = context;
        }

        // GET: api/foods
        [HttpGet]
        public async Task<IActionResult> GetFoods(string name = null, int? quantity = null)
        {
            IQueryable<Food> foods = _context.foods;

            if (!string.IsNullOrEmpty(name))
            {
                foods = foods.Where(f => f.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (quantity.HasValue)
            {
                foods = foods.Where(f => f.Quantity == quantity.Value);
            }

            var result = await foods.ToListAsync();

            if (result.Count == 0)
            {
                return NotFound(new { Message = "No foods found matching the criteria." });
            }

            return Ok(result);
        }

        // GET: api/foods/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(int id)
        {
            var item = await _context.foods.FindAsync(id);

            if (item == null)
            {
                return NotFound(new { Message = $"Food with ID {id} not found." });
            }

            return Ok(item);
        }

        // POST: api/foods
        [HttpPost]
        public async Task<IActionResult> CreateFood([FromBody] Food food)
        {
            if (food == null)
            {
                return BadRequest(new { Message = "Food data is invalid." });
            }

            await _context.foods.AddAsync(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
        }

        // PUT: api/foods/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(int id, [FromBody] Food food)
        {
            if (food == null)
            {
                return BadRequest(new { Message = "Food data is invalid." });
            }

            var existingFood = await _context.foods.FindAsync(id);

            if (existingFood == null)
            {
                return NotFound(new { Message = $"Food with ID {id} not found." });
            }

            existingFood.Name = food.Name;
            existingFood.Quantity = food.Quantity;
            existingFood.Description = food.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/foods/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var item = await _context.foods.FindAsync(id);

            if (item == null)
            {
                return NotFound(new { Message = $"Food with ID {id} not found." });
            }

            _context.foods.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

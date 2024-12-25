using Back.Models;
using Back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventory;

        public InventoryController(IInventoryService inventory)
        {
            _inventory = inventory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCard>>> GetAll()
        {
            var items = await _inventory.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCard>> GetById(string id)
        {
            var item = await _inventory.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemCard>> Create(ItemCard newItem)
        {
            var created = await _inventory.CreateAsync(newItem);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ItemCard updated)
        {
            var success = await _inventory.UpdateAsync(id, updated);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _inventory.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoController(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ToDoModel>> GetToDoItems()
        {
            return await _dbContext.ToDoList.ToListAsync();
        }


        // api/ToDo/getItem/1
        [HttpGet]
        [Route("getitems/{id}")]
        public async Task<ActionResult> GetToDoItems(int id)
        {
            var item = await _dbContext.ToDoList.FindAsync(id);
            if(item == null) return BadRequest("Item Not Found");

            return Ok(item);
        }

        // api/ToDo/AddTask 

        [HttpPost]
        [Route("AddTask")]
        public async Task<ActionResult<ToDoModel>> CreateToDoTask(ToDoModel item)
        {
            if (item == null) return BadRequest("Please Insert Task");
            if (item.Title == null) return BadRequest("Please Enter Task Name");
            if (item.Status == null) item.Status = "Incomplete";
            _dbContext.ToDoList.Add(item);
            await _dbContext.SaveChangesAsync();

            return Ok("Task Created Sucessfully");
        }

        // api/ToDo/Delete/1

        [HttpDelete]
        [Route("DeleteTask/{id}")]
        public async Task<ActionResult> DeleteToDo(int id)
        {
            var item = await _dbContext.ToDoList.FindAsync(id);
            if (item == null) return BadRequest("Item Not Found");

            _dbContext.ToDoList.Remove(item);
            await _dbContext.SaveChangesAsync();
            return Ok("Task deleted Sucessfully");
        }

        // api/ToDo/UpdateTask/1

        [HttpPut]
        [Route("UpdateTask/{id}")]
        public async Task<IActionResult> UpdateTask(int id,[FromBody] ToDoModel model)
        {
            var item = _dbContext.ToDoList.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return BadRequest("No Data Found");
                if (model.Title != null) item.Title = model.Title;
                if (model.Status != null) item.Status = model.Status;

            _dbContext.ToDoList.Update(item);
            await _dbContext.SaveChangesAsync();

            return Ok("Task Updated Sucessfully");
        }
    }
}

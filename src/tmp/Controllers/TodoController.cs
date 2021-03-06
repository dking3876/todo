using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoContext _context;
        public TodoController(TodoContext context){
            _context = context;
            if(_context.TodoITems.Count() < 5 ){
                for(int i = 0; i<5;++i){
                _context.TodoITems.Add(new TodoItem {Name = "Item number" + i});
                _context.SaveChanges();
                }
            }
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll(){
            return _context.TodoITems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id){
            var item = _context.TodoITems.Find(id);
            if(item == null){
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(TodoItem item){
            _context.TodoITems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.Id}, item);
        }

        [HttpPutAttribute("{id}")]
        public IActionResult Update(long id, TodoItem item){

            var todo = _context.TodoITems.Find(id);
            if(todo == null){
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            _context.TodoITems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
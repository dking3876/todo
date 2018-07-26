using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Shared.Models;

namespace Todo.Web.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoContext _context;
        public TodoController(TodoContext context){
            _context = context;
            if(_context.TodoItems.Count() < 5 ){
                for(int i = 0; i<5;++i){
                _context.TodoItems.Add(new TodoItem {Name = "Item number" + i});
                _context.SaveChanges();
                }
            }
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll(){
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id){
            var item = _context.TodoItems.Find(id);
            if(item == null){
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(TodoItem item){
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.Id}, item);
        }

        [HttpPutAttribute("{id}")]
        public IActionResult Update(long id, TodoItem item){

            var todo = _context.TodoItems.Find(id);
            if(todo == null){
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
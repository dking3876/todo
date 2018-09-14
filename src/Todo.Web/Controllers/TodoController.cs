using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Shared.Models;
using TodoApi.Shared;
using Todo.server;

namespace Todo.Web.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly ITodo _context;
        public TodoController(ITodo context) { 
            _context = context;
            
            if(_context.Getall().Result.Count() < 5 ){
                for(int i = 0; i<5;++i){
                _context.Create(new TodoItemPublic {
                    Name = "Item number" + i,
                    User = new User {
                        Id = 4,
                        FirstName = "",
                        LastName = "",
                        Fullname = ""
                    }
                });
                }
            }
        }

        [HttpGet]
        public ActionResult<List<TodoItemPublic>> GetAll() {

            var ToDos = _context.Getall();
            return ToDos.Result;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItemPublic> GetById(int id){
            var item = _context.Getbyid(id);
            if(item == null){
                return NotFound();
            }
            return item.Result;
        }

        [HttpPost]
        public IActionResult Create(TodoItemPublic item){
            TodoItemPublic _item = _context.Create(item).Result;
            return CreatedAtRoute("GetTodo", new { id = _item.Id}, _item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItemPublic item){

            var todo = _context.Update(id, item);
  
            if(todo == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}
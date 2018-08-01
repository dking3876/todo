using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TodoApi.Shared;
using TodoApi.Shared.Models;
using Newtonsoft.Json;

namespace Todo.Client
{
    public class TodoClient : ITodo
    {
        public TodoItem Create(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TodoItem>> Getall()
        {
            //make api call to the webapi and return the list of todos
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.GetAsync("http://localhost:56576/api/todos");
            var content = await response.Content.ReadAsStringAsync();
            return DeserializeData<List<TodoItem>>(content);
            
        }

        public List<TodoItem> Getall(bool IsComplete, int limit = 5, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public TodoItem Getbyid(long id)
        {
            throw new NotImplementedException();
        }

        public TodoItem Update(long id, TodoItem item)
        {
            throw new NotImplementedException();
        }

        private T DeserializeData<T>(string data)
        {
            var jsonSerialiserSettings = new JsonSerializerSettings();

            var deserialisedObject = JsonConvert.DeserializeObject<T>(data, jsonSerialiserSettings);

            return deserialisedObject;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TodoApi.Shared;
using TodoApi.Shared.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Todo.Client
{
    public class TodoClient : ITodo
    {
        private IConfiguration _configuration;

        private readonly ITodoConnection connection;

        public TodoClient(IConfiguration Configuration, ITodoConnection connection )
        {
            _configuration = Configuration;
            //do url configuration with injection
            _url = Configuration.GetSection("TodoApp:ApiUrl").Value;

        }
        private string _url;

        public async Task<TodoItemPublic> Create(TodoItemPublic item)
        {

            HttpClient request = new HttpClient();
            var result = await request.PostAsync(this._url + "todo", new StringContent( JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json" ));
            string resultContent = await result.Content.ReadAsStringAsync();
            TodoItemPublic todo = DeserializeData<TodoItemPublic>(resultContent);
            return todo;
            
            //var content = new FormUrlEncodedContent(item. );
        }

        public async Task<List<TodoItemPublic>> Getall()
        {
            //make api call to the webapi and return the list of todos
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.GetAsync(this._url + "todo");
            var content = await response.Content.ReadAsStringAsync();
            List<TodoItemPublic> todos = DeserializeData<List<TodoItemPublic>>(content);
            return todos;

        }

        public async Task<List<TodoItemPublic>> Getall(bool IsComplete = false, int limit = 5, int offset = 0)
        {
            Dictionary<string, string> _query = new Dictionary<string, string>()
            {
                { "IsComplete", IsComplete.ToString() },
                { "limit", limit.ToString() },
                { "offset", offset.ToString() }
            };
            string url = this._url + "todo?" + this.QueryString(_query);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            List<TodoItemPublic> todos = DeserializeData<List<TodoItemPublic>>(content);
            return todos;

            throw new NotImplementedException();
        }

        public async Task<TodoItemPublic> Getbyid(int id)
        {
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.GetAsync(this._url + "todo/"+id);
            var content = await response.Content.ReadAsStringAsync();
            TodoItemPublic todo = DeserializeData<TodoItemPublic>(content);
            return todo;
        }

        public Task<TodoItemPublic> Update(int id, TodoItemPublic item)
        {
            throw new NotImplementedException();
        }

        private T DeserializeData<T>(string data)
        {
            var jsonSerialiserSettings = new JsonSerializerSettings();
            var deserialisedObject = JsonConvert.DeserializeObject<T>(data, jsonSerialiserSettings);
            return deserialisedObject;
        }


        private string QueryString(IDictionary<string, string> dict)
        {
            var list = new List<string>();
            foreach (var item in dict)
            {
                list.Add(item.Key + "=" + item.Value);
            }
            return string.Join("&", list);
        }

    }
}

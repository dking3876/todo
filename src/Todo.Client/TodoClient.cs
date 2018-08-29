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
        private string url = "http://localhost:56576/api/";

        public async Task<TodoItem> Create(TodoItem item)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("IsComplete", item.IsComplete.ToString()),
                new KeyValuePair<string, string>("Name", item.Name)
            });

            HttpClient request = new HttpClient();
            var result = await request.PostAsync(this.url + "todo", new StringContent( JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json" ));
            string resultContent = await result.Content.ReadAsStringAsync();
            TodoItem todo = DeserializeData<TodoItem>(resultContent);
            return todo;
            
            //var content = new FormUrlEncodedContent(item. );
        }

        public async Task<List<TodoItem>> Getall()
        {
            //make api call to the webapi and return the list of todos
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.GetAsync(this.url + "todo");
            var content = await response.Content.ReadAsStringAsync();
            List<TodoItem> todos = DeserializeData<List<TodoItem>>(content);
            return todos;

        }

        public async Task<List<TodoItem>> Getall(bool IsComplete = false, int limit = 5, int offset = 0)
        {
            Dictionary<string, string> _query = new Dictionary<string, string>()
            {
                { "IsComplete", IsComplete.ToString() },
                { "limit", limit.ToString() },
                { "offset", offset.ToString() }
            };
            string url = this.url + "todo?" + this.QueryString(_query);
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            List<TodoItem> todos = DeserializeData<List<TodoItem>>(content);
            return todos;

            throw new NotImplementedException();
        }

        public async Task<TodoItem> Getbyid(long id)
        {
            HttpClient request = new HttpClient();
            HttpResponseMessage response = await request.GetAsync(this.url + "todo/"+id);
            var content = await response.Content.ReadAsStringAsync();
            TodoItem todo = DeserializeData<TodoItem>(content);
            return todo;
        }

        public Task<TodoItem> Update(long id, TodoItem item)
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

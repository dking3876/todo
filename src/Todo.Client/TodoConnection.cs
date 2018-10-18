using Brafton.Http.RestClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Client
{
    public class TodoConnection: BaseRestApiConnection, ITodoConnection
    {
        public TodoConnection(Uri baseUrl, string accessToken, ICallerVersions callerVersions) : base(baseUrl, accessToken, callerVersions)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Settings
{
    public class TodoSettings
    {

        public string Endpoint { get; set; }

        public string Auth0ClientId { get; set; }

        public bool UseApplicationCredsByDefault { get; set; } = true;
    }
}

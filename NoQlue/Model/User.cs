using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoQlue.Model
{
    
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Type type { get; set; }

        public enum Type
        {
            Teacher,
            Student
        }

    }
}

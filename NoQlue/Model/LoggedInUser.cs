using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoQlue.Model
{
    public class LoggedInUser
    {
        public static User ActiveUser { get; set; }
        private LoggedInUser()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoQlue.Data
{
    static class Constants
    {
        public const string ApiBaseUrl = "http://10.0.2.2:1337/api/";
        public const string LoginUserUrl = ApiBaseUrl + "login";
        public const string AskQuestionUrl = ApiBaseUrl + "questions/add";
        public const string AddPartimStudentUrl = ApiBaseUrl + "partims/register";
        public const string AddPartimTeacherUrl = ApiBaseUrl + "partims/add";
    }
}

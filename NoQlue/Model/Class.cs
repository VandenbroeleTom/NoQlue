using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoQlue.Model
{
    public class Class
    {
        public Class() { }
        public Class(string name, string teacherName, int code)
        {
            Name = name;
            TeacherName = teacherName;
            Code = code;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public int Code { get; set; }

        public override string ToString()
        {
            return $"{Name} - {TeacherName}";
        }
    }
}

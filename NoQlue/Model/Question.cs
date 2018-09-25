using System;
namespace NoQlue.Model {
    public class Question {
        public Question() {
        }

        public User TheUser { get; set; }
        public string TheQuestion { get; set; }
        public Class TheClass { get; set; }

        public override string ToString()
        {
            return TheQuestion;
        }

    }
}


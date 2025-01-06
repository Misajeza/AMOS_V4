using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AMOS_V4
{
    internal class Lecture
    {
        public Lecture(string path) {
            string Jstring = File.ReadAllText(path);
            Questions = JsonSerializer.Deserialize<List<Question>>(Jstring)!;
        }
        public List<Question> Questions;
    }
    internal class Question
    {
        public Question(string Name, float Mark, string[] Images, QuestionLine[] QuestionLines) 
        {
            this.Name = Name;
            this.Mark = Mark;
            this.Images = Images;
            this.QuestionLines = QuestionLines;
        }
        public string Name { get; set; }
        public float Mark { get; set; }
        public string[] Images { get; set; }
        public QuestionLine[] QuestionLines { get; set; }
    }
    internal class QuestionLine
    {
        public QuestionLine(string Query, string[] Answer, bool Swapable, bool CharSize) 
        {
            this.Query = Query;
            this.Answer = Answer;
            this.Swapable = Swapable;
            this.CharSize = CharSize;
        }
        public bool CharSize { get; set; }
        public bool Swapable { get; set; }
        public string Query { get; set; }
        public string[] Answer { get; set; }
    }
}

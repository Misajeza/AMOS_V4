using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AMOS_V4
{

    public class Lecture
    {
        public Lecture(string path)
        {
            var zippedData = new AmosFile(path);
            Questions = zippedData.ReadJson<List<Question>>();
            Images = zippedData.ReadImages();
            Name = Path.GetFileNameWithoutExtension(path);
        }
        public Lecture (List<Question> questions, Dictionary<string, Bitmap> Images, string name) {this.Questions = questions; this.Images = Images; this.Name = name; }
        public List<Question> Questions { get; set;}
        public Dictionary<string, Bitmap> Images { get; set; }
        public string Name { get; set; }
        public static Lecture Empty { get { return new Lecture(new List<Question>(), new Dictionary<string, Bitmap>(),""); } }
        public void NewQuestion() 
        {
            List<QuestionLine> questionLines = new List<QuestionLine>();
            questionLines.Add(new QuestionLine("", new List<string>(), false, false));
            Questions.Add(new Question("", 5, new List<string>(), questionLines));
        }
    }
    public class Question
    {
        public Question(string Name, float Mark, List<string> ImageNames, List<QuestionLine> QuestionLines) 
        {
            this.Name = Name;
            this.Mark = Mark;
            this.ImageNames = ImageNames;
            this.QuestionLines = QuestionLines;
        }
        public string Name { get; set; }
        public float Mark { get; set; }
        public List <string> ImageNames { get; set; }
        public List <QuestionLine> QuestionLines { get; set; }

        public static Question Empty { get { return new Question("",5,new List<string>(), new List<QuestionLine>()); } }

        public void AddLine()
        {
            List<string> answers = new List<string>();
            QuestionLines.Add(new QuestionLine("", answers, false, false));
        }
        public void RemoveLine(int index)
        {
            QuestionLines.RemoveAt(index);
        }
    }
    public class QuestionLine
    {
        public QuestionLine(string Query, List<string> Answer, bool Swapable, bool CharSize) 
        {
            this.Query = Query;
            this.Answer = Answer;
            this.Swapable = Swapable;
            this.CharSize = CharSize;
            
        }
        public bool CharSize { get; set; }
        public bool Swapable { get; set; }
        public string Query { get; set; }
        public List <string> Answer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace AMOS_V4
{
    public class Quiz
    {
        public Quiz() 
        {
            Lectures = new Files();
            lastSelected = Question.Empty;
        }
        

        //private List <string> LecturePaths = new List<string>();

        public Lecture ?SelectedLecture;
        public Files Lectures;
        public int ConservationValue = 5;

        private QuizMode mode = QuizMode.Smart;

        //string selLecturePath = string.Empty;
        int selLectureIndx = -1;
        int selQuestionIndx = -1;
        int selImageIndx = -1;

        Question lastSelected;

        Random random = new Random();
        public Bitmap? Image
        {
            get 
            {
                if (SelectedLecture == null || Question == null) return null;
                if (SelectedLecture.Questions[selQuestionIndx].ImageNames.Count==0) return null;
                else return SelectedLecture.Images[SelectedLecture.Questions[selQuestionIndx].ImageNames[selImageIndx]];
            }
            set 
            { 
                if (SelectedLecture != null) 
                SelectedLecture.Images[SelectedLecture.Questions[selQuestionIndx].ImageNames[selImageIndx]] = value;
            }
        }
        public Question? Question {
            get { if (SelectedLecture == null || selQuestionIndx == -1)return null;
                return SelectedLecture.Questions[selQuestionIndx]; }
            set { if (SelectedLecture != null)
                SelectedLecture.Questions[selQuestionIndx] = value; }
        }
        public int SelectedImageIndex { get => selImageIndx; }
        public int SelectedQuestionIndex { get => selQuestionIndx; }
        public bool IsQuestionLast { get => selQuestionIndx + 1 >= SelectedLecture.Questions.Count; }
        public bool IsQuestionFirst { get => selQuestionIndx-1 < 0; }
        public bool IsImageLast { get => selImageIndx + 1 >= Question.ImageNames.Count; }
        public bool IsImageFirst { get => selImageIndx-1 < 0; }

        public event EventHandler? LectureChanged;
        public event EventHandler? LectureClosed;
        public event EventHandler? LectureOpened;
        public event EventHandler? QuestionChanged;
        public event EventHandler? ImageChanged;

        public QuizMode Mode { get => mode; set => mode = value; }
        //public string SelLecturePath { get => selLecturePath; set => selLecturePath = value; }
        public int SelectedLectureIndex { get => selLectureIndx; set => selLectureIndx = value; }

        public void OpenLecture(string path)
        {
            Lectures.Open(path);
            LectureOpened?.Invoke(this, EventArgs.Empty);
        }
        public void CloseLecture(int indx) {
            Lectures.Close(indx);
            if (Lectures.Count >0)
                SelectLecture(0);
            else SelectedLecture = null;
            LectureClosed?.Invoke(this, EventArgs.Empty);
        }
        public void RandomChangeSellection()
        {

            if (Lectures.Count < 1) 
            {
                SelectedLecture = Lecture.Empty;
                return;
            }
            selLectureIndx = random.Next(0, Lectures.Count);
            SelectedLecture = Lectures.GetLecture(selLectureIndx,true);
            if (SelectedLecture.Questions.Count > 0)
            {
                while (true) 
                {
                    if (Mode == QuizMode.Normal)
                        selQuestionIndx = random.Next(SelectedLecture.Questions.Count);
                    else
                        selQuestionIndx = weightedChoice(SelectedLecture.Questions);
                    selImageIndx = random.Next(SelectedLecture.Questions[selQuestionIndx].ImageNames.Count);
                    if (SelectedLecture.Questions.Count == 1 || lastSelected != Question) break ;
                }
                lastSelected = Question;
            }
            else
                selQuestionIndx = -1;
            if (LectureChanged != null)
                LectureChanged.Invoke(this, EventArgs.Empty);
            if (QuestionChanged != null)
                QuestionChanged.Invoke(this, EventArgs.Empty);
        }
        public void SellectNextQuestion() {
            if (SelectedLecture != null && selQuestionIndx < SelectedLecture.Questions.Count()-1)
            {
                selQuestionIndx++;
                selImageIndx = random.Next(SelectedLecture.Questions[selQuestionIndx].ImageNames.Count());
                if (QuestionChanged != null)
                    QuestionChanged.Invoke(this, new EventArgs());
            }
        }
        public void SellectPreviousQuestion() {
            if (SelectedLecture != null && selQuestionIndx > 0)
            {
                selQuestionIndx--;
                selImageIndx = random.Next(SelectedLecture.Questions[selQuestionIndx].ImageNames.Count());
                if (QuestionChanged != null)
                    QuestionChanged.Invoke(this, new EventArgs());
            }
        }
        public void SellectNextImage()
        {
            if (SelectedLecture != null && selImageIndx+1 < Question.ImageNames.Count)
            {
                selImageIndx++;
                if (ImageChanged != null)
                    ImageChanged.Invoke(this, new EventArgs());
            }
        }
        public void SellectPreviousImage()
        {
            if (SelectedLecture != null && selImageIndx > 0)
            {
                selImageIndx--;
                if (ImageChanged != null)
                    ImageChanged.Invoke(this, new EventArgs());
            }
        }
        public void CreateNewQuestion()
        {
            SelectedLecture.NewQuestion();
            SelectQuestion(SelectedLecture.Questions.Count-1);
        }
        public void RemoveQuestion(Question question)
        {
            foreach (string imagename in Question.ImageNames)
                SelectedLecture.Images.Remove(imagename); ;
            SelectedLecture.Questions.Remove(question);

            if (selQuestionIndx >= SelectedLecture.Questions.Count)
                selQuestionIndx--;
            if (selQuestionIndx < 0)
                selQuestionIndx++;
            if (SelectedLecture.Questions.Count==0)
                selQuestionIndx = -1;
            else
                selImageIndx = random.Next(Question.ImageNames.Count);
            if (QuestionChanged != null)
                QuestionChanged.Invoke(this, new EventArgs());
        }
        public void SelectLecture(int indx)
        {
            selLectureIndx = indx;
            SelectedLecture = Lectures.GetLecture(indx,true);
            if (SelectedLecture.Questions.Count > 0) {
                selQuestionIndx = 0;
                selImageIndx = 0;
            
            }
            else 
            {
                selQuestionIndx = -1;
                selImageIndx = 0;
            }
                
            
            if (LectureChanged != null)
                LectureChanged.Invoke(this, new EventArgs());
        }
        public void SelectQuestion(int index)
        {
            if (SelectedLecture.Questions.Count==0) return;
            selQuestionIndx = index;
            selImageIndx = random.Next(Question.ImageNames.Count);
            if (QuestionChanged != null)
                QuestionChanged.Invoke(this, new EventArgs());
        }
        public void SelectImage(int index)
        {
            selImageIndx = index;
        }
        public void AddImage(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            if (fileName != "") {
                if (SelectedLecture.Images.ContainsKey(fileName + ".jpg"))
                {
                    int extension = 1;
                    while (SelectedLecture.Images.ContainsKey(fileName+extension + ".jpg"))  extension++;
                    fileName += extension;
                }
                SelectedLecture.Images.Add(fileName + ".jpg", resize(new Bitmap(path), 720));
                Question.ImageNames.Add(Path.GetFileName(fileName + ".jpg"));
            }
        }
        public void RemoveImage(int index)
        {
            SelectedLecture.Images.Remove(Question.ImageNames[index]);
            Question.ImageNames.RemoveAt(index);
            if (selImageIndx >= Question.ImageNames.Count)
                selImageIndx--;
            if (selImageIndx < 0)
                selImageIndx++;
        }
        public void NewLecture()
        {
            List<QuestionLine> newQuestionLine = new List<QuestionLine>();
            List<Question> question = new List<Question>();
            newQuestionLine.Add(new QuestionLine("", new List<string>(), false, false));
            question.Add(new Question("", 10f, new List<string>(), newQuestionLine));
            Lecture newLecture = new Lecture(question, new Dictionary<string, Bitmap>(), "name");
            Lectures.NewLecture(newLecture);
            SelectLecture(Lectures.Count - 1);
            if (LectureChanged != null)
                LectureChanged.Invoke(this,EventArgs.Empty);
        }
        public enum QuizMode
        {
            Normal,
            Smart,
            Edit
        }
        private static Bitmap resize(Bitmap Image, int longestSide)
        {
            Bitmap Outp;
            if (Image.Width > Image.Height)
            {
                Outp = new Bitmap(Image, new Size(longestSide, (int)Math.Round((float)Image.Height * ((float)longestSide / Image.Width))));
            }
            else
            {
                Outp = new Bitmap(Image, new Size((int)Math.Round((float)Image.Width * ((float)longestSide / Image.Height)), longestSide));
            }
            return Outp;
        }
        private static int weightedChoice(List<Question> questions)
        {
            Random random = new Random();
            int selected = -1;
            int bestScore = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                Question question = questions[i];
                int s = random.Next(0, (int)Math.Round(question.Mark * 100));
                if (bestScore <= s)
                {
                    selected = i;
                    bestScore = s;
                }
            }
            return selected;
        }
    }

    public class Files
    {
        private List<string> paths;
        private List<int> keys;
        private List<Lecture> loaded;

        public List<string> Paths { get => paths; }
        public List<Lecture> Loaded { get => loaded; }
        public List<int> LoadedIndexes { get => keys; }
        

        public Files()
        {
            this.paths = new List<string>();
            this.keys = new List<int>();
            this.loaded = new List<Lecture>();
        }
        public void Open(string path)
        {
            paths.Add(path);
        }
        public void Close(int index) 
        {
            paths.RemoveAt(index);
            if (IsLoaded(index)) {
                loaded.RemoveAt(keys.IndexOf(index));
                keys.Remove(index);
            }
            correction(index);
            
        }
        public void Save(int  index)
        {
            string directory = GetPath(keys[index]);
            AmosFile.CreateZippedData(directory, loaded[index].Images, loaded[index].Questions);
        }
        public void SaveAll() 
        { 
            foreach (var item in loaded.Select((value, i) => new { i, value }))
            {
                string directory = GetPath(keys[item.i]);
                AmosFile.CreateZippedData(directory, item.value.Images, item.value.Questions);
            }
        }
        public void NewLecture(Lecture lecture)
        {
            paths.Add("<NewFile>");
            keys.Add(paths.Count - 1);
            loaded.Add(lecture);
        }
        public Lecture GetLecture(int index, bool load = false)
        {
            if (load&& !IsLoaded(index)) Load(index);
            if (keys.Contains(index))
                return this.loaded[keys.IndexOf(index)];
            else
                throw new Exception("Lection is not loaded");
        }
        public string GetPath(int index) {
            if (isPath(paths[index]))
                return paths[index];
            else
                throw new Exception("File have not path");
        }
        public void SetPath(int index, string path)
        {
            paths[index] = path;
        }
        public string GetName(int index) 
        {
            if (isPath(paths[index]))
                return Path.GetFileNameWithoutExtension(paths[index]);
            else
                return paths[index].Substring(1, paths[index].Length - 2);
        }
        public void SetName(int index, string name)
        {
            paths[index] = $"<{name}>";
        }
        public void Load(int index) 
        {
            if (isPath(paths[index])) {
                if (IsLoaded(index)) throw new Exception("File is loaded");
                else {
                    loaded.Add(new Lecture(paths[index]));
                    keys.Add(index);
                }
            }
            else
                throw new Exception("Path is not defined");
        }
        public void Unload(int index) { 
            loaded.RemoveAt(keys.IndexOf(index));
            keys.Remove(index); 
        }
        public void UnloadAll()
        {
            for (int i = 0; i < loaded.Count; i++)
            {
                loaded.RemoveAt(i); keys.RemoveAt(i);
            }
        }
        public bool IsLoaded(int index)
        {
            return keys.Contains(index);
        }
        public bool IsEmpty { get => paths.Count() < 1; }
        public int Count { get => paths.Count(); }
        public string this[int i]
        {
            get => paths[i];
            set => paths[i] = value;
        }
        private static bool isPath(string path)
        {
            return !path.Contains(">");
        }
        private void correction(int removedIndex) 
        { 
            for (int i = 0; i < keys.Count; i++) 
            {
                if (keys[i] > removedIndex) keys[i]--;
            }
        }
    }
}

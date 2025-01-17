using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Runtime.CompilerServices;

namespace AMOS_V4
{
    public class Quiz
    {
        public Quiz() 
        {
            Lectures = new Files();
        }

        //private List <string> LecturePaths = new List<string>();

        public Lecture ?SelectedLecture;
        public Files Lectures;

        private QuizMode mode = QuizMode.Normal;

        //string selLecturePath = string.Empty;
        int selLectureIndx = -1;
        int selQuestionIndx = -1;
        int selImageIndx = -1;

        Random random = new Random();
        public Bitmap? Image
        {
            get 
            {
                if (SelectedLecture == null) return null;
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
            get { if (SelectedLecture == null)return null;
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
            LectureClosed?.Invoke(this, EventArgs.Empty);
        }
        public void RandomChangeSellection()
        {
            if (LectureChanged != null)
                LectureChanged.Invoke(this, EventArgs.Empty);
            if (Lectures.Count < 1) 
            {
                SelectedLecture = Lecture.Empty;
                return;
            }
            selLectureIndx = random.Next(0, Lectures.Count);
            SelectedLecture = Lectures.GetLecture(selLectureIndx,true);
            selQuestionIndx = random.Next(SelectedLecture.Questions.Count);
            selImageIndx = random.Next(SelectedLecture.Questions[selQuestionIndx].ImageNames.Count);
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
        public void SelectLecture(int indx)
        {
            SelectedLecture = Lectures.GetLecture(indx,true);
            selQuestionIndx = 0;
            selImageIndx = 0;
            if (LectureChanged != null)
                LectureChanged.Invoke(this, new EventArgs());
        }
        public void SelectQuestion(int index)
        {
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
            string fileName = Path.GetFileName(path);
            if (fileName != "") {
                if (SelectedLecture.Images.ContainsKey(fileName))
                {
                    int extension = 1;
                    while (SelectedLecture.Images.ContainsKey(fileName+extension))  extension++;
                    fileName += extension;
                }
                SelectedLecture.Images.Add(fileName, resize(new Bitmap(path), 720));
                Question.ImageNames.Add(Path.GetFileName(fileName));
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
    }
    //public class Lectures
    //{
    //    private List<string> paths;
    //    private List<int> keys;
    //    private List<Lecture> loaded;
    //    //private Dictionary<int, Lecture> loaded;

    //    public Lectures() 
    //    {
    //        this.paths = new List<string>();
    //        this.keys = new List<int>();
    //        this.loaded  = new List<Lecture>();
    //        //this.loaded = new Dictionary<int, Lecture>();
    //    }
    //    public void SetNew(Lecture Lecture)
    //    {
    //        loaded.Add(Lecture);
    //        keys.Add(paths.Count-1);
    //        paths.Add("<Nový Soubor>");
    //    }
    //    public void Open(string path)
    //    {
    //        paths.Add(path);
    //    }
    //    public void Close(int  index)
    //    {
    //        if (IsLoaded(index))
    //        {

    //            loaded.RemoveAt(keys[index]);
    //            keys.Remove(index);

    //        }
    //        correction(index);
    //        paths.RemoveAt(index);
    //    }
    //    public void Load(int index)
    //    {
    //        loaded.Add(new Lecture(paths[index]));
    //        keys.Add(index);
    //    }
    //    public int Count { get => paths.Count(); }
    //    public string LastPath { get => paths.Last(); }
    //    public string FirstPath { get => paths.First(); }
    //    public bool IsEmpty { get => paths.Count() < 1; }
    //    public string GetName(int index) {
    //        if (paths[index].Contains("<")) return paths[index].Substring(1, paths[index].Length - 2);
    //        return Path.GetFileNameWithoutExtension(paths[index]);
    //    }
    //    public Lecture GetLecture(int index)
    //    {
    //        if (IsLoaded(index)) return this.loaded[index];
    //        else
    //        {
    //            Load(index);
    //            return this.loaded[index];
    //        }
    //    }
    //    public bool IsLoaded(int index) => keys.Contains(index);
    //    public bool HavePath(int index) => !paths[index].Contains('<');
    //    public void Save(int index) 
    //    {
    //        if (!IsLoaded(index)) return;

    //    }//WIP
    //    public void SetPath(int index, string path)
    //    {
    //        paths[index] = path;
    //    }
    //    private void correction(int removedIndex)
    //    {
    //        for (int i = 0;i<keys.Count;i++) {
    //            if (keys[i] > removedIndex) keys[i]--;
    //        }
    //    }

    //}
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

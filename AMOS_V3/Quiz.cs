using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace AMOS_V4
{
    internal class Quiz
    {
        public List <Lecture> Lectures = new List<Lecture>();

        int selLectureIndx =-1;
        int selQuestionIndx =-1;
        int selImageIndx = -1;

        Random random = new Random();
        public string Image
        {
            get { return Lectures[selLectureIndx].Questions[selQuestionIndx].Images[selImageIndx]; }
            set { Lectures[selLectureIndx].Questions[selQuestionIndx].Images[selImageIndx] = value; }
        }
        public Question Question {
            get { return Lectures[selLectureIndx].Questions[selQuestionIndx]; }
            set { Lectures[selLectureIndx].Questions[selQuestionIndx] = value; }
        }

        public void OpenLecture(string path)
        {
            Lectures.Add(new Lecture(path));
        }
        public void AddLecture(Lecture lecture)
        {
            Lectures.Add(lecture);
        }
        public void CloseLecture(int indx) {
            Lectures.RemoveAt(indx);
        }
        public void RandomChangeSellection()
        {
            selLectureIndx = random.Next(Lectures.Count());
            selQuestionIndx = random.Next(Lectures[selLectureIndx].Questions.Count());
            selImageIndx = random.Next(Lectures[selLectureIndx].Questions[selQuestionIndx].Images.Count());
        }
        public void SellectNextQuestion() {
            if (selQuestionIndx < Lectures[selLectureIndx].Questions.Count())
            {
                selQuestionIndx++;
                selImageIndx = random.Next(Lectures[selLectureIndx].Questions[selQuestionIndx].Images.Count());
            }
        }
        public void SellectPreviousQuestion() {
            if (selQuestionIndx > 0)
            {
                selQuestionIndx--;
                selImageIndx = random.Next(Lectures[selLectureIndx].Questions[selQuestionIndx].Images.Count());
            }
        }
        public void SelectLecture(int indx)
        {
            selLectureIndx = indx;
            selQuestionIndx = random.Next(Lectures[selLectureIndx].Questions.Count());
            selImageIndx = random.Next(Lectures[selLectureIndx].Questions[selQuestionIndx].Images.Count());
        }
    }
}

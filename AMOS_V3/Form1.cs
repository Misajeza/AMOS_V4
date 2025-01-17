using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMOS_V4
{
    public partial class Form1 : Form
    {

        Quiz quiz = new Quiz();
        bool empty = true;

        int quizCardWidth = (int)Math.Round(Screen.PrimaryScreen.WorkingArea.Height * 0.53);
        int quizCardHeight = (int)Math.Round(Screen.PrimaryScreen.WorkingArea.Height * 0.70);
        int minSidePanelWidth = Screen.PrimaryScreen.Bounds.Width / 15;
        int maxSidePanelWidth = Screen.PrimaryScreen.Bounds.Width / 6;

        public void ChckAreLectures(object sender, EventArgs e)
        {
            if (quiz.Lectures.Count < 1)
            {
                Controls.Remove(quizCard1);
            }
            else { quizCard1.Show(); }
        }
        public Form1()
        { 
            InitializeComponent();
            MouseClick += clickSomewhere;
            quizCard1.Quiz = quiz;
            Controls.Remove(quizCard1);
            sidePanel1.quiz = quiz;
            openFileDialog1.Multiselect = true;
            this.ClientSize = new Size(quizCardWidth + minSidePanelWidth, quizCardHeight);
            this.MinimumSize = this.Size;
            sidePanel1.Load += sidePanel1_Load;
            quiz.LectureClosed += ChckAreLectures;
            quiz.LectureOpened += ChckAreLectures;
            quiz.LectureChanged += SelectionChanged;
            quizCard1.EditorImageBox.button4.Click += addImage;
            openFileDialog1.Filter  = "|*.amos|*.AMOS|*.Amos";
            openImageDialog.Filter  = "|*.jpg|*.JPG|*.png";
            //quizCard1 = new QuizCard(quiz);
            //sidePanel1 = new SidePanel(quiz);
        }
        public void ChangeCard()
        {
            switch (quiz.Mode)
            {
                case (Quiz.QuizMode.Normal):
                    quiz.RandomChangeSellection();
                    quizCard1.Refresh();
                    break;

                case (Quiz.QuizMode.Edit):
                    quiz.RandomChangeSellection();
                    quizCard1.Refresh();
                    break;
            }

        }
        //Sidepanel Events
        #region Sidepanel Events
        private void sidePanel1_Load(object sender, EventArgs e)
        {
            sidePanel1.Width = 0;
            sidePanel1.MaximumSize = new Size(maxSidePanelWidth, Screen.PrimaryScreen.Bounds.Height);
            sidePanel1.MinimumSize = new Size(minSidePanelWidth, 0);
            sidePanel1.button1.Click += OpenButtonOnClick;
            sidePanel1.button1.Text = "Otevřít";
            sidePanel1.button2.Click += EditButtonOnClick;
        }
        private void OpenButtonOnClick(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileNames[0] == "") return;

            string[] paths = openFileDialog1.FileNames;
            bool isEmpty = quiz.Lectures.IsEmpty;
            for (int i = 0; i < paths.Length; i++)
            {
                quiz.OpenLecture(paths[i]);
                sidePanel1.Refresh();
            }

            if (isEmpty)
            {
                this.Controls.Add(quizCard1);
                ChangeCard();
            }
        }
        private void EditButtonOnClick(object sender, EventArgs e)
        {
            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                quiz.Mode = Quiz.QuizMode.Normal;
                sidePanel1.button2.Text = "Upravit";
                quizCard1.button1.Click -= AddLine_Click;
                quizCard1.button1.Click += ChckAnswersButton_Click;
            }
            else
            {
                quiz.Mode = Quiz.QuizMode.Edit;
                sidePanel1.button2.Text = "Hotovo";
                quizCard1.button1.Click -= ChckAnswersButton_Click;
                quizCard1.button1.Click += AddLine_Click;
            }
            sidePanel1.Refresh();
            quizCard1.Refresh();

        }
        #endregion
        //QuestionCard Events
        #region QuestionCard Events
        private void QuizCard_Load(object sender, EventArgs e)
        {
            quizCard1.NewLineButton.Click += AddLine_Click;
            if (quiz.Mode == Quiz.QuizMode.Edit)
                quizCard1.button1.Click += AddLine_Click;
            else
                quizCard1.button1.Click += ChckAnswersButton_Click;
        }
        private void ChckAnswersButton_Click(object sender, EventArgs e)
        {
            if (quizCard1.CheckAnswers()) ChangeCard();
        }
        private void AddLine_Click(object sender, EventArgs e)
        {
            quiz.Question.AddLine();
            quizCard1.Refresh();
        }
        private void SelectionChanged(object sender, EventArgs e)
        {
            if (!Controls.Contains(quizCard1) && !quiz.Lectures.IsEmpty) Controls.Add(quizCard1);
            quizCard1.Refresh();
        }
        private void addImage(object sender, EventArgs e)
        {
            openImageDialog.ShowDialog();
            string path = openImageDialog.FileName;
            quiz.AddImage(path);
            quiz.SelectImage(quiz.Question.ImageNames.Count - 1);
            quizCard1.EditorImageBox.pictureBox1.Image = quiz.Image;
            quizCard1.ChckImageButtons();
        }
        private void clickSomewhere(object sender, EventArgs e) 
        { 
            ActiveControl = null;
            
        }
        #endregion
        //Form1 Events
        #region Form1 Events
        private void Form1_Load(object sender, EventArgs e)
        {
            if (quizCard1 != null)
            {
                quizCard1.Size = new Size(quizCardWidth, quizCardHeight);
                quizCard1.Location = new Point(minSidePanelWidth, 0);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (quizCard1 != null)
                sidePanel1.Width = quizCard1.Location.X;
        }
        #endregion
    }
}

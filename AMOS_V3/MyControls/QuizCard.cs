
using AMOS_V4.MyControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMOS_V4
{
    public partial class QuizCard : UserControl
    {
        public Quiz quiz;
        public Quiz Quiz { get { return quiz; } set { quiz = value; value.QuestionChanged += refreshOnEvent; } }
        public Button NewLineButton = new Button();
        public CardManagerBox CardManagerBox = new CardManagerBox();
        public EditorImageBox EditorImageBox = new EditorImageBox();
        List<QuizLine> quizLines = new List<QuizLine>();
        List<EditQuizLine> editQuizLines = new List<EditQuizLine>();
        public QuizCard(Quiz quiz)
        {
            this.quiz = quiz;
            InitializeComponent();
        }
        public QuizCard()
        {
            //this.quiz = quiz;
            InitializeComponent();
            NewLineButton.Text = "Nový řádek";
            NewLineButton.BackColor = Color.White;
            NewLineButton.ForeColor = Color.Black;
            NewLineButton.UseVisualStyleBackColor = true;
            CardManagerBox.TabIndex = 2;
            CardManagerBox.Dock = DockStyle.Fill;
            CardManagerBox.button1.Click += moveLeft;
            CardManagerBox.button2.Click += moveRight;
            CardManagerBox.button3.Click += newQestion;
            EditorImageBox.button2.Click += moveImageLeft;
            EditorImageBox.button3.Click += moveImageRight;
            EditorImageBox.button5.Click += removeImage;
            EditorImageBox.Dock = DockStyle.Fill;
        }
        public new void Refresh()
        {
            flowLayoutPanel1.Controls.Clear();
            quizLines.Clear();
            editQuizLines.Clear();
            this.pictureBox1.Image = quiz.Image;
            this.EditorImageBox.pictureBox1.Image = quiz.Image;
            if (quiz.Question == null) { return; }
            for (int i = 0; i < quiz.Question.QuestionLines.Count(); i++)
            {
                AddLine(quiz.Question.QuestionLines[i]);
            }
            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                NewLineButton.Width = flowLayoutPanel1.ClientSize.Width - 3;
                NewLineButton.Height = flowLayoutPanel1.ClientSize.Height / 10;
                flowLayoutPanel1.Controls.Add(NewLineButton);
                tableLayoutPanel1.Controls.Remove(button1);
                tableLayoutPanel1.Controls.Add(CardManagerBox, 0, 2);
                if (quiz.IsQuestionFirst) { CardManagerBox.button1.Hide(); }
                else { CardManagerBox.button1.Show(); }
                if (quiz.IsQuestionLast) { CardManagerBox.button2.Hide(); }
                else { CardManagerBox.button2.Show(); }
                flowLayoutPanel1.Controls.Add(NewLineButton);
                tableLayoutPanel1.Controls.Add(EditorImageBox, 0, 0);
                tableLayoutPanel1.Controls.Remove(pictureBox1);
            }
            else
            {
                tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
                tableLayoutPanel1.Controls.Remove(EditorImageBox);
                tableLayoutPanel1.Controls.Remove(CardManagerBox);
                tableLayoutPanel1.Controls.Add(button1, 0, 2);
            }
            ChckImageButtons();
        }
        public void moveLeft(object sender, EventArgs e)
        {
            quiz.SellectPreviousQuestion();
        }
        public void moveRight(object sender, EventArgs e)
        {
            quiz.SellectNextQuestion();
        }
        public void newQestion(object sender, EventArgs e)
        {
            quiz.CreateNewQuestion();
        }
        public void AddLine(QuestionLine questionLine)
        {
            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                EditQuizLine editQuizLine = new EditQuizLine(questionLine);
                editQuizLine.Width = flowLayoutPanel1.ClientSize.Width;
                editQuizLines.Add(editQuizLine);
                flowLayoutPanel1.Controls.Add(editQuizLine);
            }
            else
            {
                QuizLine quizline = new QuizLine(questionLine);
                quizline.Width = flowLayoutPanel1.ClientSize.Width;
                quizLines.Add(quizline);
                flowLayoutPanel1.Controls.Add(quizline);
            }


        }
        private void moveImageLeft(object sender, EventArgs e)
        {
            quiz.SellectPreviousImage();
            ChckImageButtons();
        }
        private void moveImageRight(object sender, EventArgs e)
        {
            quiz.SellectNextImage();
            ChckImageButtons();
        }
        private void refreshOnEvent(object sender, EventArgs e) => Refresh();
        public void removeImage(object sender, EventArgs e)
        {
            quiz.RemoveImage(quiz.SelectedImageIndex);
            ChckImageButtons();
        }
        public bool CheckAnswers()
        {
            for (int i = 0; i < quizLines.Count(); i++)
            {
                if (!quizLines[i].IsRight()) return false;

            }
            return true;
        }
        public void ChckImageButtons()
        {
            this.EditorImageBox.pictureBox1.Image = quiz.Image;
            this.EditorImageBox.label1.Text = $"{(quiz.SelectedImageIndex + 1)}/{quiz.Question.ImageNames.Count}";
            if (quiz.IsImageFirst) { EditorImageBox.button2.Hide(); }
            else { EditorImageBox.button2.Show(); }
            if (quiz.IsImageLast) { EditorImageBox.button3.Hide(); }
            else { EditorImageBox.button3.Show(); }
        }
    }
}

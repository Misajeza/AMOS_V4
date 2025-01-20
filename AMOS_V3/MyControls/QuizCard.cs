
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
        public EventHandler ChckAnswers;
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
            betterTextBox2.Leave += markTextFinish;
            EditorImageBox.Dock = DockStyle.Fill;
        }
        public new void Refresh()
        {
            //MessageBox.Show("bla");
            flowLayoutPanel1.Controls.Clear();
            quizLines.Clear();
            editQuizLines.Clear();
            this.pictureBox1.Image = quiz.Image;
            this.EditorImageBox.pictureBox1.Image = quiz.Image;
            if (quiz.Question == null)
            {
                Hide();
                return;
            }
            else Show();
            label1.Text = $"{quiz.Lectures.GetName(quiz.SelectedLectureIndex)} - {quiz.SelectedQuestionIndex + 1}/{quiz.SelectedLecture.Questions.Count}";
            label2.Text = $"Známka: {Math.Round(quiz.Question.Mark, 3)}";
            betterTextBox2.Texts = $"{Math.Round(quiz.Question.Mark, 3)}";
            for (int i = 0; i < quiz.Question.QuestionLines.Count(); i++)
            {
                AddLine(quiz.Question.QuestionLines[i]);
            }
            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                tableLayoutPanel2.Controls.Add(button2, 3, 0);
                NewLineButton.Width = flowLayoutPanel1.ClientSize.Width - 3;
                NewLineButton.Height = flowLayoutPanel1.ClientSize.Height / 10;
                flowLayoutPanel1.Controls.Add(NewLineButton);
                tableLayoutPanel1.Controls.Remove(button1);
                tableLayoutPanel1.Controls.Add(CardManagerBox, 0, 3);
                if (quiz.IsQuestionFirst) { CardManagerBox.button1.Hide(); }
                else { CardManagerBox.button1.Show(); }
                if (quiz.IsQuestionLast) { CardManagerBox.button2.Hide(); }
                else { CardManagerBox.button2.Show(); }
                flowLayoutPanel1.Controls.Add(NewLineButton);
                tableLayoutPanel1.Controls.Add(EditorImageBox, 0, 1);
                tableLayoutPanel1.Controls.Remove(pictureBox1);
                tableLayoutPanel2.Controls.Add(betterTextBox2, 2, 0);
                tableLayoutPanel2.Controls.Remove(label2);

            }
            else
            {
                if (flowLayoutPanel1.Controls.Count > 0)
                    (flowLayoutPanel1.Controls[0] as QuizLine).Focus();
                tableLayoutPanel2.Controls.Add(label2, 2, 0);
                tableLayoutPanel2.Controls.Remove(betterTextBox2);
                tableLayoutPanel2.Controls.Remove(button2);
                tableLayoutPanel1.Controls.Add(pictureBox1, 0, 1);
                tableLayoutPanel1.Controls.Remove(EditorImageBox);
                tableLayoutPanel1.Controls.Remove(CardManagerBox);
                tableLayoutPanel1.Controls.Add(button1, 0, 3);
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
                editQuizLine.Close_Click += CloseLine;
                editQuizLine.Width = flowLayoutPanel1.ClientSize.Width;
                editQuizLines.Add(editQuizLine);
                flowLayoutPanel1.Controls.Add(editQuizLine);
            }
            else
            {
                QuizLine quizline = new QuizLine(questionLine);
                quizline.keyDown += quizLineKeyEvents;
                quizline.Width = flowLayoutPanel1.ClientSize.Width;
                quizLines.Add(quizline);
                flowLayoutPanel1.Controls.Add(quizline);
            }


        }
        private void quizLineKeyEvents(object sender, KeyEventArgs e)
        {
            int index = flowLayoutPanel1.Controls.IndexOf(sender as QuizLine);
            if (e.KeyCode == Keys.Enter)
            {
                if (index + 1 >= flowLayoutPanel1.Controls.Count) ChckAnswers.Invoke(this, new EventArgs());
                else
                {
                    QuizLine quizl = (QuizLine)flowLayoutPanel1.Controls[index + 1];
                    quizl.Focus();
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        public void CloseLine(object sender, EventArgs e)
        {
            EditQuizLine EQLsender = sender as EditQuizLine;
            quiz.Question.RemoveLine(EQLsender.TabIndex);
            Refresh();
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
            int sum = 0;
            bool Correct = true;
            for (int i = 0; i < quizLines.Count(); i++)
            {
                bool isRight = quizLines[i].IsRight();
                sum += quizLines[i].Mark;
                if (Correct) Correct = isRight;
            }
            if (Correct)
                quiz.Question.Mark = (quiz.Question.Mark * quiz.ConservationValue + sum) / (quiz.ConservationValue + quizLines.Count());
            return Correct;
        }
        public void ChckImageButtons()
        {
            this.EditorImageBox.pictureBox1.Image = quiz.Image;
            this.EditorImageBox.label1.Text = $"{(quiz.SelectedImageIndex + 1)}/{quiz.Question.ImageNames.Count}";
            if (quiz.IsImageFirst) { EditorImageBox.button2.Hide(); }
            else { EditorImageBox.button2.Show(); }
            if (quiz.IsImageLast) { EditorImageBox.button3.Hide(); }
            else { EditorImageBox.button3.Show(); }
            if (quiz.Question.ImageNames.Count == 0) { EditorImageBox.button5.Hide(); EditorImageBox.label1.Hide(); }
            else { EditorImageBox.button5.Show(); EditorImageBox.label1.Show(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            quiz.RemoveQuestion(quiz.Question);
            Refresh();
        }
        private void markTextFinish(object sender, EventArgs e)
        {
            try { quiz.Question.Mark = float.Parse(betterTextBox2.Texts); }
            catch
            {
                betterTextBox2.Texts = $"{Math.Round(quiz.Question.Mark, 3)}";
            }

        }
    }
}

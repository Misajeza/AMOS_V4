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
    public partial class QuizLine : UserControl
    {
        QuestionLine questionLine;
        Random rand = new Random();
        public KeyEventHandler keyDown;
        private int mark = 1;
        private bool helpUsed = false;
        public int Mark { get => mark; set => mark = value; }

        public QuestionLine QuestionLine { get => questionLine; set => questionLine = value; }

        public QuizLine(QuestionLine questionLine)
        {
            this.questionLine = questionLine;
            InitializeComponent();
            label1.Text = questionLine.Query;
        }
        public bool IsRight()
        {
            if (questionLine.CharSize)
            {
                if (QuestionLine.Answer.Contains(betterTextBox1.Texts.Trim())) return true;
                if (mark < 5) mark++;
                return false;
            }
            else
            {
                bool isRight = false;
                foreach (string answer in questionLine.Answer)
                {
                    isRight = answer.ToLower() == betterTextBox1.Texts.Trim().ToLower();
                    if (isRight) break;
                }
                if (!isRight && mark < 5) mark++;
                return isRight;
            }
            return QuestionLine.Answer.Contains(betterTextBox1.Texts.Trim());
        }
        public void Help()
        {
            if (QuestionLine.Answer.Count() > 0)
                betterTextBox1.Texts = QuestionLine.Answer[rand.Next(QuestionLine.Answer.Count())];
            if (!helpUsed)
            {
                mark = 5;
                helpUsed = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Help();
        }

        private void betterTextBox1__KeyDown(object sender, KeyEventArgs e)
        {
            if (keyDown!=null)
                keyDown.Invoke(this, e);
        }
        public void Focus()
        {
            betterTextBox1.Focus();
        }
    }
}

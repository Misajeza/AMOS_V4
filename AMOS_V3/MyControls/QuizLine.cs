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

        public QuestionLine QuestionLine { get => questionLine; set => questionLine = value; }

        public QuizLine(QuestionLine questionLine)
        {
            this.questionLine = questionLine;
            InitializeComponent();
            label1.Text = questionLine.Query;
        }
        public bool IsRight()
        {
            return QuestionLine.Answer.Contains(betterTextBox1.Texts.Trim());
        }
        public void Help()
        {
            if (QuestionLine.Answer.Count() > 0)
                betterTextBox1.Texts = QuestionLine.Answer[rand.Next(QuestionLine.Answer.Count())];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Help();
        }
    }
}

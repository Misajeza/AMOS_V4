using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace AMOS_V4.MyControls
{
    public partial class EditQuizLine : UserControl
    {
        QuestionLine questionLine;
        Random rand = new Random();
        public EventHandler Close_Click;
        public EditQuizLine(QuestionLine questionLine)
        {
            this.questionLine = questionLine;
            InitializeComponent();
            betterTextBox2.Texts = questionLine.Query;
            betterTextBox2._TextChanged += saveQuery;
            betterTextBox1._TextChanged += saveAnswers;
            if (questionLine.Answer.Count() > 0)
                betterTextBox1.Texts = questionLine.Answer[rand.Next(questionLine.Answer.Count())];
        }
        public void saveQuery(object sender, EventArgs e)
        {
            questionLine.Query = betterTextBox2.Texts;
        }
        public void saveAnswers(object sender, EventArgs e)
        {

            string[] inp = betterTextBox1.Texts.Split(';');
            questionLine.Answer.Clear();
            for (int i = 0; i < inp.Length; i++)
            {
                questionLine.Answer.Add(inp[i].Trim());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Close_Click != null)
                Close_Click.Invoke(this, EventArgs.Empty);
        }
    }
}

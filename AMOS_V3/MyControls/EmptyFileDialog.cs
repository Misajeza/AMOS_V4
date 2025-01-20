using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMOS_V4.MyControls
{
    public partial class EmptyFileDialog : UserControl
    {
        public Quiz quiz;
        public EmptyFileDialog()
        {
            InitializeComponent();

        }
        public new void Show()
        {
            base.Show();
            if (quiz.SelectedLectureIndex != -1)
                label1.Text = $"Soubor {quiz.Lectures.GetName(quiz.SelectedLectureIndex)} je prázdný";
            else label1.Text = "Soubor je prázdný";

            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                button1.Text = "Přidat kartu";
            }
            else
            {
                button1.Text = "Vybrat jiný";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                quiz.CreateNewQuestion();
            }
            else
            {
                quiz.RandomChangeSellection();
            }
        }
    }
}

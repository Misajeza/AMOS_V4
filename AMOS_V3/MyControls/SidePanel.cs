using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMOS_V4
{
    public partial class SidePanel : UserControl
    {
        public Quiz quiz;
        List<FileBox> files = new List<FileBox>();
        List<EditFileBox> editfiles = new List<EditFileBox>();
        public Button NewFileButton = new Button();
        public BetterTextBox FilenameEntry = new BetterTextBox();
        bool FileToBeNamed = false;
        public SidePanel(Quiz quiz)
        {
            InitializeComponent();
            this.quiz = quiz;
            button2.Text = "Upravit";
        }
        public SidePanel()
        {
            InitializeComponent();
            button2.Text = "Upravit";
            NewFileButton.BackColor = Color.White;
            NewFileButton.Text = "Nový";
            NewFileButton.Size = new Size(this.Width, Screen.PrimaryScreen.WorkingArea.Height / 25);

            FilenameEntry.BackColor = Color.White;
            FilenameEntry.Text = "Nový";
            FilenameEntry.Size = new Size(this.Width, Screen.PrimaryScreen.WorkingArea.Height / 25);
            NewFileButton.Click += newFile;
            NewFileButton.Dock = DockStyle.Top;
            FilenameEntry.Leave += setFileName;
            FilenameEntry._KeyDown += nameEntry_KeyPressed;
            flowLayoutPanel1.MouseClick += clickSomewhere;

        }
        public void AddLecture(int index, string Name)
        {
            //for (int i = 0; i < quiz.LecturePaths.Count(); i++)
            //{

            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                EditFileBox box = new EditFileBox(quiz, index);
                editfiles.Add(box);
                //box.Dock = DockStyle.Fill;
                box.Size = new Size(this.Width, Screen.PrimaryScreen.WorkingArea.Height / 25);
                box.button1.Text = Name;
                box._CloseButtonClick += CloseButtonClick;
                box._SelectButtonClick += SelectLecture;
                flowLayoutPanel1.Controls.Add(box);
            }
            else
            {
                FileBox box = new FileBox(quiz);
                files.Add(box);
                //box.Dock = DockStyle.Fill;
                box.Size = new Size(this.Width, Screen.PrimaryScreen.WorkingArea.Height / 25);
                box.label1.Text = Name;
                box._CloseButtonClick += CloseButtonClick;
                flowLayoutPanel1.Controls.Add(box);
            }
        }
        public new void Refresh()
        {
            files.Clear();
            editfiles.Clear();
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < quiz.Lectures.Count; i++)
            {
                if (FileToBeNamed&&i== quiz.Lectures.Count-1)
                    break;
                AddLecture(i, quiz.Lectures.GetName(i));
            }
            if (quiz.Mode == Quiz.QuizMode.Edit)
            {
                if (FileToBeNamed)
                    flowLayoutPanel1.Controls.Add(FilenameEntry);
                else 
                    flowLayoutPanel1.Controls.Add(NewFileButton);
            }
            flowLayoutPanel1_Resize(this, new EventArgs());
        }
        private void SelectLecture(object sender, EventArgs e)
        {
            quiz.SelectLecture(((EditFileBox)sender).LectureIndex);
        }
        private void CloseButtonClick(object sender, EventArgs e)
        {
            int index = flowLayoutPanel1.Controls.IndexOf((Control)sender);
            flowLayoutPanel1.Controls.Remove((Control)sender);
            bool isSelected = quiz.SelectedLectureIndex != index;
            quiz.CloseLecture(index);
            if (isSelected)
            {
                quiz.RandomChangeSellection();
            }
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                if (flowLayoutPanel1.Controls[i].GetType() == typeof(EditFileBox))
                {
                    EditFileBox box = (EditFileBox)flowLayoutPanel1.Controls[i];
                    box.IndexCorrection(index);
                }
            }
        }
        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                flowLayoutPanel1.Controls[i].Size = new Size(this.Width, Screen.PrimaryScreen.WorkingArea.Height / 25);
            }
        }
        private void newFile(object sender, EventArgs e)
        {
            quiz.NewLecture();
            FileToBeNamed = true;
            Refresh();
            disableAllFilesButtons();
            FilenameEntry.Focus();
        }
        private void setFileName(object sender, EventArgs e) 
        {
            quiz.Lectures.SetName(quiz.Lectures.Count-1,FilenameEntry.Texts);
            FileToBeNamed= false;
            FilenameEntry.Texts = string.Empty;
            enableAllFilesButtons();
            Refresh();
        }
        private void nameEntry_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                ActiveControl = null;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void clickSomewhere(object sender, EventArgs e)
        {
            ActiveControl = null;
        }
        private void disableAllFilesButtons()
        {
            foreach (Control child in flowLayoutPanel1.Controls) 
            {
                if (child!=FilenameEntry) child.Enabled = false;
            }
        }
        private void enableAllFilesButtons()
        {
            foreach (Control child in flowLayoutPanel1.Controls)
            {
                if (child != FilenameEntry) child.Enabled = true;
            }
        }
    }
}

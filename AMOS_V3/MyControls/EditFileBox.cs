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
    public partial class EditFileBox : UserControl
    {
        Quiz quiz;
        public int LectureIndex;
        public EditFileBox(Quiz quiz,int index)
        {
            InitializeComponent();
            LectureIndex = index;
            this.quiz = quiz;
            //YourCustomButton button = sender as YourCustomButton;
        }
        public event EventHandler? _CloseButtonClick;
        public event EventHandler? _SelectButtonClick;

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (_CloseButtonClick != null) _CloseButtonClick.Invoke(this, e);
        }

        private void SelectButton_Click_1(object sender, EventArgs e)
        {
            if (_SelectButtonClick != null) _SelectButtonClick.Invoke(this, e);
        }
        public void IndexCorrection(int removedIndex)
        {
            if (LectureIndex>removedIndex) LectureIndex--;
        }
    }
}

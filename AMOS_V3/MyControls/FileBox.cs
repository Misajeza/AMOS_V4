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
    public partial class FileBox : UserControl
    {
        Quiz quiz;
        public FileBox(Quiz quiz)
        {
            InitializeComponent();
            this.quiz = quiz;
            //YourCustomButton button = sender as YourCustomButton;
        }
        public event EventHandler? _CloseButtonClick;

        private void button1_Click(object sender, EventArgs e)
        {
            if (_CloseButtonClick != null) _CloseButtonClick.Invoke(this, e);
        }
    }
}

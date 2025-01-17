using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMOS_V4
{
    public partial class BetterTextBox : UserControl
    {
        private Color borderColor = Color.Black;
        private int borderSize = 2;
        private bool underLinedStyle = false;
        private Color focusBorderColor = Color.White;
        public BetterTextBox()
        {
            InitializeComponent();
        }
        public event EventHandler? _TextChanged;
        public event KeyEventHandler? _KeyDown;
        public Color FocusBorderColor { get => focusBorderColor; set => focusBorderColor = value; }
        public Color BorderColor { get => borderColor; set { borderColor = value; this.Invalidate(); } }
        public int BorderSize { get => borderSize; set { borderSize = value; this.Invalidate(); } }
        public bool UnderLinedStyle { get => underLinedStyle; set { underLinedStyle = value; this.Invalidate(); } }
        public bool PasswordChar
        {
            get { return textBox1.UseSystemPasswordChar; }
            set { textBox1.UseSystemPasswordChar = value; }
        }
        public bool Multiline
        {
            get => textBox1.Multiline;
            set => textBox1.Multiline = value;
        }
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (DesignMode) UpdateControlHeight();
            }
        }
        public HorizontalAlignment TextAlign
        {
            get => textBox1.TextAlign;
            set => textBox1.TextAlign = value;
        }
        public string Texts
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            Color penColor;
            if (textBox1.Focused) penColor = focusBorderColor;
            else penColor = borderColor;
            using (Pen penBorder = new Pen(penColor, borderSize))
            {
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                if (underLinedStyle)
                    graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                else graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
                UpdateControlHeight();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }
        public void Focus()
        {
            textBox1.Focus();
        }
        private void UpdateControlHeight()
        {
            if (textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.Size = new Size(0, txtHeight);
                textBox1.Multiline = false;

                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this._TextChanged != null)
                this._TextChanged.Invoke(this, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this._KeyDown != null && textBox1.Focused)
                this._KeyDown.Invoke(this, e);
        }
    }
}

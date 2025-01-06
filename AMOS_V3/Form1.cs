namespace AMOS_V4
{
    public partial class Form1 : Form
    {
        Quiz Quiz = new Quiz();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            //this.MinimumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //this.AutoSize = true;
            //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
        protected override void OnResize(EventArgs e)
        {
            panel2.Width = (int)(this.Height * 0.6);
            panel2.Height = (int)(this.Height * 0.8);
            panel2.Location = new Point((this.Width / 2) - (panel2.Width / 2) + (panel1.Width / 2), (this.Height - panel2.Height) /4);
            splitContainer2.Panel1.Padding = new Padding((int)(panel2.Height / 50));
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            panel1.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height);
            panel1.MinimumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 20, Screen.PrimaryScreen.Bounds.Height);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            button1.MaximumSize = new System.Drawing.Size(panel1.MaximumSize.Width, Screen.PrimaryScreen.Bounds.Height / 20);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
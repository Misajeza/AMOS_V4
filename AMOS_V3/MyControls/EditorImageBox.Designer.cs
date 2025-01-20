namespace AMOS_V4.MyControls
{
    partial class EditorImageBox
    {
        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button5
            // 
            button5.Dock = DockStyle.Fill;
            button5.Location = new Point(685, 0);
            button5.Margin = new Padding(0);
            button5.Name = "button5";
            button5.Size = new Size(79, 77);
            button5.TabIndex = 3;
            button5.Text = "X";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Dock = DockStyle.Fill;
            button4.Location = new Point(488, 0);
            button4.Margin = new Padding(0);
            button4.Name = "button4";
            button4.Size = new Size(197, 77);
            button4.TabIndex = 2;
            button4.Text = "Přidat obrázek";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(76, 0);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Size = new Size(76, 77);
            button3.TabIndex = 1;
            button3.Text = "->";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(0, 0);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(76, 77);
            button2.TabIndex = 0;
            button2.Text = "<-";
            button2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.0150986F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.0150986F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.9790573F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.7853413F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.0150986F));
            tableLayoutPanel1.Controls.Add(button2, 0, 0);
            tableLayoutPanel1.Controls.Add(button3, 1, 0);
            tableLayoutPanel1.Controls.Add(button4, 3, 0);
            tableLayoutPanel1.Controls.Add(button5, 4, 0);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.1315794F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 89.86842F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(764, 760);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            tableLayoutPanel1.SetColumnSpan(pictureBox1, 5);
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(3, 80);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(758, 677);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Left;
            label1.Location = new Point(155, 0);
            label1.Name = "label1";
            label1.Size = new Size(59, 77);
            label1.TabIndex = 5;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EditorImageBox
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(0);
            MinimumSize = new Size(20, 20);
            Name = "EditorImageBox";
            Size = new Size(764, 760);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        public Button button5;
        public Button button4;
        public Button button3;
        public Button button2;
        private TableLayoutPanel tableLayoutPanel1;
        public PictureBox pictureBox1;
        public Label label1;
    }
}

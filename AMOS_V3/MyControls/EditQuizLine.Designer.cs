namespace AMOS_V4.MyControls
{
    partial class EditQuizLine
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
            tableLayoutPanel1 = new TableLayoutPanel();
            button1 = new Button();
            betterTextBox1 = new BetterTextBox();
            betterTextBox2 = new BetterTextBox();
            button2 = new Button();
            button3 = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.Controls.Add(button1, 2, 0);
            tableLayoutPanel1.Controls.Add(betterTextBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(betterTextBox2, 0, 0);
            tableLayoutPanel1.Controls.Add(button2, 3, 0);
            tableLayoutPanel1.Controls.Add(button3, 4, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(738, 56);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // button1
            // 
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(664, 0);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(25, 56);
            button1.TabIndex = 0;
            button1.Text = "->\\n<-";
            button1.UseVisualStyleBackColor = true;
            // 
            // betterTextBox1
            // 
            betterTextBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            betterTextBox1.BackColor = SystemColors.Control;
            betterTextBox1.BorderColor = Color.Gold;
            betterTextBox1.BorderSize = 5;
            betterTextBox1.FocusBorderColor = Color.DarkOrchid;
            betterTextBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            betterTextBox1.Location = new Point(335, 8);
            betterTextBox1.Multiline = false;
            betterTextBox1.Name = "betterTextBox1";
            betterTextBox1.Padding = new Padding(7);
            betterTextBox1.PasswordChar = false;
            betterTextBox1.Size = new Size(326, 40);
            betterTextBox1.TabIndex = 2;
            betterTextBox1.TextAlign = HorizontalAlignment.Center;
            betterTextBox1.Texts = "";
            betterTextBox1.UnderLinedStyle = true;
            // 
            // betterTextBox2
            // 
            betterTextBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            betterTextBox2.BackColor = SystemColors.Control;
            betterTextBox2.BorderColor = Color.DarkGray;
            betterTextBox2.BorderSize = 5;
            betterTextBox2.FocusBorderColor = Color.IndianRed;
            betterTextBox2.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            betterTextBox2.Location = new Point(3, 8);
            betterTextBox2.Multiline = false;
            betterTextBox2.Name = "betterTextBox2";
            betterTextBox2.Padding = new Padding(7);
            betterTextBox2.PasswordChar = false;
            betterTextBox2.Size = new Size(326, 40);
            betterTextBox2.TabIndex = 3;
            betterTextBox2.TextAlign = HorizontalAlignment.Center;
            betterTextBox2.Texts = "";
            betterTextBox2.UnderLinedStyle = true;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(689, 0);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(25, 56);
            button2.TabIndex = 4;
            button2.Text = "Aa";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(714, 0);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Size = new Size(24, 56);
            button3.TabIndex = 5;
            button3.Text = "X";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // EditQuizLine
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "EditQuizLine";
            Size = new Size(738, 56);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        public Button button1;
        public BetterTextBox betterTextBox1;
        private BetterTextBox betterTextBox2;
        private Button button2;
        private Button button3;
    }
}

﻿namespace AMOS_V4
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog1 = new OpenFileDialog();
            sidePanel1 = new SidePanel();
            quizCard1 = new QuizCard();
            openImageDialog = new OpenFileDialog();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "";
            // 
            // sidePanel1
            // 
            sidePanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            sidePanel1.BackColor = Color.DarkKhaki;
            sidePanel1.Location = new Point(0, -1);
            sidePanel1.Margin = new Padding(4, 5, 4, 5);
            sidePanel1.Name = "sidePanel1";
            sidePanel1.Size = new Size(265, 1039);
            sidePanel1.TabIndex = 0;
            // 
            // quizCard1
            // 
            quizCard1.Anchor = AnchorStyles.None;
            quizCard1.BackColor = Color.DarkGoldenrod;
            quizCard1.Location = new Point(306, -1);
            quizCard1.Name = "quizCard1";
            quizCard1.Size = new Size(810, 1039);
            quizCard1.TabIndex = 1;
            quizCard1.Load += QuizCard_Load;
            // 
            // openImageDialog
            // 
            openImageDialog.FileName = "";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1155, 1034);
            Controls.Add(quizCard1);
            Controls.Add(sidePanel1);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            SizeChanged += Form1_SizeChanged;
            ResumeLayout(false);
        }

        #endregion

        private SidePanel sidePanel1;
        private QuizCard quizCard1;
        private OpenFileDialog openFileDialog1;
        private ToolStrip toolStrip1;
        private OpenFileDialog openImageDialog;
    }
}
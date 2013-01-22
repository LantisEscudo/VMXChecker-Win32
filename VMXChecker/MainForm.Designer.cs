namespace VMXChecker
{
    partial class MainForm
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
            this.FixButton = new System.Windows.Forms.Button();
            this.inFileBox = new System.Windows.Forms.TextBox();
            this.InputFileLabel = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.InputGroup = new System.Windows.Forms.GroupBox();
            this.VideoGroup = new System.Windows.Forms.GroupBox();
            this.VideoInfoLabel = new System.Windows.Forms.Label();
            this.AudioGroup = new System.Windows.Forms.GroupBox();
            this.AudioInfoLabel = new System.Windows.Forms.Label();
            this.openFileDialogInput = new System.Windows.Forms.OpenFileDialog();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.InputGroup.SuspendLayout();
            this.VideoGroup.SuspendLayout();
            this.AudioGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FixButton
            // 
            this.FixButton.Enabled = false;
            this.FixButton.Location = new System.Drawing.Point(235, 238);
            this.FixButton.Name = "FixButton";
            this.FixButton.Size = new System.Drawing.Size(75, 23);
            this.FixButton.TabIndex = 0;
            this.FixButton.Text = "Fix";
            this.FixButton.UseVisualStyleBackColor = true;
            this.FixButton.Click += new System.EventHandler(this.FixButton_Click);
            // 
            // inFileBox
            // 
            this.inFileBox.AllowDrop = true;
            this.inFileBox.Location = new System.Drawing.Point(60, 18);
            this.inFileBox.Name = "inFileBox";
            this.inFileBox.Size = new System.Drawing.Size(370, 20);
            this.inFileBox.TabIndex = 1;
            this.inFileBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.inFileBox_DragDrop);
            this.inFileBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.inFileBox_DragEnter);
            // 
            // InputFileLabel
            // 
            this.InputFileLabel.AutoSize = true;
            this.InputFileLabel.Location = new System.Drawing.Point(6, 21);
            this.InputFileLabel.Name = "InputFileLabel";
            this.InputFileLabel.Size = new System.Drawing.Size(53, 13);
            this.InputFileLabel.TabIndex = 2;
            this.InputFileLabel.Text = "Input File:";
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(436, 16);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 3;
            this.BrowseButton.Text = "Browse...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // InputGroup
            // 
            this.InputGroup.Controls.Add(this.InputFileLabel);
            this.InputGroup.Controls.Add(this.BrowseButton);
            this.InputGroup.Controls.Add(this.inFileBox);
            this.InputGroup.Location = new System.Drawing.Point(15, 12);
            this.InputGroup.Name = "InputGroup";
            this.InputGroup.Size = new System.Drawing.Size(517, 51);
            this.InputGroup.TabIndex = 4;
            this.InputGroup.TabStop = false;
            this.InputGroup.Text = "File";
            // 
            // VideoGroup
            // 
            this.VideoGroup.Controls.Add(this.VideoInfoLabel);
            this.VideoGroup.Location = new System.Drawing.Point(15, 69);
            this.VideoGroup.Name = "VideoGroup";
            this.VideoGroup.Size = new System.Drawing.Size(259, 100);
            this.VideoGroup.TabIndex = 5;
            this.VideoGroup.TabStop = false;
            this.VideoGroup.Text = "Video";
            // 
            // VideoInfoLabel
            // 
            this.VideoInfoLabel.AutoSize = true;
            this.VideoInfoLabel.ForeColor = System.Drawing.Color.Gray;
            this.VideoInfoLabel.Location = new System.Drawing.Point(9, 20);
            this.VideoInfoLabel.Name = "VideoInfoLabel";
            this.VideoInfoLabel.Size = new System.Drawing.Size(90, 13);
            this.VideoInfoLabel.TabIndex = 0;
            this.VideoInfoLabel.Text = "No Video Loaded";
            // 
            // AudioGroup
            // 
            this.AudioGroup.Controls.Add(this.AudioInfoLabel);
            this.AudioGroup.Location = new System.Drawing.Point(280, 69);
            this.AudioGroup.Name = "AudioGroup";
            this.AudioGroup.Size = new System.Drawing.Size(252, 100);
            this.AudioGroup.TabIndex = 6;
            this.AudioGroup.TabStop = false;
            this.AudioGroup.Text = "Audio";
            // 
            // AudioInfoLabel
            // 
            this.AudioInfoLabel.AutoSize = true;
            this.AudioInfoLabel.ForeColor = System.Drawing.Color.Gray;
            this.AudioInfoLabel.Location = new System.Drawing.Point(6, 20);
            this.AudioInfoLabel.Name = "AudioInfoLabel";
            this.AudioInfoLabel.Size = new System.Drawing.Size(90, 13);
            this.AudioInfoLabel.TabIndex = 1;
            this.AudioInfoLabel.Text = "No Audio Loaded";
            // 
            // openFileDialogInput
            // 
            this.openFileDialogInput.Title = "Open File...";
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(-3, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(520, 59);
            this.MessageLabel.TabIndex = 7;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MessageLabel);
            this.panel1.Location = new System.Drawing.Point(15, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 59);
            this.panel1.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 273);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AudioGroup);
            this.Controls.Add(this.VideoGroup);
            this.Controls.Add(this.InputGroup);
            this.Controls.Add(this.FixButton);
            this.Name = "MainForm";
            this.Text = "VMX File Checker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.inFileBox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.inFileBox_DragEnter);
            this.InputGroup.ResumeLayout(false);
            this.InputGroup.PerformLayout();
            this.VideoGroup.ResumeLayout(false);
            this.VideoGroup.PerformLayout();
            this.AudioGroup.ResumeLayout(false);
            this.AudioGroup.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FixButton;
        private System.Windows.Forms.TextBox inFileBox;
        private System.Windows.Forms.Label InputFileLabel;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.GroupBox InputGroup;
        private System.Windows.Forms.GroupBox VideoGroup;
        private System.Windows.Forms.GroupBox AudioGroup;
        private System.Windows.Forms.Label VideoInfoLabel;
        private System.Windows.Forms.Label AudioInfoLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialogInput;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Panel panel1;
    }
}


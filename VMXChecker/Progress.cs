using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VMXChecker
{
    public partial class Progress : Form
    {

        private int frames;
        private int frames_completed = 0;
        private StringBuilder logBuilder = new StringBuilder();
        [DllImport("user32.dll")]
        static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
        Regex lineParse;

        public Progress()
        {
            InitializeComponent();
            frames = Program.main_form.frames;

            frames_completed = 0;
            Program.main_form.Enabled = false;
            Program.main_form.Hide();
            Program.main_form.ShowInTaskbar = false;
            progressBar1.Maximum = frames;

            lineParse = new Regex("frame=\\s*?(\\d+\\.\\d*).*fps=.*?(\\d*)", RegexOptions.None);


        }

        private void Progress_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.main_form.Enabled = true;
            Program.main_form.Show();
            Program.main_form.ShowInTaskbar = true;
            Program.main_form.cancel_encode();
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Progress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (buttonCancel.Text != "Done!")
            {
                if (MessageBox.Show("Are you sure you want to stop the encode?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = true;
            }
            else
            {
                Program.main_form.Encode_Return();
            }
        }

        public void updateProgress(string log)
        {
            if (log.Contains("frame=")) //video progress
            {
                Match m = Regex.Match(log, @"frame=\s*?(\d+).*fps=.*?(\d*\.?\d*)", RegexOptions.None);
                if (m.Success)
                {
                    Console.WriteLine("Matched pattern.");
                    frames_completed = int.Parse(m.Groups[1].Captures[0].Value);
                    if (frames_completed > progressBar1.Maximum)
                    {
                        frames_completed = progressBar1.Maximum;
                    }
                    progressBar1.Value = frames_completed;
                    label2.Text = "Progress: " + frames_completed + "/" + frames;
                    label1.Text = "FPS: " + m.Groups[2].Captures[0].Value;
                    completeLabel.Text = "Complete: " + (int)(frames_completed / frames) + "%";
                    this.Text = "Complete: " + (int)(frames_completed / frames) + "%";
                }
            }
            else if (log.StartsWith("video:")) //encoding finished
            {
                buttonCancel.Text = "Done!";
                this.Text = "Encoding completed!";
                progressBar1.Value = frames;
                progressBar1.ForeColor = System.Drawing.Color.Green;
                completeLabel.Text = "Completed: 100%";
                logBuilder.Append(log);
                textBoxLog.Text = logBuilder.ToString();
                textBoxLog.SelectionStart = textBoxLog.Text.Length;
                textBoxLog.ScrollToCaret();
                //highlight taskbar button
                IntPtr handle = this.Handle;
                FlashWindow(handle, false);
            }
            else
            {
                if (!log.Trim().Equals(""))
                {
                    logBuilder.Append(log + "\r\n");
                    textBoxLog.Text = logBuilder.ToString();
                    textBoxLog.SelectionStart = textBoxLog.Text.Length;
                    textBoxLog.ScrollToCaret();
                }
            }
        }

        
    }
}

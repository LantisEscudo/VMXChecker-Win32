using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.XPath;
using MediaInfoLib;


namespace VMXChecker
{
    public partial class MainForm : Form
    {

        string path = System.Windows.Forms.Application.StartupPath;
        string ffmpeg = "ffmpeg.exe";
        String inputfilename = "";
        String outputfilename = "";
        //private int checkUpdates = 1; // 1 = true
        public int frames = 0;
        private int InputWidth = 0;
        private int InputHeight = 0;
        private string videoFormat = "";
        private string videoFormatVersion = "";
        private string videoFormatProfile = "";
        private string videoDAR = "";
        private string videoScanType = "";
        private string videoFPS = "";
        private string audioFormat = "";
        private string audioFormatVersion = "";
        private string audioFormatProfile = "";
        private int audioSamplingRate = 0;
        private bool canceled = false;
        private bool correctVideoFormat = true;
        private bool correctAudioFormat = true;
        Thread t = null;
        Process p = null;
        public static Progress progress_form = null;
        private const int BUFSIZE = 4096;

        private delegate string build_cmdlineDelegate();
        private delegate void updateProgressDelegate(string log);

        public MainForm(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
            {
                inputfilename = args[0];
                inFileBox.Text = args[0];
                media_info();
                check_file();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void inFileBox_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files (not text or anything else)
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol
                e.Effect = DragDropEffects.All;
        }

        private void inFileBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files_drop = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files_drop[0].EndsWith(".mpg") || files_drop[0].EndsWith(".mpeg"))
            {
                inFileBox.Text = files_drop[0];
                inputfilename = files_drop[0];
                media_info();
                check_file();
            }
            else
            {
                MessageBox.Show("File must be an MPEG video file!");
            }

        }

        private void media_info()
        {
            try
            {
                MediaInfo MI = new MediaInfo();
                MI.Open(inputfilename);
                if (MI.Count_Get(StreamKind.Video) > 0)
                {
                    InputWidth = Convert.ToInt32(MI.Get(StreamKind.Video, 0, "Width"));
                    InputHeight = Convert.ToInt32(MI.Get(StreamKind.Video, 0, "Height"));
                    frames = Convert.ToInt32(MI.Get(StreamKind.Video, 0, "FrameCount"));
                    videoFormat = MI.Get(StreamKind.Video, 0, "Format");
                    videoFormatVersion = MI.Get(StreamKind.Video, 0, "Format_Version");
                    videoFormatProfile = MI.Get(StreamKind.Video, 0, "Format_Profile");
                }

                if (MI.Count_Get(StreamKind.Audio) > 0)
                {
                    audioFormat = MI.Get(StreamKind.Audio, 0, "Format");
                    audioFormatVersion = MI.Get(StreamKind.Audio, 0, "Format_Version");
                    audioFormatProfile = MI.Get(StreamKind.Audio, 0, "Format_Profile");
                    audioSamplingRate = Convert.ToInt32(MI.Get(StreamKind.Audio, 0, "SamplingRate"));
                }

                MI.Close();
            }
            catch (System.Exception excep)
            {
                MessageBox.Show("An error occured while opening the file. \r\nThe following error message was reported: \r\n" + excep.Message, "Error");
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialogInput.Filter = "MPEG Files(*.mpg, *.mpeg)|*.mpg;*.mpeg";
            if (openFileDialogInput.ShowDialog() == DialogResult.OK)
            {
                inputfilename = openFileDialogInput.FileName;
                media_info();

                inFileBox.Text = inputfilename;
                check_file();
            }
        }

        private void check_file()
        {
            correctVideoFormat = true;
            correctAudioFormat = true;
            //VideoInfoLabel.Text = videoFormat + "\n" + videoFormatVersion + "\n" + videoFormatProfile + "\n" + InputWidth.ToString() + "x" + InputHeight.ToString();
            //AudioInfoLabel.Text = audioFormat + "\n" + audioFormatVersion + "\n" + audioFormatProfile + "\n" + audioSamplingRate.ToString();

            if (videoFormat.Equals("MPEG Video") && videoFormatProfile.Equals("Main@Main") && videoFormatVersion.Equals("Version 2") && InputHeight == 480 && InputWidth == 720)
            {
                VideoInfoLabel.Text = "Correct Video Format";
                VideoInfoLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                String errorstring = "";

                if (!videoFormat.Equals("MPEG Video") || !videoFormatVersion.Equals("Version 2"))
                {
                    errorstring += "Not MPEG-2 Video\n";
                }
                else if (!videoFormatProfile.Equals("Main@Main"))
                {
                    errorstring += "Incorrect MPEG-2 Profile";
                }

                if (!videoDAR.Equals("4:3"))
                {
                    errorstring += "Incorrect Aspect Ratio\n";
                }

                if (!videoScanType.Equals("Interlaced"))
                {
                    errorstring += "Incorrect Scan Mode\n";
                }

                if (!videoFPS.Equals("29.970"))
                {
                    errorstring += "Incorrect Frame Rate\n";
                }

                if (InputHeight != 480 || InputWidth != 720)
                {
                    errorstring += "Incorrect Resolution";
                }

                VideoInfoLabel.Text = errorstring;
                VideoInfoLabel.ForeColor = System.Drawing.Color.Red;
                correctVideoFormat = false;
            }

            if (audioFormat.Equals("MPEG Audio") && audioFormatVersion.Equals("Version 1") && audioFormatProfile.Equals("Layer 2") && audioSamplingRate == 48000)
            {
                AudioInfoLabel.Text = "Correct Audio Format";
                AudioInfoLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                String errorstring = "";
                if (!audioFormat.Equals("MPEG Audio"))
                {
                    if (audioFormat.Equals("AC-3"))
                    {
                        errorstring += "Dolby Digital Audio\n";
                    }
                    else
                    {
                        errorstring += "Incorrect Audio Format\n";
                    }
                } 
                
                else if (!audioFormatProfile.Equals("Layer 2"))
                {
                    errorstring += "Incorrect MPEG Audio Profile \n";
                }

                if (audioSamplingRate != 48000)
                {
                    errorstring += "Incorrect Sampling Rate";
                }

                AudioInfoLabel.Text = errorstring;
                AudioInfoLabel.ForeColor = System.Drawing.Color.Red;
                correctAudioFormat = false;
            }

            if (correctAudioFormat && correctVideoFormat)
            {
                MessageLabel.Text = "There are no problems with this file.";
                MessageLabel.ForeColor = System.Drawing.Color.Green;
                FixButton.Enabled = false;
            }
            else
            {
                MessageLabel.Text = "There is a problem with this file.  Click the Fix button to correct the problem.";
                MessageLabel.ForeColor = System.Drawing.Color.Red;
                FixButton.Enabled = true;
            }
        }

        private void FixButton_Click(object sender, EventArgs e)
        {
            //check existance of all programs
            if (System.IO.File.Exists(path + "\\" + ffmpeg) == false)
            {
                MessageBox.Show(ffmpeg + " was not found.\nMake sure that it is located in the same folder as this application.", "Error");
                return;
            }

            //check if output file exists
            /*if (System.IO.File.Exists(textBoxMP4.Text) == true)
            {
                if (MessageBox.Show("The output file already exists.\nWould you like to overwrite it?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }*/

            //start a new thread and run the apps
            t = new Thread(new ThreadStart(this.run_stuff));
            t.Start();
            canceled = false;
            //open the progress window
            progress_form = new Progress();
            progress_form.ShowDialog();
        }

        private void run_stuff()
        {
            //break commands into lines
            string CMD = buildCommand();
            char[] breakup = { ' ' };

            //execute each line
            string[] line_parts = CMD.Split(breakup, 2);
            string line_command = line_parts[0].ToString();
            string line_args = line_parts[1].ToString();

            p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WorkingDirectory = path;
            p.StartInfo.FileName = line_command;
            p.StartInfo.Arguments = line_args;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;

            //progress_form.state = 2;
            p.Start();
            //set_priority(thePriority);
            string readError;
            while ((readError = p.StandardError.ReadLine()) != null)
                Invoke(new updateProgressDelegate(this.updateProgress), readError);
            p.WaitForExit();
            Invoke(new updateProgressDelegate(this.updateProgress), "Finished!");

        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && canceled == false)
                BeginInvoke(new updateProgressDelegate(this.updateProgress), e.Data);
        }

        private void updateProgress(string log)
        {
            progress_form.updateProgress(log);
        }

        private String buildCommand()
        {
            string cmdline = "";

            cmdline += ffmpeg + " ";

            cmdline += "-i \"" + inputfilename + "\" " + "-target ntsc-dvd ";

            if (correctVideoFormat)
            {
                cmdline += "-vcodec copy ";
            }

            if (correctAudioFormat)
            {
                cmdline += "-acodec copy ";
            }
            else
            {
                cmdline += "-acodec mp2 -b:a 192k ";
            }

            if (inputfilename.ToLower().EndsWith(".mpg"))
            {
                outputfilename = inputfilename.Remove(inputfilename.Length - 4, 4) + "-VMX.mpg";
            }
            else if (inputfilename.ToLower().EndsWith(".mpeg"))
            {
                outputfilename = inputfilename.Remove(inputfilename.Length - 5, 5) + "-VMX.mpeg";
            }
            else
            {
                outputfilename = inputfilename + "-VMX.mpg";
            }

            cmdline += "\"" + outputfilename + "\"";

            Console.WriteLine(cmdline);
            return cmdline;
        }

        public void cancel_encode()
        {
            canceled = true;
            t.Join(1000);

            if (p != null)
            {
                if (p.HasExited == false)
                {
                    p.Kill();
                    if (p.WaitForExit(1000))
                        p.WaitForExit();
                }
                if (p.HasExited == false)
                {
                    if (p.WaitForExit(1000))
                        p.WaitForExit();
                }
            }

            if (t.IsAlive)
            {
                if (!t.Join(2000))
                    t.Abort();
            }
        }




    }
}
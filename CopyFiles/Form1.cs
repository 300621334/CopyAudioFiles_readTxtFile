﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//needed for File....
//Overview of DialogueBoxes: https://msdn.microsoft.com/en-us/library/aa969773(v=vs.110).aspx
/*bgWorker:
 1.create instance of bw.
 2.init bw on loading of form (inside constructor Form1())
 * Method ------------------       calls delegate-event
 * ===============                 =======================
 -bw.RunWorkerAsync()    ---calls this---> bw.DoWork += bw_DoWork;// or same thing +=new DoWorkEventHandler(bw_DoWork);
 -bw.ReportProgress(i)   ---calls this---> bw.ProgressChanged += bw_ProgressChanged; //reports progress from within DoWork-thread to UI-thread
 -when bw efinishes job..---calls this---> bw.RunWorkerCompleted += bw_RunWorkerCompleted;
 * */
namespace CopyFiles
{
    public partial class Form1 : Form
    {
        #region Global Vars
        string fileName = "";
        string folder = "";
        string newFile = "";
        string[] allPathsArray = null;
        int missingFiles = 0;
        int found = 0;
        int counter = 0;
        BackgroundWorker bw;
        #endregion

        #region on Form Load
        public Form1()
        {
            InitializeComponent();
            lblFound.Text = "";
            lblNotFound.Text = "";
            lblReportProgress.Text = "";
            InitializeBackgroundWorker();
        }
       #endregion

        #region BackGround Worker event-Method 
        private void InitializeBackgroundWorker()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);//or//bw.DoWork += bw_DoWork; works too //param is an event handler//bw.RunWorkerAsync(); raises the event .DoWord delegate w inturn invokes bw_DoWork() where ALL
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.ProgressChanged += bw_ProgressChanged;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblReportProgress.Text = e.ProgressPercentage.ToString() + " % Copied";
        }


        private void bw_DoWork(Object sender, DoWorkEventArgs e)
        {
            startCopying();
        }
        #endregion

        #region click From-btn
        private void btnFrom_Click(object sender, EventArgs e)
        {
            btnStart.Text = "Start Copying";
            OpenFileDialog dlgFrom = new OpenFileDialog();
            DialogResult x = dlgFrom.ShowDialog();
            if(x == DialogResult.OK)
            {
             
                fileName = dlgFrom.FileName;
                textBox1.Text = fileName;
                lblFound.Text = "";
                lblNotFound.Text = "";
                newFile = "";
                allPathsArray = null;
                missingFiles = 0;
                counter = 0;
            }
            
        }
        #endregion

        #region click To-btn
        private void btnTo_Click(object sender, EventArgs e)
        {
            btnStart.Text = "Start Copying";
            FolderBrowserDialog dlgTo = new FolderBrowserDialog();
            DialogResult x = dlgTo.ShowDialog();
            if (x == DialogResult.OK) //if (d.ShowDialog() == DialogResult.OK)
            {
                folder = dlgTo.SelectedPath;
                textBox2.Text = folder;
            }
        }
        #endregion

        #region click StartCopying btn
        private void btnStart_Click(object sender, EventArgs e)
        {
           

            if (fileName == "")
            {
                btnStart.Text = "Please select a file!";
                return;
            }
            if(folder=="")
            {
                btnStart.Text = "Please select a folder!";
                return;
            }
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
            btnFrom.Enabled  = false;
            btnTo.Enabled    = false;
            btnStart.Enabled = false;
            //startCopying();

          
        }
        #endregion



        #region Method StartCopying
        private void startCopying()
        {
            allPathsArray = File.ReadAllLines(fileName, Encoding.UTF8);
            

            foreach (string path in allPathsArray)
            {
                found++;
                //if(!string.IsNullOrWhiteSpace(path))
                if (File.Exists(path))
                {
                newFile = folder + "\\" + path.Substring(path.LastIndexOf('\\') + 1);
                try
                {
                    File.Copy(path, newFile, true);
                    counter++;
                }
                catch (FileNotFoundException ex)
                {
                    missingFiles++;
                    continue;
                }
                }
                bw.ReportProgress(found*100/allPathsArray.Length);
            }
            lblFound.Text = "Files Found: " + counter;
            counter = 0;
            found = 0;
            lblNotFound.Text = " Files NOT found: " + missingFiles;
            btnFrom.Enabled = true;
            btnTo.Enabled = true;
            btnStart.Enabled = true;
        }
        #endregion

    }
}

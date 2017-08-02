using System;
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

/*From Tools->Options->Environment->Keyboard->mapping scheme (Default)

Expand all: CTRL + M, CTRL + X  == M=minimize, A=All, X=none :-)
Collapse all: CTRL + M, CTRL + A
For selected areas:

Expand selection: CTRL + M, CTRL + E
Collapse selection: CTRL + M, CTRL + S == M=Minimize, S=Selected, E=Expand :-)
 */


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
        string newFile = "", xmlOriginFile = "", xmlDestinFile = "";
        string[] allPathsArray = null;
        int missingFiles = 0;
        int found = 0;
        int linesRead = 0;
        int counter = 0;
        BackgroundWorker bw;
        #endregion

        #region on Form Load
        public Form1()
        {
            InitializeComponent();
            lblFound.Text = "";//remove the default txt from lbls
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
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);//If the BackgroundWorker was created from the UI thread, then the RunWorkerCompleted event will also be raised on the UI thread
            bw.ProgressChanged += bw_ProgressChanged;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (999999 == e.ProgressPercentage)
            {
                lblFound.Text = "Files Found: " + counter;//if set break point here & ebug then err. Thou runs fine without debug. Class-Race issue in multiThread apps. Speed at w sth is done matters. Tried moving this line to bw_completed BUT didn't work:Reason=<<If the BackgroundWorker was created from the UI thread, then the RunWorkerCompleted event will also be raised on the UI thread>> = https://social.msdn.microsoft.com/Forums/vstudio/en-US/110f362f-6009-465b-a940-895e23545ad5/getting-invalidoperationexception-when-running-debug-due-to-a-crossthread-operation?forum=vsdebug
                counter = 0;
                found = 0;
                linesRead = 0;
                fileName = "";
                //folder = "";
                lblNotFound.Text = " Files NOT found: " + missingFiles;
                btnFrom.Enabled = true;
                btnTo.Enabled = true;
                btnStart.Enabled = true;
            }
            else if (999 == e.ProgressPercentage)
            {
                lblFound.Text = "Files Found: " + counter;//if set break point here & ebug then err. Thou runs fine without debug. Class-Race issue in multiThread apps. Speed at w sth is done matters. Tried moving this line to bw_completed BUT didn't work:Reason=<<If the BackgroundWorker was created from the UI thread, then the RunWorkerCompleted event will also be raised on the UI thread>> = https://social.msdn.microsoft.com/Forums/vstudio/en-US/110f362f-6009-465b-a940-895e23545ad5/getting-invalidoperationexception-when-running-debug-due-to-a-crossthread-operation?forum=vsdebug
                counter = 0;
                found = 0;
                linesRead = 0;
                //fileName = "";
                //folder = "";
                lblNotFound.Text = " Files NOT found: " + missingFiles;
                btnFrom.Enabled = true;
                btnTo.Enabled = true;
                btnStart.Enabled = true;
            }
            else
            {
                lblReportProgress.Text = e.ProgressPercentage.ToString() + " % Attempted";
            }
            
        }


        private void bw_DoWork(Object sender, DoWorkEventArgs e)
        {
            startCopying((bool)e.Argument);//if copyXml chk Bx checked, pass true, else pass false. Arg comes from bw.RunWorkerAsync(copyXml);
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
                textBox1.Text = fileName;//reverse way of assignment operator
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
            lblFound.Text = "";
            lblNotFound.Text = "";
            lblReportProgress.Text = "";
            missingFiles = 0;
            counter = 0;

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
                bool copyXml = copyXmlChk.Checked;
                bw.RunWorkerAsync(copyXml);//in single thread app, until this fn completes, control will NOT move forward. But in this Async_BgWorker, control moves on & a 2nd thread starts here
            }
            btnFrom.Enabled  = false;
            btnTo.Enabled    = false;
            btnStart.Enabled = false;
            //startCopying();

          
        }
        #endregion



        #region Method StartCopying
        private void startCopying(bool copyXml)
        {
            try
            {
                allPathsArray = File.ReadAllLines(fileName, Encoding.UTF8);
            }
            catch (Exception)
            {
                MessageBox.Show("Source file not found!");
                //Application.Restart();//does NOT restart bg worker thread hence err
                bw.ReportProgress(999999);
                return;
            }

            if (allPathsArray != null)
            {
            foreach (string path in allPathsArray)
            {
                linesRead++;
                //if(!string.IsNullOrWhiteSpace(path))
                if (File.Exists(path))
                {
                newFile = folder + "\\" + path.Substring(path.LastIndexOf('\\') + 1);
                xmlOriginFile = path.Replace(".wav",".xml");
                xmlDestinFile = newFile.Replace(".wav", ".xml");
                try
                {
                    File.Copy(path, newFile, true);
                    if(copyXml)File.Copy(xmlOriginFile, xmlDestinFile, true);
                    counter++;
                }
                catch (FileNotFoundException ex)
                {
                    missingFiles++;
                    continue;
                }
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(path)) missingFiles++;
                }
                bw.ReportProgress(linesRead * 100 / allPathsArray.Length);//'ReportProgress' calls the delegate 'ProgressChanged' & indirectly all the methods assigned to that delg
            }
        }
            bw.ReportProgress(999);
            //lblFound.Text = "Files Found: " + counter;//if set break point here & ebug then err. Thou runs fine without debug. Class-Race issue in multiThread apps. Speed at w sth is done matters. Tried moving this line to bw_completed BUT didn't work:Reason=<<If the BackgroundWorker was created from the UI thread, then the RunWorkerCompleted event will also be raised on the UI thread>> = https://social.msdn.microsoft.com/Forums/vstudio/en-US/110f362f-6009-465b-a940-895e23545ad5/getting-invalidoperationexception-when-running-debug-due-to-a-crossthread-operation?forum=vsdebug
            //counter = 0;
            //found = 0;
            //linesRead = 0;
            //lblNotFound.Text = " Files NOT found: " + missingFiles;
            //btnFrom.Enabled = true;
            //btnTo.Enabled = true;
            //btnStart.Enabled = true;
        }
        #endregion

    }
}

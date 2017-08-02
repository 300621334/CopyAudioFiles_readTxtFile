namespace CopyFiles
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
            this.btnFrom = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnTo = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblFound = new System.Windows.Forms.Label();
            this.lblNotFound = new System.Windows.Forms.Label();
            this.lblReportProgress = new System.Windows.Forms.Label();
            this.copyXmlChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnFrom
            // 
            this.btnFrom.Location = new System.Drawing.Point(483, 29);
            this.btnFrom.Name = "btnFrom";
            this.btnFrom.Size = new System.Drawing.Size(129, 37);
            this.btnFrom.TabIndex = 0;
            this.btnFrom.Text = "Soure";
            this.btnFrom.UseVisualStyleBackColor = true;
            this.btnFrom.Click += new System.EventHandler(this.btnFrom_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(30, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(447, 22);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(30, 112);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(447, 22);
            this.textBox2.TabIndex = 2;
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(483, 107);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(129, 34);
            this.btnTo.TabIndex = 3;
            this.btnTo.Text = "Destination";
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(30, 153);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(175, 41);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start Copying";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblFound
            // 
            this.lblFound.AutoSize = true;
            this.lblFound.Location = new System.Drawing.Point(239, 77);
            this.lblFound.Name = "lblFound";
            this.lblFound.Size = new System.Drawing.Size(46, 17);
            this.lblFound.TabIndex = 5;
            this.lblFound.Text = "label1";
            // 
            // lblNotFound
            // 
            this.lblNotFound.AutoSize = true;
            this.lblNotFound.Location = new System.Drawing.Point(239, 153);
            this.lblNotFound.Name = "lblNotFound";
            this.lblNotFound.Size = new System.Drawing.Size(46, 17);
            this.lblNotFound.TabIndex = 6;
            this.lblNotFound.Text = "label2";
            // 
            // lblReportProgress
            // 
            this.lblReportProgress.AutoSize = true;
            this.lblReportProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportProgress.ForeColor = System.Drawing.Color.Blue;
            this.lblReportProgress.Location = new System.Drawing.Point(135, 6);
            this.lblReportProgress.Name = "lblReportProgress";
            this.lblReportProgress.Size = new System.Drawing.Size(70, 25);
            this.lblReportProgress.TabIndex = 7;
            this.lblReportProgress.Text = "label1";
            // 
            // copyXmlChk
            // 
            this.copyXmlChk.AutoSize = true;
            this.copyXmlChk.Location = new System.Drawing.Point(382, 172);
            this.copyXmlChk.Name = "copyXmlChk";
            this.copyXmlChk.Size = new System.Drawing.Size(106, 21);
            this.copyXmlChk.TabIndex = 8;
            this.copyXmlChk.Text = "Copy XML ?";
            this.copyXmlChk.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 206);
            this.Controls.Add(this.copyXmlChk);
            this.Controls.Add(this.lblReportProgress);
            this.Controls.Add(this.lblNotFound);
            this.Controls.Add(this.lblFound);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnFrom);
            this.Name = "Form1";
            this.Text = "Copy Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFrom;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblFound;
        private System.Windows.Forms.Label lblNotFound;
        private System.Windows.Forms.Label lblReportProgress;
        private System.Windows.Forms.CheckBox copyXmlChk;
    }
}


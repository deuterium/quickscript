namespace QuickScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.tbDisplayContent = new System.Windows.Forms.TextBox();
            this.lbDisplayScripts = new System.Windows.Forms.ListBox();
            this.btnDisplayReset = new System.Windows.Forms.Button();
            this.tbDisplaySearch = new System.Windows.Forms.TextBox();
            this.tabPageEdit = new System.Windows.Forms.TabPage();
            this.btnEditDelete = new System.Windows.Forms.Button();
            this.btnEditSave = new System.Windows.Forms.Button();
            this.btnEditNew = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEditTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEditSearchTerms = new System.Windows.Forms.TextBox();
            this.tbEditContents = new System.Windows.Forms.TextBox();
            this.lbEditScripts = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgramInfo = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageDisplay.SuspendLayout();
            this.tabPageEdit.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDisplay);
            this.tabControl.Controls.Add(this.tabPageEdit);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(681, 382);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabStop = false;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.tbDisplayContent);
            this.tabPageDisplay.Controls.Add(this.lbDisplayScripts);
            this.tabPageDisplay.Controls.Add(this.btnDisplayReset);
            this.tabPageDisplay.Controls.Add(this.tbDisplaySearch);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplay.Size = new System.Drawing.Size(673, 356);
            this.tabPageDisplay.TabIndex = 0;
            this.tabPageDisplay.Text = "Scripts";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // tbDisplayContent
            // 
            this.tbDisplayContent.Location = new System.Drawing.Point(239, 7);
            this.tbDisplayContent.Multiline = true;
            this.tbDisplayContent.Name = "tbDisplayContent";
            this.tbDisplayContent.ReadOnly = true;
            this.tbDisplayContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDisplayContent.Size = new System.Drawing.Size(428, 342);
            this.tbDisplayContent.TabIndex = 2;
            // 
            // lbDisplayScripts
            // 
            this.lbDisplayScripts.FormattingEnabled = true;
            this.lbDisplayScripts.Location = new System.Drawing.Point(7, 33);
            this.lbDisplayScripts.Name = "lbDisplayScripts";
            this.lbDisplayScripts.Size = new System.Drawing.Size(225, 316);
            this.lbDisplayScripts.TabIndex = 1;
            this.lbDisplayScripts.Click += new System.EventHandler(this.lbDisplayScripts_Click);
            this.lbDisplayScripts.DoubleClick += new System.EventHandler(this.lbDisplayScripts_DoubleClick);
            this.lbDisplayScripts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbDisplayScripts_KeyDown);
            this.lbDisplayScripts.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lbDisplayScripts_PreviewKeyDown);
            // 
            // btnDisplayReset
            // 
            this.btnDisplayReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplayReset.Location = new System.Drawing.Point(191, 6);
            this.btnDisplayReset.Name = "btnDisplayReset";
            this.btnDisplayReset.Size = new System.Drawing.Size(43, 20);
            this.btnDisplayReset.TabIndex = 99;
            this.btnDisplayReset.TabStop = false;
            this.btnDisplayReset.Text = "Reset";
            this.btnDisplayReset.UseVisualStyleBackColor = true;
            this.btnDisplayReset.Click += new System.EventHandler(this.btnDisplayReset_Click);
            // 
            // tbDisplaySearch
            // 
            this.tbDisplaySearch.Location = new System.Drawing.Point(6, 6);
            this.tbDisplaySearch.Name = "tbDisplaySearch";
            this.tbDisplaySearch.Size = new System.Drawing.Size(182, 20);
            this.tbDisplaySearch.TabIndex = 0;
            this.tbDisplaySearch.Enter += new System.EventHandler(this.tbDisplaySearch_Enter);
            this.tbDisplaySearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbDisplaySearch_KeyUp);
            // 
            // tabPageEdit
            // 
            this.tabPageEdit.Controls.Add(this.btnEditDelete);
            this.tabPageEdit.Controls.Add(this.btnEditSave);
            this.tabPageEdit.Controls.Add(this.btnEditNew);
            this.tabPageEdit.Controls.Add(this.label3);
            this.tabPageEdit.Controls.Add(this.tbEditTitle);
            this.tabPageEdit.Controls.Add(this.label2);
            this.tabPageEdit.Controls.Add(this.label1);
            this.tabPageEdit.Controls.Add(this.tbEditSearchTerms);
            this.tabPageEdit.Controls.Add(this.tbEditContents);
            this.tabPageEdit.Controls.Add(this.lbEditScripts);
            this.tabPageEdit.Location = new System.Drawing.Point(4, 22);
            this.tabPageEdit.Name = "tabPageEdit";
            this.tabPageEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEdit.Size = new System.Drawing.Size(673, 356);
            this.tabPageEdit.TabIndex = 1;
            this.tabPageEdit.Text = "Edit";
            this.tabPageEdit.UseVisualStyleBackColor = true;
            // 
            // btnEditDelete
            // 
            this.btnEditDelete.Location = new System.Drawing.Point(319, 325);
            this.btnEditDelete.Name = "btnEditDelete";
            this.btnEditDelete.Size = new System.Drawing.Size(75, 23);
            this.btnEditDelete.TabIndex = 12;
            this.btnEditDelete.TabStop = false;
            this.btnEditDelete.Text = "Delete";
            this.btnEditDelete.UseVisualStyleBackColor = true;
            this.btnEditDelete.Click += new System.EventHandler(this.btnEditDelete_Click);
            // 
            // btnEditSave
            // 
            this.btnEditSave.Enabled = false;
            this.btnEditSave.Location = new System.Drawing.Point(592, 325);
            this.btnEditSave.Name = "btnEditSave";
            this.btnEditSave.Size = new System.Drawing.Size(75, 23);
            this.btnEditSave.TabIndex = 3;
            this.btnEditSave.Text = "Save";
            this.btnEditSave.UseVisualStyleBackColor = true;
            this.btnEditSave.Click += new System.EventHandler(this.btnEditSave_Click);
            // 
            // btnEditNew
            // 
            this.btnEditNew.Location = new System.Drawing.Point(241, 325);
            this.btnEditNew.Name = "btnEditNew";
            this.btnEditNew.Size = new System.Drawing.Size(75, 23);
            this.btnEditNew.TabIndex = 9;
            this.btnEditNew.TabStop = false;
            this.btnEditNew.Text = "New";
            this.btnEditNew.UseVisualStyleBackColor = true;
            this.btnEditNew.Click += new System.EventHandler(this.btnEditNew_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "/\\ Title";
            // 
            // tbEditTitle
            // 
            this.tbEditTitle.Location = new System.Drawing.Point(241, 7);
            this.tbEditTitle.MaxLength = 255;
            this.tbEditTitle.Name = "tbEditTitle";
            this.tbEditTitle.Size = new System.Drawing.Size(174, 20);
            this.tbEditTitle.TabIndex = 0;
            this.tbEditTitle.TextChanged += new System.EventHandler(this.tbEditTitle_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(610, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Content \\/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(418, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "/\\ Search Terms (comma seperated)";
            // 
            // tbEditSearchTerms
            // 
            this.tbEditSearchTerms.Location = new System.Drawing.Point(421, 7);
            this.tbEditSearchTerms.MaxLength = 255;
            this.tbEditSearchTerms.Name = "tbEditSearchTerms";
            this.tbEditSearchTerms.Size = new System.Drawing.Size(246, 20);
            this.tbEditSearchTerms.TabIndex = 1;
            this.tbEditSearchTerms.TextChanged += new System.EventHandler(this.tbEditSearchTerms_TextChanged);
            // 
            // tbEditContents
            // 
            this.tbEditContents.AcceptsReturn = true;
            this.tbEditContents.Location = new System.Drawing.Point(239, 46);
            this.tbEditContents.Multiline = true;
            this.tbEditContents.Name = "tbEditContents";
            this.tbEditContents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbEditContents.Size = new System.Drawing.Size(428, 273);
            this.tbEditContents.TabIndex = 2;
            this.tbEditContents.TextChanged += new System.EventHandler(this.tbEditContents_TextChanged);
            // 
            // lbEditScripts
            // 
            this.lbEditScripts.FormattingEnabled = true;
            this.lbEditScripts.Location = new System.Drawing.Point(7, 7);
            this.lbEditScripts.Name = "lbEditScripts";
            this.lbEditScripts.Size = new System.Drawing.Size(225, 342);
            this.lbEditScripts.TabIndex = 2;
            this.lbEditScripts.TabStop = false;
            this.lbEditScripts.Click += new System.EventHandler(this.lbEditScripts_Click);
            this.lbEditScripts.DoubleClick += new System.EventHandler(this.lbEditScripts_DoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.lblProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 401);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(706, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.MarqueeAnimationSpeed = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.progressBar.Size = new System.Drawing.Size(172, 16);
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 17);
            // 
            // lblProgramInfo
            // 
            this.lblProgramInfo.AutoSize = true;
            this.lblProgramInfo.Location = new System.Drawing.Point(552, 407);
            this.lblProgramInfo.Name = "lblProgramInfo";
            this.lblProgramInfo.Size = new System.Drawing.Size(155, 13);
            this.lblProgramInfo.TabIndex = 3;
            this.lblProgramInfo.Text = "QuickScript v1R2 - Chris Wood";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 423);
            this.Controls.Add(this.lblProgramInfo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "QuickScript";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            this.tabPageEdit.ResumeLayout(false);
            this.tabPageEdit.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private System.Windows.Forms.TabPage tabPageEdit;
        private System.Windows.Forms.TextBox tbDisplaySearch;
        private System.Windows.Forms.TextBox tbDisplayContent;
        private System.Windows.Forms.ListBox lbDisplayScripts;
        private System.Windows.Forms.Button btnDisplayReset;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ListBox lbEditScripts;
        private System.Windows.Forms.TextBox tbEditContents;
        private System.Windows.Forms.TextBox tbEditSearchTerms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbEditTitle;
        private System.Windows.Forms.Button btnEditSave;
        private System.Windows.Forms.Button btnEditNew;
        private System.Windows.Forms.Button btnEditDelete;
        private System.Windows.Forms.Label lblProgramInfo;
    }
}


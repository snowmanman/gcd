namespace GCDCore.UserInterface.ChangeDetection
{
    partial class frmDoDResults : System.Windows.Forms.Form
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

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoDResults));
            this.cmdOK = new System.Windows.Forms.Button();
            this.txtDoDName = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.ucProperties = new GCDCore.UserInterface.ChangeDetection.ucDoDPropertiesGrid();
            this.tbpElevationChangeDistribution = new System.Windows.Forms.TabPage();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ucHistogram = new GCDCore.UserInterface.ChangeDetection.ucDoDHistogram();
            this.ucBars = new GCDCore.UserInterface.ChangeDetection.ucChangeBars();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.ucSummary = new GCDCore.UserInterface.ChangeDetection.ucDoDSummary();
            this.tabProperties = new System.Windows.Forms.TabControl();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.cmdProperties = new System.Windows.Forms.Button();
            this.cmdAddToMap = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.tTip = new System.Windows.Forms.ToolTip(this.components);
            this.TabPage3.SuspendLayout();
            this.tbpElevationChangeDistribution.SuspendLayout();
            this.TableLayoutPanel1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.tabProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Image = global::GCDCore.Properties.Resources.Save;
            this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdOK.Location = new System.Drawing.Point(527, 495);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // txtDoDName
            // 
            this.txtDoDName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDoDName.Location = new System.Drawing.Point(48, 12);
            this.txtDoDName.MaxLength = 100;
            this.txtDoDName.Name = "txtDoDName";
            this.txtDoDName.Size = new System.Drawing.Size(550, 20);
            this.txtDoDName.TabIndex = 1;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(7, 16);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(35, 13);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Name";
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.ucProperties);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(668, 411);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Analysis Details";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // ucProperties
            // 
            this.ucProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucProperties.Location = new System.Drawing.Point(3, 3);
            this.ucProperties.Name = "ucProperties";
            this.ucProperties.Size = new System.Drawing.Size(662, 405);
            this.ucProperties.TabIndex = 0;
            // 
            // tbpElevationChangeDistribution
            // 
            this.tbpElevationChangeDistribution.Controls.Add(this.TableLayoutPanel1);
            this.tbpElevationChangeDistribution.Location = new System.Drawing.Point(4, 22);
            this.tbpElevationChangeDistribution.Name = "tbpElevationChangeDistribution";
            this.tbpElevationChangeDistribution.Padding = new System.Windows.Forms.Padding(3);
            this.tbpElevationChangeDistribution.Size = new System.Drawing.Size(668, 411);
            this.tbpElevationChangeDistribution.TabIndex = 1;
            this.tbpElevationChangeDistribution.Text = "Graphical Results";
            this.tbpElevationChangeDistribution.UseVisualStyleBackColor = true;
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.51274F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.48726F));
            this.TableLayoutPanel1.Controls.Add(this.ucHistogram, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.ucBars, 1, 0);
            this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(662, 405);
            this.TableLayoutPanel1.TabIndex = 1;
            // 
            // ucHistogram
            // 
            this.ucHistogram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucHistogram.ChartContextMenuStrip = null;
            this.ucHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucHistogram.Location = new System.Drawing.Point(3, 3);
            this.ucHistogram.Name = "ucHistogram";
            this.ucHistogram.Size = new System.Drawing.Size(487, 399);
            this.ucHistogram.TabIndex = 0;
            // 
            // ucBars
            // 
            this.ucBars.ChangeStats = null;
            this.ucBars.ChartContextMenuStrip = null;
            this.ucBars.DisplayUnits = null;
            this.ucBars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBars.Location = new System.Drawing.Point(496, 3);
            this.ucBars.Name = "ucBars";
            this.ucBars.Size = new System.Drawing.Size(163, 399);
            this.ucBars.TabIndex = 1;
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.ucSummary);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(668, 411);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Tabular Results";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // ucSummary
            // 
            this.ucSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSummary.Location = new System.Drawing.Point(3, 3);
            this.ucSummary.Name = "ucSummary";
            this.ucSummary.Size = new System.Drawing.Size(662, 405);
            this.ucSummary.TabIndex = 0;
            // 
            // tabProperties
            // 
            this.tabProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabProperties.Controls.Add(this.TabPage1);
            this.tabProperties.Controls.Add(this.tbpElevationChangeDistribution);
            this.tabProperties.Controls.Add(this.TabPage3);
            this.tabProperties.Location = new System.Drawing.Point(7, 48);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.SelectedIndex = 0;
            this.tabProperties.Size = new System.Drawing.Size(676, 437);
            this.tabProperties.TabIndex = 5;
            // 
            // cmdHelp
            // 
            this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdHelp.Location = new System.Drawing.Point(7, 495);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(75, 23);
            this.cmdHelp.TabIndex = 7;
            this.cmdHelp.Text = "Help";
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Image = global::GCDCore.Properties.Resources.BrowseFolder;
            this.cmdBrowse.Location = new System.Drawing.Point(603, 11);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(23, 23);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // cmdProperties
            // 
            this.cmdProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdProperties.Image = global::GCDCore.Properties.Resources.Settings;
            this.cmdProperties.Location = new System.Drawing.Point(632, 11);
            this.cmdProperties.Name = "cmdProperties";
            this.cmdProperties.Size = new System.Drawing.Size(23, 23);
            this.cmdProperties.TabIndex = 3;
            this.cmdProperties.UseVisualStyleBackColor = true;
            this.cmdProperties.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // cmdAddToMap
            // 
            this.cmdAddToMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddToMap.Image = global::GCDCore.Properties.Resources.AddToMap;
            this.cmdAddToMap.Location = new System.Drawing.Point(660, 11);
            this.cmdAddToMap.Name = "cmdAddToMap";
            this.cmdAddToMap.Size = new System.Drawing.Size(23, 23);
            this.cmdAddToMap.TabIndex = 4;
            this.cmdAddToMap.UseVisualStyleBackColor = true;
            this.cmdAddToMap.Click += new System.EventHandler(this.cmdAddToMap_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(608, 495);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // frmDoDResults
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(695, 530);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAddToMap);
            this.Controls.Add(this.cmdProperties);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.txtDoDName);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.tabProperties);
            this.Controls.Add(this.cmdOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "frmDoDResults";
            this.Text = "Change Detection Results";
            this.Load += new System.EventHandler(this.DoDResultsForm_Load);
            this.TabPage3.ResumeLayout(false);
            this.tbpElevationChangeDistribution.ResumeLayout(false);
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.tabProperties.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.TextBox txtDoDName;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TabPage TabPage3;
        internal System.Windows.Forms.TabPage tbpElevationChangeDistribution;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal ucDoDHistogram ucHistogram;
        internal ucChangeBars ucBars;
        internal System.Windows.Forms.TabPage TabPage1;
        internal ucDoDSummary ucSummary;
        internal System.Windows.Forms.TabControl tabProperties;
        private System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.Button cmdProperties;
        private System.Windows.Forms.Button cmdAddToMap;
        private ucDoDPropertiesGrid ucProperties;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolTip tTip;
    }
}
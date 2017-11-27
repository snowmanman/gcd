using System;

namespace GCDCore.UserInterface.SurveyLibrary
{
    partial class frmDEMSurveyProperties : System.Windows.Forms.Form
    {

        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDEMSurveyProperties));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnHlp = new System.Windows.Forms.Button();
            this.ttpTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.pgeErrors = new System.Windows.Forms.TabPage();
            this.btnCalculateError = new System.Windows.Forms.Button();
            this.cmdAddErrorToMap = new System.Windows.Forms.Button();
            this.grdErrorSurfaces = new System.Windows.Forms.DataGridView();
            this.btnErrorDelete = new System.Windows.Forms.Button();
            this.btnErrorSettings = new System.Windows.Forms.Button();
            this.btnAddError = new System.Windows.Forms.Button();
            this.pgeSurfaces = new System.Windows.Forms.TabPage();
            this.grdAssocSurface = new System.Windows.Forms.DataGridView();
            this.btnAddAssociatedSurface = new System.Windows.Forms.Button();
            this.cmdAddAssocToMap = new System.Windows.Forms.Button();
            this.btnDeleteAssociatedSurface = new System.Windows.Forms.Button();
            this.btnSettingsAssociatedSurface = new System.Windows.Forms.Button();
            this.pgeSurvey = new System.Windows.Forms.TabPage();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.txtProperties = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.cboSingle = new System.Windows.Forms.ComboBox();
            this.btnBrowseMask = new System.Windows.Forms.Button();
            this.cmdAddDEMToMap = new System.Windows.Forms.Button();
            this.cboIdentify = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.rdoMulti = new System.Windows.Forms.RadioButton();
            this.rdoSingle = new System.Windows.Forms.RadioButton();
            this.txtRasterPath = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmdDateTime = new System.Windows.Forms.Button();
            this.lblDatetime = new System.Windows.Forms.Label();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pgeErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.grdErrorSurfaces).BeginInit();
            this.pgeSurfaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.grdAssocSurface).BeginInit();
            this.pgeSurvey.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            //
            //btnOK
            //
            this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnOK.Location = new System.Drawing.Point(482, 577);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Save Survey and Close";
            this.btnOK.UseVisualStyleBackColor = true;
            //
            //btnHlp
            //
            this.btnHlp.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnHlp.Location = new System.Drawing.Point(12, 577);
            this.btnHlp.Name = "btnHlp";
            this.btnHlp.Size = new System.Drawing.Size(75, 23);
            this.btnHlp.TabIndex = 9;
            this.btnHlp.Text = "Help";
            this.btnHlp.UseVisualStyleBackColor = true;
            //
            //pgeErrors
            //
            this.pgeErrors.Controls.Add(this.btnCalculateError);
            this.pgeErrors.Controls.Add(this.cmdAddErrorToMap);
            this.pgeErrors.Controls.Add(this.grdErrorSurfaces);
            this.pgeErrors.Controls.Add(this.btnErrorDelete);
            this.pgeErrors.Controls.Add(this.btnErrorSettings);
            this.pgeErrors.Controls.Add(this.btnAddError);
            this.pgeErrors.Location = new System.Drawing.Point(4, 22);
            this.pgeErrors.Name = "pgeErrors";
            this.pgeErrors.Padding = new System.Windows.Forms.Padding(3);
            this.pgeErrors.Size = new System.Drawing.Size(602, 460);
            this.pgeErrors.TabIndex = 2;
            this.pgeErrors.Text = "Error Calculations";
            this.pgeErrors.UseVisualStyleBackColor = true;
            //
            //btnCalculateError
            //
            this.btnCalculateError.Image = Properties.Resources.sigma;
            this.btnCalculateError.Location = new System.Drawing.Point(40, 6);
            this.btnCalculateError.Name = "btnCalculateError";
            this.btnCalculateError.Size = new System.Drawing.Size(29, 23);
            this.btnCalculateError.TabIndex = 5;
            this.btnCalculateError.UseVisualStyleBackColor = true;
            //
            //cmdAddErrorToMap
            //
            this.cmdAddErrorToMap.Enabled = false;
            this.cmdAddErrorToMap.Image = Properties.Resources.AddToMap;
            this.cmdAddErrorToMap.Location = new System.Drawing.Point(144, 6);
            this.cmdAddErrorToMap.Name = "cmdAddErrorToMap";
            this.cmdAddErrorToMap.Size = new System.Drawing.Size(29, 23);
            this.cmdAddErrorToMap.TabIndex = 3;
            this.cmdAddErrorToMap.UseVisualStyleBackColor = true;
            //
            //grdErrorSurfaces
            //
            this.grdErrorSurfaces.AllowUserToAddRows = false;
            this.grdErrorSurfaces.AllowUserToDeleteRows = false;
            this.grdErrorSurfaces.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdErrorSurfaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdErrorSurfaces.Location = new System.Drawing.Point(3, 35);
            this.grdErrorSurfaces.MultiSelect = false;
            this.grdErrorSurfaces.Name = "grdErrorSurfaces";
            this.grdErrorSurfaces.ReadOnly = true;
            this.grdErrorSurfaces.RowHeadersVisible = false;
            this.grdErrorSurfaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdErrorSurfaces.Size = new System.Drawing.Size(593, 493);
            this.grdErrorSurfaces.TabIndex = 4;
            //
            //btnErrorDelete
            //
            this.btnErrorDelete.Enabled = false;
            this.btnErrorDelete.Image = Properties.Resources.Delete;
            this.btnErrorDelete.Location = new System.Drawing.Point(74, 6);
            this.btnErrorDelete.Name = "btnErrorDelete";
            this.btnErrorDelete.Size = new System.Drawing.Size(29, 23);
            this.btnErrorDelete.TabIndex = 2;
            this.btnErrorDelete.UseVisualStyleBackColor = true;
            //
            //btnErrorSettings
            //
            this.btnErrorSettings.Enabled = false;
            this.btnErrorSettings.Image = Properties.Resources.Settings;
            this.btnErrorSettings.Location = new System.Drawing.Point(109, 6);
            this.btnErrorSettings.Name = "btnErrorSettings";
            this.btnErrorSettings.Size = new System.Drawing.Size(29, 23);
            this.btnErrorSettings.TabIndex = 1;
            this.btnErrorSettings.UseVisualStyleBackColor = true;
            //
            //btnAddError
            //
            this.btnAddError.Image = Properties.Resources.Add;
            this.btnAddError.Location = new System.Drawing.Point(6, 6);
            this.btnAddError.Name = "btnAddError";
            this.btnAddError.Size = new System.Drawing.Size(29, 23);
            this.btnAddError.TabIndex = 0;
            this.btnAddError.UseVisualStyleBackColor = true;
            //
            //pgeSurfaces
            //
            this.pgeSurfaces.AutoScroll = true;
            this.pgeSurfaces.Controls.Add(this.grdAssocSurface);
            this.pgeSurfaces.Controls.Add(this.btnAddAssociatedSurface);
            this.pgeSurfaces.Controls.Add(this.cmdAddAssocToMap);
            this.pgeSurfaces.Controls.Add(this.btnDeleteAssociatedSurface);
            this.pgeSurfaces.Controls.Add(this.btnSettingsAssociatedSurface);
            this.pgeSurfaces.Location = new System.Drawing.Point(4, 22);
            this.pgeSurfaces.Name = "pgeSurfaces";
            this.pgeSurfaces.Padding = new System.Windows.Forms.Padding(3);
            this.pgeSurfaces.Size = new System.Drawing.Size(602, 460);
            this.pgeSurfaces.TabIndex = 1;
            this.pgeSurfaces.Text = "Associated Surfaces";
            this.pgeSurfaces.UseVisualStyleBackColor = true;
            //
            //grdAssocSurface
            //
            this.grdAssocSurface.AllowUserToAddRows = false;
            this.grdAssocSurface.AllowUserToDeleteRows = false;
            this.grdAssocSurface.BackgroundColor = System.Drawing.SystemColors.Control;
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            DataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAssocSurface.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1;
            this.grdAssocSurface.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAssocSurface.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colName,
                this.colType
            });
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            DataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAssocSurface.DefaultCellStyle = DataGridViewCellStyle2;
            this.grdAssocSurface.Location = new System.Drawing.Point(3, 35);
            this.grdAssocSurface.MultiSelect = false;
            this.grdAssocSurface.Name = "grdAssocSurface";
            this.grdAssocSurface.ReadOnly = true;
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            DataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAssocSurface.RowHeadersDefaultCellStyle = DataGridViewCellStyle3;
            this.grdAssocSurface.RowHeadersVisible = false;
            this.grdAssocSurface.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAssocSurface.Size = new System.Drawing.Size(593, 425);
            this.grdAssocSurface.TabIndex = 4;
            //
            //btnAddAssociatedSurface
            //
            this.btnAddAssociatedSurface.Image = Properties.Resources.Add;
            this.btnAddAssociatedSurface.Location = new System.Drawing.Point(3, 6);
            this.btnAddAssociatedSurface.Name = "btnAddAssociatedSurface";
            this.btnAddAssociatedSurface.Size = new System.Drawing.Size(29, 23);
            this.btnAddAssociatedSurface.TabIndex = 0;
            this.btnAddAssociatedSurface.UseVisualStyleBackColor = true;
            //
            //cmdAddAssocToMap
            //
            this.cmdAddAssocToMap.Enabled = false;
            this.cmdAddAssocToMap.Image = Properties.Resources.AddToMap;
            this.cmdAddAssocToMap.Location = new System.Drawing.Point(108, 6);
            this.cmdAddAssocToMap.Name = "cmdAddAssocToMap";
            this.cmdAddAssocToMap.Size = new System.Drawing.Size(29, 23);
            this.cmdAddAssocToMap.TabIndex = 3;
            this.cmdAddAssocToMap.UseVisualStyleBackColor = true;
            //
            //btnDeleteAssociatedSurface
            //
            this.btnDeleteAssociatedSurface.Enabled = false;
            this.btnDeleteAssociatedSurface.Image = Properties.Resources.Delete;
            this.btnDeleteAssociatedSurface.Location = new System.Drawing.Point(73, 6);
            this.btnDeleteAssociatedSurface.Name = "btnDeleteAssociatedSurface";
            this.btnDeleteAssociatedSurface.Size = new System.Drawing.Size(29, 23);
            this.btnDeleteAssociatedSurface.TabIndex = 2;
            this.btnDeleteAssociatedSurface.UseVisualStyleBackColor = true;
            //
            //btnSettingsAssociatedSurface
            //
            this.btnSettingsAssociatedSurface.Enabled = false;
            this.btnSettingsAssociatedSurface.Image = Properties.Resources.Settings;
            this.btnSettingsAssociatedSurface.Location = new System.Drawing.Point(38, 6);
            this.btnSettingsAssociatedSurface.Name = "btnSettingsAssociatedSurface";
            this.btnSettingsAssociatedSurface.Size = new System.Drawing.Size(29, 23);
            this.btnSettingsAssociatedSurface.TabIndex = 1;
            this.btnSettingsAssociatedSurface.UseVisualStyleBackColor = true;
            //
            //pgeSurvey
            //
            this.pgeSurvey.Controls.Add(this.GroupBox2);
            this.pgeSurvey.Controls.Add(this.GroupBox1);
            this.pgeSurvey.Location = new System.Drawing.Point(4, 22);
            this.pgeSurvey.Name = "pgeSurvey";
            this.pgeSurvey.Padding = new System.Windows.Forms.Padding(3);
            this.pgeSurvey.Size = new System.Drawing.Size(602, 460);
            this.pgeSurvey.TabIndex = 0;
            this.pgeSurvey.Text = "DEM Survey";
            this.pgeSurvey.UseVisualStyleBackColor = true;
            //
            //GroupBox2
            //
            this.GroupBox2.Controls.Add(this.txtProperties);
            this.GroupBox2.Location = new System.Drawing.Point(18, 239);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(565, 209);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Survey Raster Properties";
            //
            //txtProperties
            //
            this.txtProperties.Location = new System.Drawing.Point(8, 20);
            this.txtProperties.Multiline = true;
            this.txtProperties.Name = "txtProperties";
            this.txtProperties.ReadOnly = true;
            this.txtProperties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProperties.Size = new System.Drawing.Size(551, 183);
            this.txtProperties.TabIndex = 0;
            this.txtProperties.TabStop = false;
            //
            //GroupBox1
            //
            this.GroupBox1.Controls.Add(this.txtMask);
            this.GroupBox1.Controls.Add(this.cboSingle);
            this.GroupBox1.Controls.Add(this.btnBrowseMask);
            this.GroupBox1.Controls.Add(this.cmdAddDEMToMap);
            this.GroupBox1.Controls.Add(this.cboIdentify);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.rdoMulti);
            this.GroupBox1.Controls.Add(this.rdoSingle);
            this.GroupBox1.Controls.Add(this.txtRasterPath);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Location = new System.Drawing.Point(18, 16);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(565, 217);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Source";
            //
            //txtMask
            //
            this.txtMask.Location = new System.Drawing.Point(119, 144);
            this.txtMask.Name = "txtMask";
            this.txtMask.ReadOnly = true;
            this.txtMask.Size = new System.Drawing.Size(405, 20);
            this.txtMask.TabIndex = 8;
            this.txtMask.TabStop = false;
            //
            //cboSingle
            //
            this.cboSingle.DisplayMember = "Name";
            this.cboSingle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSingle.FormattingEnabled = true;
            this.cboSingle.Location = new System.Drawing.Point(41, 80);
            this.cboSingle.Name = "cboSingle";
            this.cboSingle.Size = new System.Drawing.Size(186, 21);
            this.cboSingle.TabIndex = 5;
            this.cboSingle.ValueMember = "Name";
            //
            //btnBrowseMask
            //
            this.btnBrowseMask.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnBrowseMask.Image = Properties.Resources.BrowseFolder;
            this.btnBrowseMask.Location = new System.Drawing.Point(530, 143);
            this.btnBrowseMask.Name = "btnBrowseMask";
            this.btnBrowseMask.Size = new System.Drawing.Size(29, 23);
            this.btnBrowseMask.TabIndex = 9;
            this.btnBrowseMask.UseVisualStyleBackColor = true;
            //
            //cmdAddDEMToMap
            //
            this.cmdAddDEMToMap.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.cmdAddDEMToMap.Image = Properties.Resources.AddToMap;
            this.cmdAddDEMToMap.Location = new System.Drawing.Point(530, 25);
            this.cmdAddDEMToMap.Name = "cmdAddDEMToMap";
            this.cmdAddDEMToMap.Size = new System.Drawing.Size(29, 23);
            this.cmdAddDEMToMap.TabIndex = 2;
            this.cmdAddDEMToMap.UseVisualStyleBackColor = true;
            //
            //cboIdentify
            //
            this.cboIdentify.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIdentify.FormattingEnabled = true;
            this.cboIdentify.Location = new System.Drawing.Point(119, 181);
            this.cboIdentify.Name = "cboIdentify";
            this.cboIdentify.Size = new System.Drawing.Size(149, 21);
            this.cboIdentify.TabIndex = 11;
            //
            //Label5
            //
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(41, 184);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(72, 13);
            this.Label5.TabIndex = 10;
            this.Label5.Text = "Identifier field:";
            //
            //Label6
            //
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(41, 148);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(36, 13);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "Mask:";
            //
            //rdoMulti
            //
            this.rdoMulti.AutoSize = true;
            this.rdoMulti.Location = new System.Drawing.Point(18, 122);
            this.rdoMulti.Name = "rdoMulti";
            this.rdoMulti.Size = new System.Drawing.Size(119, 17);
            this.rdoMulti.TabIndex = 6;
            this.rdoMulti.TabStop = true;
            this.rdoMulti.Text = "Multi-method survey";
            this.rdoMulti.UseVisualStyleBackColor = true;
            //
            //rdoSingle
            //
            this.rdoSingle.AutoSize = true;
            this.rdoSingle.Checked = true;
            this.rdoSingle.Location = new System.Drawing.Point(18, 57);
            this.rdoSingle.Name = "rdoSingle";
            this.rdoSingle.Size = new System.Drawing.Size(126, 17);
            this.rdoSingle.TabIndex = 4;
            this.rdoSingle.TabStop = true;
            this.rdoSingle.Text = "Single method survey";
            this.rdoSingle.UseVisualStyleBackColor = true;
            //
            //txtRasterPath
            //
            this.txtRasterPath.Location = new System.Drawing.Point(97, 26);
            this.txtRasterPath.Name = "txtRasterPath";
            this.txtRasterPath.ReadOnly = true;
            this.txtRasterPath.Size = new System.Drawing.Size(427, 20);
            this.txtRasterPath.TabIndex = 1;
            this.txtRasterPath.TabStop = false;
            //
            //Label3
            //
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(15, 30);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(76, 13);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Raster source:";
            //
            //txtName
            //
            this.txtName.Location = new System.Drawing.Point(89, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(286, 20);
            this.txtName.TabIndex = 1;
            //
            //Label1
            //
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(74, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Survey Name:";
            //
            //tabControl
            //
            this.tabControl.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.tabControl.Controls.Add(this.pgeSurvey);
            this.tabControl.Controls.Add(this.pgeSurfaces);
            this.tabControl.Controls.Add(this.pgeErrors);
            this.tabControl.Location = new System.Drawing.Point(12, 85);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(610, 486);
            this.tabControl.TabIndex = 6;
            //
            //txtFolder
            //
            this.txtFolder.Location = new System.Drawing.Point(89, 49);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(533, 20);
            this.txtFolder.TabIndex = 5;
            //
            //Label4
            //
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(12, 53);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(39, 13);
            this.Label4.TabIndex = 4;
            this.Label4.Text = "Folder:";
            //
            //cmdDateTime
            //
            this.cmdDateTime.Location = new System.Drawing.Point(382, 18);
            this.cmdDateTime.Name = "cmdDateTime";
            this.cmdDateTime.Size = new System.Drawing.Size(108, 23);
            this.cmdDateTime.TabIndex = 2;
            this.cmdDateTime.Text = "Survey Date/Time";
            this.cmdDateTime.UseVisualStyleBackColor = true;
            //
            //lblDatetime
            //
            this.lblDatetime.AutoSize = true;
            this.lblDatetime.Location = new System.Drawing.Point(497, 23);
            this.lblDatetime.Name = "lblDatetime";
            this.lblDatetime.Size = new System.Drawing.Size(99, 13);
            this.lblDatetime.TabIndex = 3;
            this.lblDatetime.Text = "10 Dec 2012 23:59";
            //
            //colName
            //
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            //
            //colType
            //
            this.colType.DataPropertyName = "AssocSurfaceType";
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            //
            //frmDEMSurveyProperties
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 612);
            this.ControlBox = false;
            this.Controls.Add(this.lblDatetime);
            this.Controls.Add(this.cmdDateTime);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.btnHlp);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDEMSurveyProperties";
            this.Text = "DEM Survey Properties";
            this.pgeErrors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.grdErrorSurfaces).EndInit();
            this.pgeSurfaces.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.grdAssocSurface).EndInit();
            this.pgeSurvey.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Button withEventsField_btnOK;
        internal System.Windows.Forms.Button btnOK
        {
            get { return withEventsField_btnOK; }
            set
            {
                if (withEventsField_btnOK != null)
                {
                    withEventsField_btnOK.Click -= btnOK_Click;
                }
                withEventsField_btnOK = value;
                if (withEventsField_btnOK != null)
                {
                    withEventsField_btnOK.Click += btnOK_Click;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_btnHlp;
        internal System.Windows.Forms.Button btnHlp
        {
            get { return withEventsField_btnHlp; }
            set
            {
                if (withEventsField_btnHlp != null)
                {
                    withEventsField_btnHlp.Click -= btnHlp_Click;
                }
                withEventsField_btnHlp = value;
                if (withEventsField_btnHlp != null)
                {
                    withEventsField_btnHlp.Click += btnHlp_Click;
                }
            }
        }
        internal System.Windows.Forms.ToolTip ttpTooltip;
        internal System.Windows.Forms.TabPage pgeErrors;
        private System.Windows.Forms.Button withEventsField_cmdAddErrorToMap;
        internal System.Windows.Forms.Button cmdAddErrorToMap
        {
            get { return withEventsField_cmdAddErrorToMap; }
            set
            {
                if (withEventsField_cmdAddErrorToMap != null)
                {
                    withEventsField_cmdAddErrorToMap.Click -= btnAddErrorToMap_Click;
                }
                withEventsField_cmdAddErrorToMap = value;
                if (withEventsField_cmdAddErrorToMap != null)
                {
                    withEventsField_cmdAddErrorToMap.Click += btnAddErrorToMap_Click;
                }
            }
        }
        private System.Windows.Forms.DataGridView withEventsField_grdErrorSurfaces;
        internal System.Windows.Forms.DataGridView grdErrorSurfaces
        {
            get { return withEventsField_grdErrorSurfaces; }
            set
            {
                if (withEventsField_grdErrorSurfaces != null)
                {
                    withEventsField_grdErrorSurfaces.CellEnter -= Error_CellContentEnter;
                    withEventsField_grdErrorSurfaces.CellLeave -= Error_CellContentLeave;
                    withEventsField_grdErrorSurfaces.CellContentDoubleClick -= Error_DoubleClick;
                }
                withEventsField_grdErrorSurfaces = value;
                if (withEventsField_grdErrorSurfaces != null)
                {
                    withEventsField_grdErrorSurfaces.CellEnter += Error_CellContentEnter;
                    withEventsField_grdErrorSurfaces.CellLeave += Error_CellContentLeave;
                    withEventsField_grdErrorSurfaces.CellContentDoubleClick += Error_DoubleClick;
                }
            }
        }
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn7;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn8;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn9;
        private System.Windows.Forms.Button withEventsField_btnErrorDelete;
        internal System.Windows.Forms.Button btnErrorDelete
        {
            get { return withEventsField_btnErrorDelete; }
            set
            {
                if (withEventsField_btnErrorDelete != null)
                {
                    withEventsField_btnErrorDelete.Click -= btnDeleteErrorSurface_Click;
                }
                withEventsField_btnErrorDelete = value;
                if (withEventsField_btnErrorDelete != null)
                {
                    withEventsField_btnErrorDelete.Click += btnDeleteErrorSurface_Click;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_btnErrorSettings;
        internal System.Windows.Forms.Button btnErrorSettings
        {
            get { return withEventsField_btnErrorSettings; }
            set
            {
                if (withEventsField_btnErrorSettings != null)
                {
                    withEventsField_btnErrorSettings.Click -= btnErrorSurfaceSettings_Click;
                }
                withEventsField_btnErrorSettings = value;
                if (withEventsField_btnErrorSettings != null)
                {
                    withEventsField_btnErrorSettings.Click += btnErrorSurfaceSettings_Click;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_btnAddError;
        internal System.Windows.Forms.Button btnAddError
        {
            get { return withEventsField_btnAddError; }
            set
            {
                if (withEventsField_btnAddError != null)
                {
                    withEventsField_btnAddError.Click -= btn_AddErrorSurface_Click;
                }
                withEventsField_btnAddError = value;
                if (withEventsField_btnAddError != null)
                {
                    withEventsField_btnAddError.Click += btn_AddErrorSurface_Click;
                }
            }
        }
        internal System.Windows.Forms.TabPage pgeSurfaces;
        private System.Windows.Forms.DataGridView withEventsField_grdAssocSurface;
        internal System.Windows.Forms.DataGridView grdAssocSurface
        {
            get { return withEventsField_grdAssocSurface; }
            set
            {
                if (withEventsField_grdAssocSurface != null)
                {
                    withEventsField_grdAssocSurface.CellContentDoubleClick -= ViewAssociatedSurface;
                    withEventsField_grdAssocSurface.CellEnter -= Associated_CellContentEnter;
                    withEventsField_grdAssocSurface.CellLeave -= Associated_CellContentLeave;
                }
                withEventsField_grdAssocSurface = value;
                if (withEventsField_grdAssocSurface != null)
                {
                    withEventsField_grdAssocSurface.CellContentDoubleClick += ViewAssociatedSurface;
                    withEventsField_grdAssocSurface.CellEnter += Associated_CellContentEnter;
                    withEventsField_grdAssocSurface.CellLeave += Associated_CellContentLeave;
                }
            }
        }
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn4;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button withEventsField_btnAddAssociatedSurface;
        internal System.Windows.Forms.Button btnAddAssociatedSurface
        {
            get { return withEventsField_btnAddAssociatedSurface; }
            set
            {
                if (withEventsField_btnAddAssociatedSurface != null)
                {
                    withEventsField_btnAddAssociatedSurface.Click -= btnAddAssociatedSurface_Click;
                }
                withEventsField_btnAddAssociatedSurface = value;
                if (withEventsField_btnAddAssociatedSurface != null)
                {
                    withEventsField_btnAddAssociatedSurface.Click += btnAddAssociatedSurface_Click;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_cmdAddAssocToMap;
        internal System.Windows.Forms.Button cmdAddAssocToMap
        {
            get { return withEventsField_cmdAddAssocToMap; }
            set
            {
                if (withEventsField_cmdAddAssocToMap != null)
                {
                    withEventsField_cmdAddAssocToMap.Click -= btnAddToMap_Click;
                }
                withEventsField_cmdAddAssocToMap = value;
                if (withEventsField_cmdAddAssocToMap != null)
                {
                    withEventsField_cmdAddAssocToMap.Click += btnAddToMap_Click;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_btnDeleteAssociatedSurface;
        internal System.Windows.Forms.Button btnDeleteAssociatedSurface
        {
            get { return withEventsField_btnDeleteAssociatedSurface; }
            set
            {
                if (withEventsField_btnDeleteAssociatedSurface != null)
                {
                    withEventsField_btnDeleteAssociatedSurface.Click -= btnDeleteAssociatedSurface_Click;
                }
                withEventsField_btnDeleteAssociatedSurface = value;
                if (withEventsField_btnDeleteAssociatedSurface != null)
                {
                    withEventsField_btnDeleteAssociatedSurface.Click += btnDeleteAssociatedSurface_Click;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_btnSettingsAssociatedSurface;
        internal System.Windows.Forms.Button btnSettingsAssociatedSurface
        {
            get { return withEventsField_btnSettingsAssociatedSurface; }
            set
            {
                if (withEventsField_btnSettingsAssociatedSurface != null)
                {
                    withEventsField_btnSettingsAssociatedSurface.Click -= ViewAssociatedSurface;
                }
                withEventsField_btnSettingsAssociatedSurface = value;
                if (withEventsField_btnSettingsAssociatedSurface != null)
                {
                    withEventsField_btnSettingsAssociatedSurface.Click += ViewAssociatedSurface;
                }
            }
        }
        internal System.Windows.Forms.TabPage pgeSurvey;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ComboBox cboSingle;
        private System.Windows.Forms.Button withEventsField_btnBrowseMask;
        internal System.Windows.Forms.Button btnBrowseMask
        {
            get { return withEventsField_btnBrowseMask; }
            set
            {
                if (withEventsField_btnBrowseMask != null)
                {
                    withEventsField_btnBrowseMask.Click -= btnBrowseMask_Click;
                }
                withEventsField_btnBrowseMask = value;
                if (withEventsField_btnBrowseMask != null)
                {
                    withEventsField_btnBrowseMask.Click += btnBrowseMask_Click;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_cmdAddDEMToMap;
        internal System.Windows.Forms.Button cmdAddDEMToMap
        {
            get { return withEventsField_cmdAddDEMToMap; }
            set
            {
                if (withEventsField_cmdAddDEMToMap != null)
                {
                    withEventsField_cmdAddDEMToMap.Click -= btnBrowseFile_Click;
                }
                withEventsField_cmdAddDEMToMap = value;
                if (withEventsField_cmdAddDEMToMap != null)
                {
                    withEventsField_cmdAddDEMToMap.Click += btnBrowseFile_Click;
                }
            }
        }
        internal System.Windows.Forms.ComboBox cboIdentify;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.RadioButton rdoMulti;
        internal System.Windows.Forms.RadioButton rdoSingle;
        internal System.Windows.Forms.TextBox txtRasterPath;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TabControl tabControl;
        internal System.Windows.Forms.TextBox txtProperties;
        private System.Windows.Forms.TextBox withEventsField_txtMask;
        internal System.Windows.Forms.TextBox txtMask
        {
            get { return withEventsField_txtMask; }
            set
            {
                if (withEventsField_txtMask != null)
                {
                    withEventsField_txtMask.TextChanged -= txtMask_TextChanged;
                }
                withEventsField_txtMask = value;
                if (withEventsField_txtMask != null)
                {
                    withEventsField_txtMask.TextChanged += txtMask_TextChanged;
                }
            }
        }
        private System.Windows.Forms.Button withEventsField_btnCalculateError;
        internal System.Windows.Forms.Button btnCalculateError
        {
            get { return withEventsField_btnCalculateError; }
            set
            {
                if (withEventsField_btnCalculateError != null)
                {
                    withEventsField_btnCalculateError.Click -= btnCalculateError_Click;
                }
                withEventsField_btnCalculateError = value;
                if (withEventsField_btnCalculateError != null)
                {
                    withEventsField_btnCalculateError.Click += btnCalculateError_Click;
                }
            }
        }
        private System.Windows.Forms.TextBox withEventsField_txtFolder;
        internal System.Windows.Forms.TextBox txtFolder
        {
            get { return withEventsField_txtFolder; }
            set
            {
                if (withEventsField_txtFolder != null)
                {
                    withEventsField_txtFolder.DoubleClick -= txtFolder_DoubleClick;
                }
                withEventsField_txtFolder = value;
                if (withEventsField_txtFolder != null)
                {
                    withEventsField_txtFolder.DoubleClick += txtFolder_DoubleClick;
                }
            }
        }
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Button withEventsField_cmdDateTime;
        internal System.Windows.Forms.Button cmdDateTime
        {
            get { return withEventsField_cmdDateTime; }
            set
            {
                if (withEventsField_cmdDateTime != null)
                {
                    withEventsField_cmdDateTime.Click -= cmdDateTime_Click;
                }
                withEventsField_cmdDateTime = value;
                if (withEventsField_cmdDateTime != null)
                {
                    withEventsField_cmdDateTime.Click += cmdDateTime_Click;
                }
            }
        }
        internal System.Windows.Forms.Label lblDatetime;
        internal System.Windows.Forms.DataGridViewTextBoxColumn colName;
        internal System.Windows.Forms.DataGridViewTextBoxColumn colType;
    }

}
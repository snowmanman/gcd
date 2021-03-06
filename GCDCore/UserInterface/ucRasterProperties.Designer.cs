﻿namespace GCDCore.UserInterface
{
    partial class ucRasterProperties
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.colProperty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.SuspendLayout();
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.grdData);
            this.grpProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProperties.Location = new System.Drawing.Point(0, 0);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(402, 200);
            this.grpProperties.TabIndex = 9;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Properties";
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.AllowUserToResizeRows = false;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProperty,
            this.colValue});
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(3, 16);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(396, 181);
            this.grdData.TabIndex = 0;
            this.grdData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdData_CellFormatting);
            // 
            // colProperty
            // 
            this.colProperty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProperty.DataPropertyName = "Property";
            this.colProperty.HeaderText = "Property";
            this.colProperty.Name = "colProperty";
            this.colProperty.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.DataPropertyName = "Value";
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // ucRasterProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpProperties);
            this.Name = "ucRasterProperties";
            this.Size = new System.Drawing.Size(402, 200);
            this.Load += new System.EventHandler(this.ucRasterProperties_Load);
            this.grpProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    }
}

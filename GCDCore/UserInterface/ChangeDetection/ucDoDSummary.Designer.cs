using GCDCore.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
namespace GCDCore.UserInterface.ChangeDetection
{
	partial class ucDoDSummary : System.Windows.Forms.UserControl
	{

		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try {
				if (disposing && components != null) {
					components.Dispose();
				}
			} finally {
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
			this.grdData = new System.Windows.Forms.DataGridView();
			this.colAttribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colRaw = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colThresholded = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SymbolCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colError = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colErrorPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)this.grdData).BeginInit();
			this.SuspendLayout();
			//
			//grdData
			//
			this.grdData.AllowUserToAddRows = false;
			this.grdData.AllowUserToDeleteRows = false;
			this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
				this.colAttribute,
				this.colRaw,
				this.colThresholded,
				this.SymbolCol,
				this.colError,
				this.colErrorPC
			});
			this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdData.Location = new System.Drawing.Point(0, 0);
			this.grdData.Name = "grdData";
			this.grdData.ReadOnly = true;
			this.grdData.Size = new System.Drawing.Size(500, 400);
			this.grdData.TabIndex = 3;
			//
			//colAttribute
			//
			this.colAttribute.Frozen = true;
			this.colAttribute.HeaderText = "Attribute";
			this.colAttribute.Name = "colAttribute";
			this.colAttribute.ReadOnly = true;
			this.colAttribute.Width = 300;
			//
			//colRaw
			//
			this.colRaw.HeaderText = "Raw";
			this.colRaw.Name = "colRaw";
			this.colRaw.ReadOnly = true;
			this.colRaw.Width = 70;
			//
			//colThresholded
			//
			this.colThresholded.HeaderText = "Thresholded";
			this.colThresholded.Name = "colThresholded";
			this.colThresholded.ReadOnly = true;
			this.colThresholded.Width = 70;
			//
			//SymbolCol
			//
			this.SymbolCol.HeaderText = "Symbol";
			this.SymbolCol.Name = "SymbolCol";
			this.SymbolCol.ReadOnly = true;
			this.SymbolCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.SymbolCol.Width = 15;
			//
			//colError
			//
			this.colError.HeaderText = "Error Volume";
			this.colError.Name = "colError";
			this.colError.ReadOnly = true;
			this.colError.Width = 70;
			//
			//colErrorPC
			//
			this.colErrorPC.HeaderText = "Error PC";
			this.colErrorPC.Name = "colErrorPC";
			this.colErrorPC.ReadOnly = true;
			this.colErrorPC.Width = 70;
			//
			//ucDoDSummary
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grdData);
			this.Name = "ucDoDSummary";
			this.Size = new System.Drawing.Size(500, 400);
			((System.ComponentModel.ISupportInitialize)this.grdData).EndInit();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.DataGridView grdData;
		internal System.Windows.Forms.DataGridViewTextBoxColumn colAttribute;
		internal System.Windows.Forms.DataGridViewTextBoxColumn colRaw;
		internal System.Windows.Forms.DataGridViewTextBoxColumn colThresholded;
		internal System.Windows.Forms.DataGridViewTextBoxColumn SymbolCol;
		internal System.Windows.Forms.DataGridViewTextBoxColumn colError;

		internal System.Windows.Forms.DataGridViewTextBoxColumn colErrorPC;
	}
}
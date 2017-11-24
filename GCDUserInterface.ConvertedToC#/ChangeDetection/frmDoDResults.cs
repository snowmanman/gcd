using GCDCore.Project;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using GCDCore.ChangeDetection;

namespace GCDUserInterface.ChangeDetection
{
	public partial class frmDoDResults
	{

		/// <summary>
		/// Stores the status of what columns, rows and units to use in the child user controls
		/// </summary>
		/// <remarks>This is passed to the pop-up form </remarks>
		private DoDSummaryDisplayOptions m_Options;

		private DoDBase DoD;

		public frmDoDResults(GCDCore.Project.DoDBase dodItem)
		{
			Load += DoDResultsForm_Load;
			InitializeComponent();

			DoD = dodItem;
			m_Options = new DoDSummaryDisplayOptions(ProjectManager.Project.Units);
			txtDoDName.Text = DoD.Name;
			ucProperties.Initialize(DoD);

			// Select the tab control to make it easy for user to quickly pan results
			tabProperties.Select();

		}


		private void DoDResultsForm_Load(object sender, System.EventArgs e)
		{
			if (!ProjectManager.IsArcMap) {
				cmdAddToMap.Visible = false;
				txtDoDName.Width = cmdAddToMap.Right - txtDoDName.Left;
			}

			ucBars.ChangeStats = DoD.Statistics;
			ucHistogram.LoadHistograms(DoD.Histograms.Raw.Data, DoD.Histograms.Thr.Data);
			ucSummary.RefreshDisplay(DoD.Statistics, ref m_Options);

		}


		private void cmdAddToMap_Click(System.Object sender, System.EventArgs e)
		{
			throw new Exception("Add to map not implemented");

		}

		private void cmdBrowse_Click(System.Object sender, System.EventArgs e)
		{
			Process.Start("explorer.exe", DoD.RawDoD.RasterPath.Directory.FullName);
		}

		private void cmdSettings_Click(object sender, EventArgs e)
		{
			try {
				frmDoDSummaryProperties frm = new frmDoDSummaryProperties(m_Options);
				if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					ucSummary.RefreshDisplay(DoD.Statistics, ref m_Options);
					ucHistogram.SetHistogramUnits(m_Options.Units);
					ucBars.Refresh();
				}

			} catch (Exception ex) {
				naru.error.ExceptionUI.HandleException(ex);
			}
		}

		private void cmdHelp_Click(System.Object sender, System.EventArgs e)
		{
			Process.Start(GCDCore.Properties.Resources.HelpBaseURL + "gcd-command-reference/gcd-project-explorer/l-individual-change-detection-context-menu/i-view-change-detection-results");
		}

	}

}

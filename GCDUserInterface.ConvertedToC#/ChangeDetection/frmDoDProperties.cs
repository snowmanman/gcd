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
using System.Windows.Forms;

namespace GCDUserInterface.ChangeDetection
{

	public partial class frmDoDProperties
	{

		//Private m_pArcMap As ESRI.ArcGIS.ArcMapUI.IMxApplication
		private bool m_bUserEditedName;

		private frmCoherenceProperties m_frmSpatialCoherence;
		// These are the results of the analysis. They are not populated until
		// the user clicks OK and the change detection completes successfully.

		private DoDBase m_DoD;
		// Initial DEM Surveys to select. (User right clicked on a pair of DEMs
		// in the project explorer and chose to add a new DoD for the same DEMs.
		private DEMSurvey m_InitialNewDEM;

		private DEMSurvey m_InitialOldDEM;
		/// <summary>
		/// Retrieve the GCD project dataset DoD row generated by the change detection
		/// </summary>
		/// <returns>GCD project dataset DoD row generated by the change detection</returns>
		/// <remarks>Returns nothing if not calculated.</remarks>
		public DoDBase DoD {
			get { return m_DoD; }
		}


		public frmDoDProperties()
		{
			Load += DoDPropertiesForm_Load;
			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			m_bUserEditedName = false;

		}


		public frmDoDProperties(DEMSurvey newDEM, DEMSurvey oldDEM)
		{
			Load += DoDPropertiesForm_Load;
			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			//m_pArcMap = pArcMap
			m_bUserEditedName = false;

			m_InitialNewDEM = newDEM;
			m_InitialOldDEM = oldDEM;

		}


		private void DoDPropertiesForm_Load(object sender, System.EventArgs e)
		{
			cboNewDEM.DataSource = ProjectManager.Project.DEMsSortByName(false);
			cboOldDEM.DataSource = ProjectManager.Project.DEMsSortByName(false);

			if (m_InitialNewDEM == null) {
				// There's no initial DEM so simply pick the first one
				if (cboNewDEM.Items.Count > 0) {
					cboNewDEM.SelectedIndex = 0;
				}
			} else {
				cboNewDEM.SelectedItem = m_InitialNewDEM;
			}

			if (m_InitialOldDEM == null) {
				// There's no initial DEM so simply pick the SECOND dem (i.e. different than the new DEM abovE)
				if (cboOldDEM.Items.Count > 1) {
					cboOldDEM.SelectedIndex = 1;
				}
			} else {
				cboOldDEM.SelectedItem = m_InitialOldDEM;
			}

			EnableDisableControls();

			lblMinLodThreshold.Text = string.Format("Threshold ({0})", UnitsNet.Length.GetAbbreviation(ProjectManager.Project.Units.VertUnit));

			if (cboNewDEM.Items.Count > 0 && cboNewDEM.SelectedIndex < 0) {
				cboNewDEM.SelectedIndex = 0;
			}

			UpdateAnalysisName();

		}


		private void cmdOK_Click(System.Object sender, System.EventArgs e)
		{
			if (!ValidateForm()) {
				this.DialogResult = System.Windows.Forms.DialogResult.None;
				return;
			}

			try {
				Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

				System.IO.DirectoryInfo dFolder = new System.IO.DirectoryInfo(txtOutputFolder.Text);
				GCDCore.Engines.ChangeDetectionEngineBase cdEngine = null;

				if (rdoMinLOD.Checked) {
					cdEngine = new GCDCore.Engines.ChangeDetectionEngineMinLoD(txtName.Text, dFolder, cboNewDEM.SelectedItem, cboOldDEM.SelectedItem, valMinLodThreshold.Value);

				} else if (rdoPropagated.Checked) {
					cdEngine = new GCDCore.Engines.ChangeDetectionEnginePropProb(txtName.Text, dFolder, cboNewDEM.SelectedItem, cboOldDEM.SelectedItem, cboNewError.SelectedItem, cboOldError.SelectedItem);
				} else {
					CoherenceProperties spatCoherence = null;
					if (chkBayesian.Checked) {
						spatCoherence = new CoherenceProperties(m_frmSpatialCoherence.FilterSize, m_frmSpatialCoherence.PercentLess, m_frmSpatialCoherence.PercentGreater);
					}

					cdEngine = new GCDCore.Engines.ChangeDetectionEngineProbabilistic(txtName.Text, dFolder, cboNewDEM.SelectedItem, cboOldDEM.SelectedItem, cboNewError.SelectedItem, cboOldError.SelectedItem, Convert.ToDouble(valConfidence.Value), spatCoherence);

				}

				m_DoD = cdEngine.Calculate(true, ProjectManager.Project.CellArea, ProjectManager.Project.Units);

				ProjectManager.Project.DoDs(txtName.Name) = m_DoD;
				ProjectManager.Project.Save();

			} catch (Exception ex) {
				naru.error.ExceptionUI.HandleException(ex);
				m_DoD = null;
			} finally {
				Cursor.Current = Cursors.Default;
			}

		}

		private bool ValidateForm()
		{

			// Safety step to prevent names with just spaces or difficult to detect trailing spaces
			txtName.Text = txtName.Text.Trim();

			if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtName.Text.Trim())) {
				Interaction.MsgBox("Please enter a name for the analysis.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
				txtName.Select();
				return false;
			}

			if (System.IO.Directory.Exists(txtOutputFolder.Text)) {
				Interaction.MsgBox("An analysis folder with the same output path already exists. Please change the analysis name so that a different output folder will be used.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
				txtName.Select();
				return false;
			}

			if (!ProjectManager.Project.IsDoDNameUnique(txtName.Text, null)) {
				Interaction.MsgBox("A change detection already exists with this name. Please choose a unique name.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
				txtName.Select();
				return false;
			}

			if (cboNewDEM.SelectedItem is DEMSurvey) {
				if (cboOldDEM.SelectedItem is DEMSurvey) {
					if (object.ReferenceEquals(cboNewDEM.SelectedItem, cboOldDEM.SelectedItem)) {
						Interaction.MsgBox("Please choose two different DEM Surveys.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
						return false;
					}
				} else {
					Interaction.MsgBox("Please select an Old DEM Survey.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
					return false;
				}
			} else {
				Interaction.MsgBox("Please select a New DEM Survey.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
				return false;
			}

			if (!rdoMinLOD.Checked) {
				if (!cboNewError.SelectedItem is naru.db.NamedObject) {
					Interaction.MsgBox("Please select a new error surface.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
					return false;
				}

				if (!cboOldError.SelectedItem is naru.db.NamedObject) {
					Interaction.MsgBox("Please select an old error surface.", MsgBoxStyle.Information, GCDCore.Properties.Resources.ApplicationNameLong);
					return false;
				}
			}

			return true;

		}


		private void rdoProbabilistic_CheckedChanged(object sender, System.EventArgs e)
		{
			EnableDisableControls();
			UpdateAnalysisName();

		}


		private void EnableDisableControls()
		{
			lblNewError.Enabled = !rdoMinLOD.Checked;
			cboNewError.Enabled = !rdoMinLOD.Checked;
			lblOldError.Enabled = !rdoMinLOD.Checked;
			cboOldError.Enabled = !rdoMinLOD.Checked;

			valMinLodThreshold.Enabled = rdoMinLOD.Checked;
			lblMinLodThreshold.Enabled = rdoMinLOD.Checked;

			lblConfidence.Enabled = rdoProbabilistic.Checked;
			valConfidence.Enabled = rdoProbabilistic.Checked;
			chkBayesian.Enabled = rdoProbabilistic.Checked;
			cmdBayesianProperties.Enabled = rdoProbabilistic.Checked && chkBayesian.Checked;

		}

		#region "DEM Selection Changed"


		private void cboNewDEM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboNewDEM.SelectedItem is DEMSurvey) {
				cboNewError.DataSource = ((DEMSurvey)cboNewDEM.SelectedItem).ErrorSurfaces.Values.ToList();

				if (cboNewError.Items.Count == 1) {
					cboNewError.SelectedIndex = 0;
				}
			}

			UpdateAnalysisName();

		}


		private void cboOldDEM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboOldDEM.SelectedItem is DEMSurvey) {
				cboOldError.DataSource = ((DEMSurvey)cboOldDEM.SelectedItem).ErrorSurfaces.Values.ToList();

				if (cboOldError.Items.Count == 1) {
					cboOldError.SelectedIndex = 0;
				}
			}

			UpdateAnalysisName();

		}


		private void UpdateAnalysisName()
		{
			if (m_bUserEditedName) {
				return;
			}

			string sAnalysisName = naru.os.File.RemoveDangerousCharacters(cboNewDEM.Text);
			if (!string.IsNullOrEmpty(sAnalysisName)) {
				sAnalysisName += "_";
			}

			if (!string.IsNullOrEmpty(cboOldDEM.Text)) {
				sAnalysisName += naru.os.File.RemoveDangerousCharacters(cboOldDEM.Text);
			}

			if (rdoMinLOD.Checked) {
				sAnalysisName += " MinLoD " + valMinLodThreshold.Value.ToString("#0.00");
			} else if (rdoPropagated.Checked) {
				sAnalysisName += " Prop";
			} else {
				sAnalysisName += " Prob " + valConfidence.Value.ToString("#0.00");
			}

			txtName.Text = sAnalysisName.Trim();

		}

		#endregion


		private void rdoCommonArea_CheckedChanged(object sender, System.EventArgs e)
		{
			EnableDisableControls();

		}

		private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_bUserEditedName = true;
		}


		private void txtName_TextChanged(object sender, System.EventArgs e)
		{
			try {
				if (string.IsNullOrEmpty(txtName.Text)) {
					txtOutputFolder.Text = string.Empty;
				} else {
					txtOutputFolder.Text = ProjectManager.OutputManager.GetDoDOutputFolder(txtName.Text);
				}

			} catch (Exception ex) {
			}

		}


		private void cmdBayesianProperties_Click(System.Object sender, System.EventArgs e)
		{
			m_frmSpatialCoherence.ShowDialog();

		}

		private void chkBayesian_CheckedChanged(object sender, System.EventArgs e)
		{
			EnableDisableControls();
		}


		private void valConfidence_ValueChanged(object sender, System.EventArgs e)
		{
			UpdateAnalysisName();
		}

		private void cmdHelp_Click(System.Object sender, System.EventArgs e)
		{
			Process.Start(GCDCore.Properties.Resources.HelpBaseURL + "gcd-command-reference/gcd-project-explorer/j-change-detection-context-menu/i-add-change-detection");
		}
	}

}

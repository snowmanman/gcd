using GCDCore.Project;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace GCDCore.UserInterface.ChangeDetection
{
    public partial class frmDoDProperties
    {
        private bool m_bUserEditedName;

        // These are the results of the analysis. They are not populated until
        // the user clicks OK and the change detection completes successfully.

        private DoDBase m_DoD;
        /// <summary>
        /// Retrieve the GCD project dataset DoD row generated by the change detection
        /// </summary>
        /// <returns>GCD project dataset DoD row generated by the change detection</returns>
        /// <remarks>Returns nothing if not calculated.</remarks>
        public DoDBase DoD
        {
            get { return m_DoD; }
        }

        public frmDoDProperties()
        {
            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            m_bUserEditedName = false;
        }

        public frmDoDProperties(DEMSurvey newDEM, DEMSurvey oldDEM)
        {
            // This call is required by the designer.
            InitializeComponent();

            ucDEMs.NewDEM = newDEM;
            ucDEMs.OldDEM = oldDEM;

            // Add any initialization after the InitializeComponent() call.
            //m_pArcMap = pArcMap
            m_bUserEditedName = false;
        }

        private void DoDPropertiesForm_Load(object sender, System.EventArgs e)
        {
            EnableDisableControls();

            // Subscribe to the event when DEM selection changes
            ucDEMs.SelectedDEMsChanged += UpdateAnalysisName;
            ucThresholding.OnThresholdingMethodChanged += ThresholdMethodChanged;

            // Subscribe to the event when DEM selection changes
            ucDEMs.SelectedDEMsChanged += UpdateAnalysisName;
            ucThresholding.OnThresholdingMethodChanged += ThresholdMethodChanged;

            UpdateAnalysisName(sender, e);
        }

        private void cmdOK_Click(System.Object sender, System.EventArgs e)
        {
            if (!ValidateForm())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            try
            {
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                System.IO.DirectoryInfo dFolder = new System.IO.DirectoryInfo(txtOutputFolder.Text);
                GCDCore.Engines.ChangeDetectionEngineBase cdEngine = null;

                if (ucThresholding.ThresholdProperties.Method == Engines.DoD.ThresholdProps.ThresholdMethods.MinLoD)
                {
                    cdEngine = new Engines.ChangeDetectionEngineMinLoD(ucDEMs.NewDEM, ucDEMs.OldDEM, ucThresholding.ThresholdProperties.Threshold);
                }
                else
                {
                    if (ucThresholding.ThresholdProperties.Method == Engines.DoD.ThresholdProps.ThresholdMethods.Propagated)
                    {
                        cdEngine = new Engines.ChangeDetectionEnginePropProb(ucDEMs.NewDEM, ucDEMs.OldDEM, ucDEMs.NewError, ucDEMs.OldError);
                    }
                    else
                    {
                        cdEngine = new Engines.ChangeDetectionEngineProbabilistic(ucDEMs.NewDEM, ucDEMs.OldDEM, ucDEMs.NewError, ucDEMs.OldError, ucThresholding.ThresholdProperties.Threshold, ucThresholding.ThresholdProperties.SpatialCoherenceProps);
                    }
                }

                m_DoD = cdEngine.Calculate(txtName.Text, dFolder, true, ProjectManager.Project.Units);

                ProjectManager.Project.DoDs[txtName.Name] = m_DoD;
                ProjectManager.Project.Save();

            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
                m_DoD = null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ValidateForm()
        {
            // Safety step to prevent names with just spaces or difficult to detect trailing spaces
            txtName.Text = txtName.Text.Trim();

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Please enter a name for the analysis.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            if (System.IO.Directory.Exists(txtOutputFolder.Text))
            {
                MessageBox.Show("An analysis folder with the same output path already exists. Please change the analysis name so that a different output folder will be used.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            if (!ProjectManager.Project.IsDoDNameUnique(txtName.Text, null))
            {
                MessageBox.Show("A change detection already exists with this name. Please choose a unique name.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            if (!ucDEMs.ValidateForm())
            {
                ucDEMs.Select();
                return false;
            }

            return true;
        }

        private void EnableDisableControls()
        {
            ucDEMs.EnableErrorSurfaces(ucThresholding.ThresholdProperties.Method != Engines.DoD.ThresholdProps.ThresholdMethods.MinLoD);
        }

        private void ThresholdMethodChanged(object sender, EventArgs e)
        {
            EnableDisableControls();
            UpdateAnalysisName(sender, e);
        }

        #region "DEM Selection Changed"  

        private void UpdateAnalysisName(object sender, EventArgs e)
        {
            if (m_bUserEditedName)
            {
                return;
            }

            if (ucDEMs.NewDEM == null || ucDEMs.OldDEM == null)
            {
                return;
            }

            txtName.Text = GetUniqueAnalysisName(ucDEMs.NewDEM.Name, ucDEMs.OldDEM.Name, ucThresholding.ThresholdProperties.ThresholdString);
        }

        public static string GetUniqueAnalysisName(string newDEM, string oldDEM, string Threshold)
        {

            int index = 0;
            string rootName = string.Format("{0}_{1} {2}", newDEM, oldDEM, Threshold);
            string dodName = string.Empty;
            do
            {
                dodName = rootName;
                if (index > 0)
                    dodName += string.Format(" ({0})", index);

                index++;
            } while (!ProjectManager.Project.IsDoDNameUnique(dodName, null));

            return dodName;
        }

        #endregion

        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            m_bUserEditedName = true;
        }

        private void txtName_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    txtOutputFolder.Text = string.Empty;
                }
                else
                {
                    txtOutputFolder.Text = ProjectManager.Project.GetRelativePath(ProjectManager.OutputManager.GetDoDOutputFolder(txtName.Text).FullName);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void cmdHelp_Click(System.Object sender, System.EventArgs e)
        {
            Process.Start(GCDCore.Properties.Resources.HelpBaseURL + "gcd-command-reference/gcd-project-explorer/j-change-detection-context-menu/i-add-change-detection");
        }
    }
}

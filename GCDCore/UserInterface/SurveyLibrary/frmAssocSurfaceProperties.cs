using GCDCore.Project;
using System.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;

namespace GCDCore.UserInterface.SurveyLibrary
{
    public partial class frmAssocSurfaceProperties
    {

        private enum AssociatedSurfaceMethods
        {
            Browse,
            PointDensity,
            SlopePercent,
            SlopeDegree,
            Roughness,
            PointQuality3D,
            InterpolationError
        }

        private AssocSurface m_Assoc;
        private DEMSurvey DEM;
        private frmPointDensity m_frmPointDensity;
        private frmImportRaster m_ImportForm;
        //Private m_SurfaceRoughnessForm As frmSurfaceRoughness

        // This method tracks whether the surface is from an existing
        // raster or to be generated by GCD from slope or point density.
        // Note that browsing to an existing point density or slope raster
        // is considered "Browsing".

        private AssociatedSurfaceMethods m_eMethod;
        public AssocSurface AssociatedSurface
        {
            get { return m_Assoc; }
        }


        public frmAssocSurfaceProperties(DEMSurvey parentDEM, AssocSurface assoc)
        {
            Load += SurfacePropertiesForm_Load;
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            m_eMethod = AssociatedSurfaceMethods.Browse;
            DEM = parentDEM;
            m_Assoc = assoc;

        }


        private void SurfacePropertiesForm_Load(System.Object sender, System.EventArgs e)
        {
            ttpTooltip.SetToolTip(btnCancel, "Cancel and close this form.");
            ttpTooltip.SetToolTip(btnHelp, string.Empty);
            ttpTooltip.SetToolTip(cboType, "Associated surface type. Use this to define what physical phenomenon this associated surface represents.");
            ttpTooltip.SetToolTip(btnBrowse, "Browse and choose an existing raster that represents the associated surface.");
            ttpTooltip.SetToolTip(btnSlopePercent, "Calculate a slope raster - in percent - from the DEM survey.");
            ttpTooltip.SetToolTip(btnSlopeDegree, "Calculate a slope raster - in degrees - from the DEM survey.");
            ttpTooltip.SetToolTip(btnDensity, "Calculate a point density raster from the DEM Survey. You will be presented with options for the point density calculation.");
            ttpTooltip.SetToolTip(btnRoughness, "Calculate a surface roughness raster from space delimited point cloud file.");

            cboType.Items.Clear();
            cboType.Items.Add(new naru.db.NamedObject((long)AssociatedSurfaceMethods.PointDensity, "Point Density"));
            cboType.Items.Add(new naru.db.NamedObject((long)AssociatedSurfaceMethods.SlopeDegree, "Slope (Degrees)"));
            cboType.Items.Add(new naru.db.NamedObject((long)AssociatedSurfaceMethods.SlopePercent, "Slope (Percent)"));
            cboType.Items.Add(new naru.db.NamedObject((long)AssociatedSurfaceMethods.Roughness, "Roughness"));
            cboType.Items.Add(new naru.db.NamedObject((long)AssociatedSurfaceMethods.PointQuality3D, "3D Point Quality"));
            cboType.Items.Add(new naru.db.NamedObject((long)AssociatedSurfaceMethods.InterpolationError, "Interpolation Error"));
            cboType.SelectedIndex = cboType.Items.Add(new naru.db.NamedObject((long)AssociatedSurfaceMethods.Browse, "Unknown"));

            btnDensity.Enabled = ProjectManager.IsArcMap;
            btnRoughness.Enabled = ProjectManager.IsArcMap;

            if ((m_Assoc != null))
            {
                txtName.Text = m_Assoc.Name;
                txtProjectRaster.Text = ProjectManager.Project.GetRelativePath(m_Assoc.Raster.GISFileInfo);
                txtProjectRaster.ReadOnly = true;
                btnSlopePercent.Visible = false;
                btnDensity.Visible = false;
                btnBrowse.Visible = false;
                btnSlopeDegree.Visible = false;
                btnRoughness.Visible = false;
                txtOriginalRaster.Width = txtName.Width;

                cboType.Text = m_Assoc.AssocSurfaceType;


                //For i As Integer = 0 To cboType.Items.Count - 1
                //    If String.Compare(cboType.Items(i).ToString.Replace("(", "").Replace(")", ""), m_Assoc.AssocSurfaceType, True) = 0 Then
                //        cboType.SelectedIndex = i
                //        Exit For
                //    End If
                //Next
                cboType.Select();
            }

        }

        private AssociatedSurfaceMethods GetAssociatedSurfaceType(long nSurfaceID)
        {

            if (m_Assoc is AssocSurface)
            {
                foreach (naru.db.NamedObject item in cboType.Items)
                {
                    if (string.Compare(item.Name, m_Assoc.AssocSurfaceType, true) == 0)
                    {
                        return (AssociatedSurfaceMethods)Convert.ToInt32(item.ID);
                    }
                }
            }

            return AssociatedSurfaceMethods.Browse;

        }


        private void btnOK_Click(System.Object sender, System.EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                if (m_Assoc == null)
                {
                    if (!ImportRaster())
                    {
                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }

                    m_Assoc = new AssocSurface(txtName.Text.Trim(), ProjectManager.Project.GetAbsolutePath(txtProjectRaster.Text), cboType.Text, DEM);
                    DEM.AssocSurfaces.Add(m_Assoc.Name, m_Assoc);
                }
                else
                {
                    m_Assoc.Name = txtName.Text.Trim();
                    m_Assoc.AssocSurfaceType = cboType.Text;
                }

                ProjectManager.Project.Save();

            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex, "The associated surface failed to save to the GCD project file. The associated surface raster still exists.");
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            finally
            {
                Cursor = System.Windows.Forms.Cursors.Default;
            }

        }

        private bool ImportRaster()
        {

            var bRasterImportSuccessful = false;
            System.IO.FileInfo fiOutput = new System.IO.FileInfo(txtProjectRaster.Text);

            try
            {
                // Make sure that the destination folder exists where the associated surface will be output
                string sWorkspacePath = System.IO.Path.GetDirectoryName(txtProjectRaster.Text);
                System.IO.Directory.CreateDirectory(sWorkspacePath);


                // Create the slope surface or point density rasters
                switch (m_eMethod)
                {
                    case AssociatedSurfaceMethods.SlopeDegree:
                    case AssociatedSurfaceMethods.SlopePercent:
                    case AssociatedSurfaceMethods.PointDensity:
                    case AssociatedSurfaceMethods.Roughness:

                        switch (m_eMethod)
                        {
                            case AssociatedSurfaceMethods.SlopeDegree:
                                GCDConsoleLib.RasterOperators.SlopeDegrees(DEM.Raster, fiOutput);

                                break;
                            case AssociatedSurfaceMethods.SlopePercent:
                                GCDConsoleLib.RasterOperators.SlopePercent(DEM.Raster, fiOutput);

                                break;
                            case AssociatedSurfaceMethods.PointDensity:
                                GCDConsoleLib.RasterOperators.PointDensity(DEM.Raster, m_frmPointDensity.ucPointCloud.SelectedItem, new FileInfo(txtProjectRaster.Text), GCDConsoleLib.RasterOperators.KernelShapes.Square, 4m);

                                break;
                            case AssociatedSurfaceMethods.Roughness:
                                break;
                                //m_SurfaceRoughnessForm.CalculateRoughness(txtProjectRaster.Text, gDEMRaster)

                        }

                        // Build raster pyramids
                        if (ProjectManager.PyramidManager.AutomaticallyBuildPyramids(GCDCore.RasterPyramidManager.PyramidRasterTypes.AssociatedSurfaces))
                        {
                            ProjectManager.PyramidManager.PerformRasterPyramids(GCDCore.RasterPyramidManager.PyramidRasterTypes.AssociatedSurfaces, fiOutput);
                        }

                        break;
                    default:
                        m_ImportForm.ProcessRaster();

                        break;
                }

                bRasterImportSuccessful = true;

            }
            catch (Exception ex)
            {
                // Something went wrong. Check if the raster exists and safely attempt to clean it up if it does.
                if (fiOutput.Exists)
                {
                    try
                    {
                        GCDConsoleLib.Raster.Delete(fiOutput);
                    }
                    catch (Exception ex2)
                    {
                        Debug.Print("ERROR attempting to delete associated surface raster after an error during its creation");
                        Debug.Print(ex.Message);
                    }
                }
            }

            return bRasterImportSuccessful;

        }

        private bool ValidateForm()
        {

            // Safety check against names with only blank spaces
            txtName.Text = txtName.Text.Trim();

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please provide a name for the associated surface.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }
            else
            {
                if (!DEM.IsAssocNameUnique(txtName.Text, m_Assoc))
                {
                    MessageBox.Show("The name '" + txtName.Text + "' is already in use by another associated surface within this survey. Please choose a unique name.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtName.Select();
                    return false;
                }

            }

            if (string.IsNullOrEmpty(txtProjectRaster.Text))
            {
                if (m_eMethod == AssociatedSurfaceMethods.Browse)
                {
                    MessageBox.Show("You must either browse and select an existing raster for this associated surface, or choose to generate a slope or point density raster from the DEM Survey raster.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                if (m_Assoc == null && System.IO.File.Exists(txtProjectRaster.Text))
                {
                    MessageBox.Show("The associated surface project raster path already exists. Changing the name of the associated surface will change the raster path.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("Please select an error type to continue.", GCDCore.Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboType.Focus();
                return false;
            }

            return true;

        }


        private void btnBrowse_Click(System.Object sender, System.EventArgs e)
        {
            if (m_ImportForm == null)
            {
                m_ImportForm = new frmImportRaster(DEM.Raster, DEM, frmImportRaster.ImportRasterPurposes.AssociatedSurface, "Associated Surface");
            }

            m_ImportForm.txtName.Text = txtName.Text;
            if (m_ImportForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtName.Text = m_ImportForm.txtName.Text;
                txtOriginalRaster.Text = m_ImportForm.ucRaster.SelectedItem.GISFileInfo.FullName;
            }

        }

        private void btnSlope_Click(System.Object sender, System.EventArgs e)
        {
            SlopeTypeSelected(AssociatedSurfaceMethods.SlopePercent);
        }

        private void btnSlopeDegree_Click(System.Object sender, System.EventArgs e)
        {
            SlopeTypeSelected(AssociatedSurfaceMethods.SlopeDegree);
        }


        private void SlopeTypeSelected(AssociatedSurfaceMethods eType)
        {
            // Assign a name if the user hasn't already
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Text = string.Format("Slope {0}", (m_eMethod == AssociatedSurfaceMethods.SlopeDegree ? "Degrees" : "Percent"));
            }

            m_eMethod = eType;

            // Select the appropriate type in the dropdown box
            for (int i = 0; i <= cboType.Items.Count - 1; i++)
            {
                if (((naru.db.NamedObject)cboType.Items[i]).ID == (long)m_eMethod)
                {
                    cboType.SelectedIndex = i;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            txtOriginalRaster.Text = DEM.Raster.GISFileInfo.FullName;

            System.Windows.Forms.MessageBox.Show("The slope raster will be generated after you click OK.", GCDCore.Properties.Resources.ApplicationNameLong, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        }


        private void btnDensity_Click(System.Object sender, System.EventArgs e)
        {
            if (m_frmPointDensity == null)
            {
                m_frmPointDensity = new frmPointDensity(ProjectManager.Project.Units.VertUnit);
            }

            if (m_frmPointDensity.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    txtName.Text = "PDensity";
                }
                m_eMethod = AssociatedSurfaceMethods.PointDensity;

                //Dim sOutputRasterPath As String = GISCode.ChangeDetection.OutputManager.GetAssociatedSurfaceCopyPath(ProjectManager.ds.DEMSurvey.FindByDEMSurveyID(m_nDEMSurveyID).Name, txtName.Text, txtName.Text)
                //sOutputRasterPath = IO.Path.ChangeExtension(sOutputRasterPath, GetDefaultRasterExtension)
                //txtSource.Text = FileSystem.GetNewSafeFileName(sOutputRasterPath)
                //
                // Select the appropriate type in the dropdown box
                //
                for (int i = 0; i <= cboType.Items.Count - 1; i++)
                {
                    if (string.Compare(cboType.Items[i].ToString(), "Point Density", true) == 0)
                    {
                        cboType.SelectedIndex = i;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                txtOriginalRaster.Text = m_frmPointDensity.ucPointCloud.SelectedItem.GISFileInfo.FullName;
            }
        }


        private void txtName_TextChanged(object sender, System.EventArgs e)
        {
            if (m_Assoc == null)
            {
                txtProjectRaster.Text = ProjectManager.OutputManager.AssociatedSurfaceRasterPath(DEM.Name, txtName.Text).FullName;
            }

        }


        private void btnRoughness_Click(System.Object sender, System.EventArgs e)
        {
            //If m_SurfaceRoughnessForm Is Nothing Then
            //    Dim dReferenceResolution As Double = Math.Abs(DEMSurveyRaster.Extent.CellWidth)
            //    m_SurfaceRoughnessForm = New frmSurfaceRoughness(dReferenceResolution)
            //End If

            //If m_SurfaceRoughnessForm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            //    If String.IsNullOrEmpty(txtName.Text) Then
            //        txtName.Text = "Roughness"
            //    End If
            //    m_eMethod = AssociatedSurfaceMethods.Roughness

            //    ' Select the appropriate type in the dropdown box
            //    '
            //    For i As Integer = 0 To cboType.Items.Count - 1
            //        If String.Compare(cboType.Items(i).ToString, "Roughness", True) = 0 Then
            //            cboType.SelectedIndex = i
            //            Exit For
            //        End If
            //    Next
            //    Try
            //        txtOriginalRaster.Text = m_SurfaceRoughnessForm.ucToPCAT_Inputs.txtBox_RawPointCloudFile.Text
            //    Catch ex As Exception
            //        naru.error.ExceptionUI.HandleException(ex)
            //    End Try
            //End If

        }

        private void btnHelp_Click(System.Object sender, System.EventArgs e)
        {
            Process.Start(GCDCore.Properties.Resources.HelpBaseURL + "gcd-command-reference/gcd-project-explorer/d-dem-context-menu/iv-add-associated-surface");
        }
    }

}

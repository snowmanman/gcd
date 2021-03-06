using System.Diagnostics;
using GCDConsoleLib;
using System;
using System.Linq;
using System.Collections.Generic;
using GCDCore.Project;
using System.Windows.Forms;
using System.IO;

namespace GCDCore.UserInterface.SurveyLibrary
{
    public partial class frmPointDensity : IProjectItemForm
    {
        public readonly DEMSurvey DEM;
        public AssocSurface Assoc { get; internal set; }
        public GCDProjectItem GCDProjectItem { get { return Assoc as GCDProjectItem; } }

        private RasterOperators.KernelShapes KernelShape { get { return (RasterOperators.KernelShapes)cboNeighbourhood.SelectedItem; } }

        public frmPointDensity(DEMSurvey dem)
        {
            InitializeComponent();

            DEM = dem;

            foreach (RasterOperators.KernelShapes shape in Enum.GetValues(typeof(RasterOperators.KernelShapes)))
            {
                int index = cboNeighbourhood.Items.Add(shape);
            }
            cboNeighbourhood.SelectedIndex = 0;

            ucPointCloud.InitializeBrowseNew("Point Cloud", GCDConsoleLib.GDalGeometryType.SimpleTypes.Point);
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                FileInfo fiOutput = ProjectManager.Project.GetAbsolutePath(txtPath.Text);
                fiOutput.Directory.Create();

                RasterOperators.PointDensity(DEM.Raster, ucPointCloud.SelectedItem, fiOutput, KernelShape, valSampleDistance.Value, 
                    ProjectManager.OnProgressChange);

                Assoc = new AssocSurface(txtName.Text, fiOutput, DEM, AssocSurface.AssociatedSurfaceTypes.PointDensity);
                DEM.AssocSurfaces.Add(Assoc);
                ProjectManager.Project.Save();
                ProjectManager.AddNewProjectItemToMap(Assoc);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.None;
                GCDException.HandleException(ex, "Error generating point density associated surface raster.");
            }
        }

        private bool ValidateForm()
        {
            // Sanity check to avoid empty names
            txtName.Text = txtName.Text.Trim();

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("You must provide a name for the point density associated surface.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!DEM.IsAssocNameUnique(txtName.Text, Assoc))
            {
                MessageBox.Show("The name '" + txtName.Text + "' is already in use by another associated surface within this survey. Please choose a unique name.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Select();
                return false;
            }

            if (ucPointCloud.SelectedItem is GCDConsoleLib.Vector)
            {
                if (!UserInterface.SurveyLibrary.GISDatasetValidation.DSHasSpatialRef(ucPointCloud.SelectedItem, "feature class", "feature classes") ||
                    !UserInterface.SurveyLibrary.GISDatasetValidation.DSSpatialRefMatchesProjectWithMsgbox(ucPointCloud.SelectedItem, "feature class", "feature classes") ||
                    !UserInterface.SurveyLibrary.GISDatasetValidation.DSHorizUnitsMatchProject(ucPointCloud.SelectedItem, "feature class", "feature classes"))
                {
                    ucPointCloud.Select();
                    return false;
                }

            }
            else
            {
                MessageBox.Show("You must select a point cloud Shape File to continue.", "Missing Point Shape File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void btnHelp_Click(System.Object sender, System.EventArgs e)
        {
            Process.Start(GCDCore.Properties.Resources.HelpBaseURL + "gcd-command-reference/gcd-project-explorer/d-dem-context-menu/iv-add-associated-surface/3-deriving-point-density");
        }

        private void cboNeighbourhood_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitsNet.Units.LengthUnit lUnits = ProjectManager.Project.Units.HorizUnit;

            string label;
            if ((RasterOperators.KernelShapes)cboNeighbourhood.SelectedItem == RasterOperators.KernelShapes.Circle)
            {
                label = "Diameter";
                tTip.SetToolTip(valSampleDistance, string.Format("Diameter of the circular sample window (in {0}) over which point density is calculated", lUnits));
            }
            else
            {
                label = "Length";
                tTip.SetToolTip(valSampleDistance, string.Format("Size of the square sample window (in {0}) over which point density is calculated", lUnits));
            }

            lblDistance.Text = string.Format("{0} ({1})", label, UnitsNet.Length.GetAbbreviation(lUnits));
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtPath.Text = string.Empty;
            }
            else
            {
                txtPath.Text = ProjectManager.Project.GetRelativePath(DEM.AssocSurfacePath(txtName.Text));
            }
        }

        private void frmPointDensity_Load(object sender, EventArgs e)
        {
            btnOK.Text = Properties.Resources.CreateButtonText;

            ucPointCloud.PathChanged += OnPointCloudChanged;

            tTip.SetToolTip(txtName, "The name used to refer to this point density associated surface raster within this GCD project. It cannot be empty and must be unique among all associated surfaces for the parent DEM survey.");
            tTip.SetToolTip(txtPath, "The relative file path of the point density associated surface raster that will be generated.");
            tTip.SetToolTip(cboNeighbourhood, "The shape of moving window kernel that will be used to count points in the point cloud ShapeFile.");
            tTip.SetToolTip(valSampleDistance, "The size of the moving window. For circular kernels this is the radius and for squares this is half the width of the square. The units are determined by the linear units of the parent DEM survey raster.");
        }

        private void OnPointCloudChanged(object sender, naru.ui.PathEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Text = Path.GetFileNameWithoutExtension(e.Path.FullName);
            }
        }

        private void btnHelp_Click_1(object sender, EventArgs e)
        {
            OnlineHelp.Show(Name);
        }
    }
}

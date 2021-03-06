﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GCDCore.Project;
using GCDCore.UserInterface.Masks;
using GCDCore.Project.ProfileRoutes;

namespace GCDCore.UserInterface.ProfileRoutes
{
    public partial class frmProfileRouteProperties : Form, IProjectItemForm
    {
        public GCDCore.Project.ProfileRoutes.ProfileRoute ProfileRoute { get; internal set; }
        public GCDProjectItem GCDProjectItem { get { return ProfileRoute; } }
        public readonly GCDCore.Project.ProfileRoutes.ProfileRoute.ProfileRouteTypes ProfileRouteType;

        public frmProfileRouteProperties(ProfileRoute.ProfileRouteTypes eType, GCDCore.Project.ProfileRoutes.ProfileRoute route)
        {
            InitializeComponent();
            ProfileRoute = route;
            ProfileRouteType = eType;
        }

        private void frmProfileRouteProperties_Load(object sender, EventArgs e)
        {
            if (ProfileRouteType == ProfileRoute.ProfileRouteTypes.Transect)
            {
                Text = "Transect Profile Route Properties";
                lblStation.Text = "Station values";
            }
            else
            {
                Text = "Longitudinal Profile Route Properties";
                lblStation.Text = "Station offset values";

                Bitmap formImage = Properties.Resources.longitudinal;
                Icon = Icon.FromHandle(formImage.GetHicon());
            }

            // subscribe to the even when the user changes the input ShapeFile
            ucPolyline.PathChanged += InputShapeFileChanged;
            if (ProfileRoute == null)
            {
                cmdOK.Text = Properties.Resources.CreateButtonText;
                ucPolyline.InitializeBrowseNew("Profile Route", GCDConsoleLib.GDalGeometryType.SimpleTypes.LineString);
            }
            else
            {
                cmdOK.Text = Properties.Resources.UpdateButtonText;
                txtName.Text = ProfileRoute.Name;
                ucPolyline.InitializeExisting("Profile Route", ProfileRoute.Vector);
                ucPolyline.AddToMap += cmdAddToMap_Click;

                lblPath.Visible = false;
                txtPath.Visible = false;
                Height -= grpFeatureClass.Top - txtPath.Top;
            }

            UpdateControls(sender, e);

            MinimumSize = new System.Drawing.Size(277, Height);

            tTip.SetToolTip(txtName, "The name used to identify this profile route within this GCD project. It cannot be empty and it must be unique among all profile routes within this GCD project.");
            tTip.SetToolTip(txtPath, "The relative file path where this profile route ShapeFile will get stored.");
            tTip.SetToolTip(cboDistance, "The floating point field that identifies the distance downstream of each polyline feature in the profile route feature class.");
            tTip.SetToolTip(chkLabel, "Toggles whether a separate field is used for the display labels for each profile route feature. Unchecking this box uses the distance values as the labels.");
            tTip.SetToolTip(cboLabel, "Optional string label field that contains the string labels used to display each feature in the profile route feature class.");
        }

        private void UpdateControls(object sender, EventArgs e)
        {
            cboLabel.Enabled = chkLabel.Checked;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                if (ProfileRoute == null)
                {
                    FileInfo fiMask = ProjectManager.Project.GetAbsolutePath(txtPath.Text);
                    fiMask.Directory.Create();

                    ucPolyline.SelectedItem.Copy(fiMask);

                    string lablField = chkLabel.Checked ? cboLabel.Text : string.Empty;

                    ProfileRoute = new GCDCore.Project.ProfileRoutes.ProfileRoute(txtName.Text, fiMask, cboDistance.Text, lablField, ProfileRouteType);
                    ProjectManager.Project.ProfileRoutes.Add(ProfileRoute);
                    ProjectManager.AddNewProjectItemToMap(ProfileRoute);
                }
                else
                {
                    ProfileRoute.Name = txtName.Text;
                }

                ProjectManager.Project.Save();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                GCDException.HandleException(ex, "Error creating regular mask.");
            }
        }

        private bool ValidateForm()
        {
            // Sanity check to prevent empty names
            txtName.Text = txtName.Text.Trim();

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("You must provide a name for the profile route.", "Missing Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!ProjectManager.Project.IsProfileRouteNameUnique(txtName.Text, ProfileRoute))
            {
                MessageBox.Show("This project already contains a mask with this name. Please choose a unique name.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            if (ProfileRoute == null)
            {
                if (!(ucPolyline.SelectedItem is GCDConsoleLib.Vector))
                {
                    MessageBox.Show("You must choose a ShapeFile to continue.", Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ucPolyline.Select();
                    return false;
                }

                // Should be safe after Validate call above
                GCDConsoleLib.Vector shp = ucPolyline.SelectedItem;

                // Validate that hte user actually chose a POLYGON ShapeFile
                if (shp.GeometryType.SimpleType != GCDConsoleLib.GDalGeometryType.SimpleTypes.LineString)
                {
                    MessageBox.Show(string.Format("The selected ShapeFile appears to be of {0} geometry type. Only polyline ShapeFiles can be used as profile routes.", shp.GeometryType.SimpleType), "Invalid Geometry Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ucPolyline.Select();
                    return false;
                }

                if (!UserInterface.SurveyLibrary.GISDatasetValidation.DSHasSpatialRef(ucPolyline.SelectedItem, "feature class", "feature classes") ||
                    !UserInterface.SurveyLibrary.GISDatasetValidation.DSHorizUnitsMatchProject(ucPolyline.SelectedItem, "feature class", "feature classes"))
                {
                    ucPolyline.Select();
                    return false;
                }

                if (!SurveyLibrary.GISDatasetValidation.DSSpatialRefMatchesProject(ucPolyline.SelectedItem))
                {
                    string msg = string.Format(
                        "{0}{1}{0}If you believe that these projections are the same (or equivalent) choose \"Yes\" to continue anyway. Otherwise choose \"No\" to abort.",
                        Environment.NewLine, SurveyLibrary.GISDatasetValidation.SpatialRefNoMatchString(ucPolyline.SelectedItem, "raster", "rasters"));

                    DialogResult result = MessageBox.Show(msg, Properties.Resources.ApplicationNameLong, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.No)
                    {
                        ucPolyline.Select();
                        return false;
                    }
                }

                return true;
            }

            if (cboDistance.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a floating point field in the ShapeFile that provides distance values or uncheck the distance checkbox.", "Missing Distance Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDistance.Select();
                return false;
            }

            if (chkLabel.Checked)
            {
                if (cboLabel.SelectedIndex < 0)
                {
                    MessageBox.Show("You must select a text field in the ShapeFile that provides profile route labels or uncheck the label checkbox.", "Missing Label Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboLabel.Select();
                    return false;
                }
            }

            if (ucPolyline == null)
            {
                if (ucPolyline.SelectedItem.Features.Values.Any(x => x.IsNull(cboDistance.Text)))
                {
                    MessageBox.Show(string.Format("One or more features in the ShapeFile have null or invalid values in the {0} field. A valid distance field must possess valid floating point values for all features.", cboDistance.Text), "Invalid Distance Values", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
                txtPath.Text = string.Empty;
            else
                txtPath.Text = ProjectManager.Project.GetRelativePath(ProjectManager.GetProjectItemPath(ProjectManager.Project.ProfileRoutesFolder, "Route", txtName.Text, "shp"));
        }

        private void InputShapeFileChanged(object sender, naru.ui.PathEventArgs e)
        {
            if (ucPolyline.SelectedItem == null)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            GCDConsoleLib.Vector shapeFile = ucPolyline.SelectedItem;

            // Use the ShapeFile file name if the user hasn't specified one yet
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Text = naru.os.File.RemoveDangerousCharacters(System.IO.Path.GetFileNameWithoutExtension(shapeFile.GISFileInfo.FullName));
            }

            cboLabel.DataSource = shapeFile.Fields.Values.Where(x => x.Type.Equals(GCDConsoleLib.GDalFieldType.StringField)).ToList<GCDConsoleLib.VectorField>();
            cboDistance.DataSource = shapeFile.Fields.Values.Where(x => x.Type.Equals(GCDConsoleLib.GDalFieldType.RealField)).ToList<GCDConsoleLib.VectorField>();
            Cursor = Cursors.Default;
        }

        private void cmdAddToMap_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectManager.OnAddVectorToMap(ProfileRoute);
            }
            catch (Exception ex)
            {
                GCDException.HandleException(ex, "Error adding directional mask to the map.");
            }
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            OnlineHelp.Show(Name);
        }
    }
}

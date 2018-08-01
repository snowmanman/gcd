﻿using System;
using System.Windows.Forms;
using GCDCore.Project;
using System.Collections.Generic;
using System.Drawing;

namespace GCDCore.UserInterface.SurveyLibrary
{
    public partial class frmSurfaceProperties : Form, IProjectItemForm
    {
        public readonly GCDProjectRasterItem Surface;
        private SurveyDateTime SurveyDate { get; set; }
        private readonly naru.ui.SortableBindingList<GridViewPropertyValueItem> ItemProperties;

        public GCDProjectItem GCDProjectItem { get { return Surface; } }

        private string Noun
        {
            get
            {
                if (Surface is DEMSurvey)
                    return "DEM Survey";
                else if (Surface is ErrorSurface)
                    return "Reference Error Surface";
                else if (Surface is Surface)
                    return "Reference Surface";
                else
                    return string.Empty;
            }
        }

        public frmSurfaceProperties(GCDProjectRasterItem surface)
        {
            InitializeComponent();
            ItemProperties = new naru.ui.SortableBindingList<GridViewPropertyValueItem>();
            Surface = surface;
            if (surface is DEMSurvey)
            {
                SurveyDate = ((DEMSurvey)surface).SurveyDate;
            }
        }

        private void frmDEMProperties_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            Text = string.Format("{0} Properties", Noun);
            grpProperties.Text = string.Format("{0} Raster Properties", Noun);
            cmdOK.Text = Properties.Resources.UpdateButtonText;

            txtName.Text = Surface.Name;
            txtPath.Text = ProjectManager.Project.GetRelativePath(Surface.Raster.GISFileInfo);

            ItemProperties.Add(new GridViewPropertyValueItem("Raster Properties"));
            ItemProperties.Add(new GridViewPropertyValueItem("Cell size", UnitsNet.Length.From((double)Surface.Raster.Extent.CellWidth, Surface.Raster.Proj.HorizontalUnit).ToString()));
            ItemProperties.Add(new GridViewPropertyValueItem("Rows", Surface.Raster.Extent.Rows.ToString("N0")));
            ItemProperties.Add(new GridViewPropertyValueItem("Columns", Surface.Raster.Extent.Cols.ToString("N0")));
            ItemProperties.Add(new GridViewPropertyValueItem("Height", UnitsNet.Length.From((double)Surface.Raster.Extent.Height, Surface.Raster.Proj.HorizontalUnit).ToString()));
            ItemProperties.Add(new GridViewPropertyValueItem("Width", UnitsNet.Length.From((double)Surface.Raster.Extent.Width, Surface.Raster.Proj.HorizontalUnit).ToString()));
            ItemProperties.Add(new GridViewPropertyValueItem("Spatial Reference", Surface.Raster.Proj.Wkt));

            // Values from raster stats are optional
            try
            {
                Surface.Raster.ComputeStatistics();
                Dictionary<string, decimal> stats = Surface.Raster.GetStatistics();
                ItemProperties.Add(new GridViewPropertyValueItem("Raster Statistics"));
                ItemProperties.Add(new GridViewPropertyValueItem("Maximum raster value", stats["max"].ToString("n2")));
                ItemProperties.Add(new GridViewPropertyValueItem("Minimum raster value", stats["min"].ToString("n2")));
                ItemProperties.Add(new GridViewPropertyValueItem("Mean raster value", stats["mean"].ToString("n2")));
                ItemProperties.Add(new GridViewPropertyValueItem("Standard deviation of raster values", stats["stddev"].ToString("n2")));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error calculating statistics from {0}, {1}", Surface.Raster.GISFileInfo.FullName, ex.Message));
            }

            if (Surface is DEMSurvey)
            {
                txtSurveyDate.Text = SurveyDate is SurveyDateTime ? SurveyDate.ToString() : SurveyDateTime.NotSetString;
            }
            else
            {
                lblSurveyDate.Visible = false;
                txtSurveyDate.Visible = false;
                cmdSurveyDate.Visible = false;
                grpProperties.Top = txtSurveyDate.Top;
            }

            if (!ProjectManager.IsArcMap)
            {
                cmdAddTopMap.Visible = false;
                txtPath.Width = cmdAddTopMap.Right - txtPath.Left;
            }

            grdData.AutoGenerateColumns = false;
            grdData.ColumnHeadersVisible = false;
            grdData.DataSource = ItemProperties;
            grdData.Select();

            tTip.SetToolTip(txtName, "The name used to refer to this item within the GCD project. It cannot be empty and it must be unique among all other items of this type.");
            tTip.SetToolTip(txtPath, "The relative file path of this raster within the GCD project.");
            tTip.SetToolTip(cmdAddTopMap, "Add the raster to the current map document.");
            tTip.SetToolTip(txtSurveyDate, "The date on which the DEM survey was collected.");
            tTip.SetToolTip(cmdSurveyDate, "Configure the date on which the DEM survey was collected.");
            tTip.SetToolTip(grdData, "Properties of the raster associated with this item.");

            Cursor = Cursors.Default;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = DialogResult.None;
                return;
            }

            // Detect if name has been changed - need to update owner dictionaries
            if (string.Compare(Surface.Name, txtName.Text, false) != 0)
            {
                Surface.Name = txtName.Text;                
            }

            if (Surface is DEMSurvey)
                ((DEMSurvey)Surface).SurveyDate = SurveyDate;

            ProjectManager.Project.Save();
        }

        private bool ValidateForm()
        {
            // Sanity check to prevent empty names
            txtName.Text = txtName.Text.Trim();

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(string.Format("You must provide a name for the {0}. The name cannot be blank.", Noun), "Missing Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            bool bUnique = true;
            if (Surface is DEMSurvey)
            {
                bUnique = ProjectManager.Project.IsDEMNameUnique(txtName.Text, (DEMSurvey)Surface);
            }
            else if (Surface is Surface)
            {
                bUnique = ProjectManager.Project.IsReferenceSurfaceNameUnique(txtName.Text, (Surface)Surface);
            }
            else if (Surface is ErrorSurface)
            {
                bUnique = ((ErrorSurface)Surface).Surf.IsErrorNameUnique(txtName.Text, (ErrorSurface)Surface);
            }
            else
            {
                throw new Exception("Unhandled surface type");
            }

            if (!bUnique)
            {
                MessageBox.Show(string.Format("A {0} with this name already exists within this project. Please enter a unique name for the {0}", Noun.ToLower()), "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Select();
                return false;
            }

            return true;
        }

        private void cmdAddTopMap_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectManager.OnAddRasterToMap(Surface);
            }
            catch (Exception ex)
            {
                GCDException.HandleException(ex);
            }
        }

        private void cmdSurveyDate_Click(object sender, EventArgs e)
        {
            frmSurveyDateTime frm = new frmSurveyDateTime(SurveyDate);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                SurveyDate = frm.SurveyDateTime;
                txtSurveyDate.Text = SurveyDate is SurveyDateTime ? SurveyDate.ToString() : SurveyDateTime.NotSetString;
            }
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            OnlineHelp.Show(Name);
        }

        private void grdData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                GridViewPropertyValueItem prop = (GridViewPropertyValueItem)grdData.Rows[e.RowIndex].DataBoundItem;

                if (prop.Header)
                {
                    grdData.Rows[e.RowIndex].Cells[0].Style.Font = new Font(grdData.Font, FontStyle.Bold);
                }
                else
                {
                    grdData.Rows[e.RowIndex].Cells[0].Style.Padding = new Padding(prop.LeftPadding, 0, 0, 0);
                }                
            }
        }
    }
}

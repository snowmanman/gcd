﻿using System;
using GCDCore.Project;
using System.Windows.Forms;

namespace GCDCore.UserInterface.UtilityForms
{
    public class ucVectorInput : naru.ui.ucInput
    {
        public event BrowseVectorEventHandler BrowseVector;
        public delegate void BrowseVectorEventHandler(TextBox txtPath, naru.ui.PathEventArgs e, GCDConsoleLib.GDalGeometryType.SimpleTypes geometryType);

        private GCDConsoleLib.GDalGeometryType.SimpleTypes m_GeometryType;

        public GCDConsoleLib.Vector SelectedItem
        {
            get
            {
                if (FullPath is System.IO.FileInfo)
                {
                    return new GCDConsoleLib.Vector(FullPath);
                }
                else
                {
                    return null;
                }
            }
        }

        private GCDConsoleLib.GDalGeometryType.SimpleTypes GeometryType
        {
            get { return m_GeometryType; }
            set { m_GeometryType = value; }
        }

        public ucVectorInput()
        {
            Browse += cmdBrowse_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sNoun"></param>
        /// <param name="eBrowseType"></param>
        /// <remarks></remarks>
        public void InitializeBrowseNew(string sNoun, GCDConsoleLib.GDalGeometryType.SimpleTypes eBrowseType)
        {
            base.InitializeBrowseNew(sNoun);
            GeometryType = eBrowseType;
            BrowseVector += ProjectManager.OnBrowseVector;
        }

        public void InitializeExisting(string sNoun, GCDConsoleLib.Vector vector)
        {
            base.InitializeExisting(sNoun, vector.GISFileInfo, ProjectManager.Project.GetRelativePath(vector.GISFileInfo));
            GeometryType = vector.GeometryType.SimpleType;
        }

        public void cmdBrowse_Click(object sender, naru.ui.PathEventArgs e)
        {
            try
            {
                if (ProjectManager.IsArcMap)
                {
                    if (BrowseVector != null)
                    {
                        BrowseVector((TextBox)sender, e, m_GeometryType);
                    }
                }
                else
                {
                    naru.ui.Textbox.BrowseOpenVector(txtPath, naru.ui.UIHelpers.WrapMessageWithNoun("Browse and Select a", Noun, "ShapeFile"));
                }

                if (!string.IsNullOrEmpty(txtPath.Text) && System.IO.File.Exists(txtPath.Text))
                {
                    FullPath = new System.IO.FileInfo(txtPath.Text);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("missing spatial reference"))
                {
                    string msg = string.Format("The selected feature class appears to be missing a spatial reference. All GCD feature classes must possess a spatial reference and it must be the same spatial reference for all GIS datasets in a GCD project.");

                    GCDConsoleLib.Projection refProj = ProjectManager.Project.ReferenceProjection;
                    if (refProj != null)
                    {
                        msg += string.Format("{1}{1}If the selected feature class does in fact exist in the GCD project coordinate system, but the feature class coordinate system has not yet been defined, then" +
                                    " use a GIS 'Define Projection' geoprocessing tool to correct the problem with the selected feature class by defining the spatial reference as follows. " +
                                    "Then try importing it into the GCD again:{1}{1}{1}{0}", refProj.PrettyWkt, Environment.NewLine);
                    }

                    txtPath.Text = string.Empty;
                    MessageBox.Show(msg, "Missing Spatial Reference", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ex.Data.Contains("MultiPart"))
                {
                    MessageBox.Show("The Shapefile contains one or more multi-part features. Multi-part features are incompatible with the GCD." +
                        " Remove or split all multi-part features and then try again.", "Multi-Part Features", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPath.Text = string.Empty;
                }
                else
                    GCDException.HandleException(ex, string.Format("Error browsing to vector {0}", Noun));
            }
        }
    }
}
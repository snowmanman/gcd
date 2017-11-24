﻿using System;
using System.Linq;
using System.Windows.Forms;
using GCDCore.Project;

namespace GCDStandalone
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = GCDCore.Properties.Resources.ApplicationNameLong;

            try
            {
                GCDCore.WorkspaceManager.Initialize();
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex, "Error initializing temporary workspace.");
            }

            try
            {
                ProjectManager.CopyDeployFolder();
                ProjectManager.Init(GCDCore.Properties.Settings.Default.AutomaticPyramids);
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex, "Error setting up application files.");
            }

            ucProjectExplorer1.ProjectTreeNodeSelectionChange += UpdateMenusAndToolstrips;
            UpdateMenusAndToolstrips(sender, e);

            if (Properties.Settings.Default.LoadLastProjectAtStart)
            {
                string lastGCD = GCDCore.Properties.Settings.Default.LastUsedProjectFolder;
                if (!string.IsNullOrEmpty(lastGCD) && System.IO.Directory.Exists(lastGCD))
                {
                    string[] gcdFiles = System.IO.Directory.GetFiles(lastGCD, "*.gcd", System.IO.SearchOption.TopDirectoryOnly);
                    if (gcdFiles.Length == 1)
                    {
                        OpenGCDProject(gcdFiles[0]);
                    }
                }
            }
        }

        private void ProjectProperties_Click(object sender, EventArgs e)
        {
            try
            {
                bool bEditMode = string.Compare(((ToolStripItem)sender).Name, "projectPropertiesToolStripMenuItem", true) == 0 ||
                        string.Compare(((ToolStripItem)sender).Name, "tsiProjectProperties", true) == 0;

                GCDCore.UserInterface.Project.frmProjectProperties frm = new GCDCore.UserInterface.Project.frmProjectProperties(!bEditMode);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ucProjectExplorer1.cmdRefresh_Click(sender, e);
                }

                UpdateMenusAndToolstrips(sender, e);
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
            }
        }

        private void openGCDProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.DefaultExt = "xml";
            f.Filter = "GCD Project Files (*.gcd)|*.gcd";
            f.Title = "Open Existing GCD Project";
            f.CheckFileExists = true;
            //
            // PGB 2 May 2011 - Use the last browsed folder for project files. Note that
            // this is stored in a user setting and does not rely on the FileDialog to 
            // remember this value because the FileDialog may have been used for other purposes.
            if (!string.IsNullOrEmpty(GCDCore.Properties.Settings.Default.LastUsedProjectFolder) && System.IO.Directory.Exists(GCDCore.Properties.Settings.Default.LastUsedProjectFolder))
            {
                f.InitialDirectory = GCDCore.Properties.Settings.Default.LastUsedProjectFolder;

                // Try and find the last used project in the folder
                string[] fis = System.IO.Directory.GetFiles(GCDCore.Properties.Settings.Default.LastUsedProjectFolder, "*.gcd", System.IO.SearchOption.TopDirectoryOnly);
                if (fis.Count<string>() > 0)
                {
                    f.FileName = System.IO.Path.GetFileName(fis[0]);
                }
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OpenGCDProject(f.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error reading the GCD project file '{0}'. Ensure that the file is a valid GCD project file with valid and complete XML contents.", f.FileName), GCDCore.Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            UpdateMenusAndToolstrips(sender, e);
        }

        private void OpenGCDProject(string gcdProject)
        {
            // Set the project file path first (which will attempt to read the XML file and throw an error if anything goes wrong)
            // Then set the settings if the read was successful.
            ProjectManager.OpenProject(new System.IO.FileInfo(gcdProject));
            GCDCore.Properties.Settings.Default.LastUsedProjectFolder = System.IO.Path.GetDirectoryName(gcdProject);
            GCDCore.Properties.Settings.Default.Save();

            try
            {
                //GCDCore.Project.ProjectManager.Validate();
            }
            catch (Exception ex)
            {
                throw new Exception("Error validating GCD project", ex);
            }

            ucProjectExplorer1.cmdRefresh_Click(null, null);
        }

        private void browseGCDProjectFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectManager.Project is GCDProject)
            {
                System.Diagnostics.Process.Start(ProjectManager.Project.ProjectFile.DirectoryName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateMenusAndToolstrips(object sender, EventArgs e)
        {
            UpdateMenuItemStatus(toolStrip1.Items);
            UpdateMenuItemStatus(menuStrip1.Items);
        }

        private void UpdateMenuItemStatus(ToolStripItemCollection aMenu)
        {
            foreach (ToolStripItem subMenu in aMenu)
            {
                // Skip over separators etc
                if (!(subMenu is ToolStripMenuItem || subMenu is ToolStripButton))
                    continue;

                if ((subMenu is ToolStripMenuItem && ((ToolStripMenuItem)subMenu).HasDropDownItems)) // if subMenu has children
                {
                    switch (subMenu.Name)
                    {
                        // Skip top level menus
                        case "analysisToolStripMenuItem":
                        case "dataPreparationToolStripMenuItem":
                        case "customizeToolStripMenuItem":
                        case "helpToolStripMenuItem1":
                            return;

                        default:
                            UpdateMenuItemStatus(((ToolStripMenuItem)subMenu).DropDownItems); // Call recursive Method.
                            break;
                    }
                }
                else
                {
                    switch (subMenu.Name)
                    {
                        // Skip specific menu items here
                        case "newGCDProjectToolStripMenuItem":
                        case "openGCDProjectToolStripMenuItem":
                        case "exitToolStripMenuItem":
                        case "customizeToolStripMenuItem":
                            break; // do nothing. Always enabled.

                        // Skip specific tool strip items here
                        case "tsiNewProject":
                        case "tsiOpenProject":
                            break;

                        default:
                            subMenu.Enabled = ProjectManager.Project is GCDProject;
                            break;
                    }
                }
            }

            // Now update the tool status strip
            tssProjectPath.Text = ProjectManager.Project is GCDProject ? ProjectManager.Project.ProjectFile.FullName : string.Empty;
        }

        private void onlineGCDHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(GCDCore.Properties.Resources.HelpBaseURL);
        }

        private void gCDWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(GCDCore.Properties.Resources.GCDWebSiteURL);
        }

        private void aboutGCDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GCDCore.UserInterface.About.frmAbout frm = new GCDCore.UserInterface.About.frmAbout();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GCDCore.UserInterface.Options.frmOptions frm = new GCDCore.UserInterface.Options.frmOptions();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
            }
        }

        private void cleanRasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GCDCore.UserInterface.SurveyLibrary.frmImportRaster frm = new GCDCore.UserInterface.SurveyLibrary.frmImportRaster();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
            }
        }

        private void fISLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GCDCore.UserInterface.FISLibrary.frmFISLibrary frm = new GCDCore.UserInterface.FISLibrary.frmFISLibrary();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
            }
        }

        private void closeGCDProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectManager.CloseCurrentProject();
                ucProjectExplorer1.cmdRefresh_Click(sender, e);
                UpdateMenusAndToolstrips(sender, e);
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.O:
                        openGCDProjectToolStripMenuItem_Click(null, null);
                        break;

                    case Keys.N:
                        ProjectProperties_Click(newGCDProjectToolStripMenuItem, null);
                        break;

                    case Keys.S:
                        ucProjectExplorer1.AddDEMSurvey();
                        break;
                }
            }

            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        // show project properties if a project is open
                        if (ProjectManager.Project is GCDProject)
                            ProjectProperties_Click(projectPropertiesToolStripMenuItem, null);
                        break;
                }
            }
        }
    }
}
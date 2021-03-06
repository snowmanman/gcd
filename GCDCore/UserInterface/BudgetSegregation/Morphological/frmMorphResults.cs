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
using UnitsNet;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Globalization;

namespace GCDCore.UserInterface.BudgetSegregation.Morphological
{
    public partial class frmMorphResults : Form, IProjectItemForm
    {
        private const string EROSION_CHART_SERIES = "Erosion";
        private const string DEPOSIT_CHART_SERIES = "Deposition";
        private const string VOLOUT__CHART_SERIES = "VolOut";
        private const string ERROR___CHART_SERIES = "Error";

        private Color colDoD = Color.FromArgb(255, 0, 255);
        private Color colVIn = Color.FromArgb(180, 95, 6);
        private Color colVOu = Color.FromArgb(56, 118, 29);
        private Color colDep = Color.FromArgb(0, 0, 255);
        private Color colErr = Color.FromArgb(255, 0, 0);

 
        public readonly GCDCore.Project.Morphological.MorphologicalAnalysis Analysis;

        public GCDProjectItem GCDProjectItem { get { return Analysis; } }


        public frmMorphResults(GCDCore.Project.Morphological.MorphologicalAnalysis ma)
        {
            InitializeComponent();
            Analysis = ma;
         }

        private void frmMorphResults_Load(object sender, EventArgs e)
        {
            InitializeToolTips();

            cmdOK.Text = Properties.Resources.UpdateButtonText;

            ucDoDPropertiesGrid1.Initialize(Analysis.BS.DoD);
            ucDoDPropertiesGrid1.AddDoDProperty("Budget Segregation", Analysis.BS.Name);
            ucDoDPropertiesGrid1.AddDoDProperty("Directional Mask", Analysis.BS.Mask.Name);
            ucDoDPropertiesGrid1.AddDoDProperty("Directional Mask Field", Analysis.BS.Mask._Field);
            ucDoDPropertiesGrid1.AddDoDProperty("Mask Type", Analysis.BS.Mask.Noun);

            cboBoundaryType.Items.Add(GCDCore.Project.Morphological.MorphologicalAnalysis.FluxDirection.Input);
            cboBoundaryType.Items.Add(GCDCore.Project.Morphological.MorphologicalAnalysis.FluxDirection.Output);
            cboBoundaryType.SelectedIndex = 1;

            cboDuration.DataSource = GCDUnits.GCDDurationUnits();
            GCDUnits.SelectUnit(cboDuration, Analysis.DisplayUnits_Duration);
            valDuration.Value = (decimal)Analysis.Duration.As(Analysis.DisplayUnits_Duration);
            valPorosity.Value = Analysis.Porosity;
            valDensity.Value = Analysis.Density;
            valBoundaryFlux.Value = (decimal)Analysis.BoundaryFlux.As(ProjectManager.Project.Units.VolUnit);

            txtName.Text = Analysis.Name;
            txtPath.Text = ProjectManager.Project.GetRelativePath(Analysis.OutputFolder.FullName);

            grdData.AutoGenerateColumns = false;
            grdData.DataSource = Analysis.Units;

            cboBoundaryUnits.SelectedIndexChanged += BoundaryUnitsChanged;
            foreach (UnitsNet.Units.VolumeUnit val in Enum.GetValues(typeof(UnitsNet.Units.VolumeUnit)))
            {
                int i = cboBoundaryUnits.Items.Add(new GCDUnits.FormattedUnit<UnitsNet.Units.VolumeUnit>(val));
                if (val == ProjectManager.Project.Units.VolUnit)
                    cboBoundaryUnits.SelectedIndex = i;
            }

            cboDuration.SelectedIndexChanged += cboDuration_SelectedIndexChanged;
            valDuration.ValueChanged += valDuration_ValueChanged;
            UpdateCriticalDuration();

            cboBoundaryUnit.DataSource = new BindingList<GCDCore.Project.Morphological.MorphologicalUnit>(Analysis.Units.Where(x => !x.IsTotal).ToList());
            cboBoundaryUnit.SelectedItem = Analysis.BoundaryFluxUnit;

            valPorosity.ValueChanged += PorosityChanged;

            // Make the grid the default control
            grdData.Select();

            valBoundaryFlux.ValueChanged += MinFlux_Changed;
            cboBoundaryUnit.SelectedIndexChanged += MinFlux_Changed;
            cboBoundaryType.SelectedIndexChanged += MinFlux_Changed;
            //UpdateFormulae();

            UnitsChanged(null, null);
            cmdReset_Click(null, null);
        }

        private void InitializeToolTips()
        {
            tTip.SetToolTip(txtName, "Name of the morphological analysis. Cannot be blank and must be unique within the project.");
            tTip.SetToolTip(txtPath, Analysis.OutputFolder.FullName);
            tTip.SetToolTip(cmdBrowse, "Explorer the morphological analysis output folder.");
            tTip.SetToolTip(cmdOptions, "Configure the morphological display options.");
            tTip.SetToolTip(valPorosity, "Porosity of the bed material. A value of zero represents all materal and a value of one represents all air.");
            tTip.SetToolTip(valDensity, "The mass of material per unit volume. This is always specified in grams per cubic centimeter.");
            tTip.SetToolTip(valDuration, "Total duration over which the event occured. Specified in the units shown beside the value.");
            tTip.SetToolTip(cboDuration, "Units of time in which the duration value is specified.");
            tTip.SetToolTip(valPercentCompetent, "Proportion of the duration during which competent flows occured that could move bed material. Zero represents no competent flows and one represents 100% competent flows.");
            tTip.SetToolTip(txtCriticalDuration, "The duration of critical flows. This is the total duration multiplies by the proportion of competent flows.");
            //tTip.SetToolTip(picVIn, "");
            //tTip.SetToolTip(picVD, "");
            //tTip.SetToolTip(picVE, "");
            //tTip.SetToolTip(picVDoD, "");
            //tTip.SetToolTip(picVOut, "");
            //tTip.SetToolTip(txtVIn, "");
            //tTip.SetToolTip(txtVD, "");
            //tTip.SetToolTip(txtVE, "");
            //tTip.SetToolTip(txtVDoD, "");
            //tTip.SetToolTip(txtVOut, "");
            //tTip.SetToolTip(txtMinFlux, "");
            //tTip.SetToolTip(txtMinFluxRate, "");
            //tTip.SetToolTip(cboBoundaryUnit, "");
            //tTip.SetToolTip(cboBoundaryType, "");
            //tTip.SetToolTip(valBoundaryFlux, "");
            //tTip.SetToolTip(cboBoundaryUnits, "");
            tTip.SetToolTip(cmdReset, "Reset and impose the minimum flux boundary condition.");
            tTip.SetToolTip(cmdOK, "Update and save this morphological analysis. This also rewrites the accompanying morphological analysis spreadsheet.");
        }

        private void PorosityChanged(object sender, EventArgs e)
        {
            Analysis.Porosity = valPorosity.Value;
            UpdateMinFluxDisplay();
        }

        //private void DurationChanged(object sender, EventArgs e)
        //{
        //    Analysis.Duration = UnitsNet.Duration.From((double)valDuration.Value, Analysis.DisplayUnits_Duration);
        //    UpdateMinFluxDisplay();
        //}

        private void valDensity_ValueChanged(object sender, EventArgs e)
        {
            Analysis.Density = valDensity.Value;
            UpdateMinFluxDisplay();
        }

        /// <summary>
        /// Note that changing the duration units here only changes the value 
        /// of the numeric updown. The actual value doesn't affect the analysis calculations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            valDuration.ValueChanged -= valDuration_ValueChanged;

            UnitsNet.Units.DurationUnit newUnits = ((GCDUnits.FormattedUnit<UnitsNet.Units.DurationUnit>)cboDuration.SelectedItem).Unit;
            valDuration.Value = (decimal)Analysis.Duration.As(newUnits);

            valDuration.ValueChanged += valDuration_ValueChanged;

            UpdateCriticalDuration();
        }

        private UnitsNet.Units.DurationUnit SelectedDurationUnit { get { return ((GCDUnits.FormattedUnit<UnitsNet.Units.DurationUnit>)cboDuration.SelectedItem).Unit; } }
        private UnitsNet.Units.VolumeUnit SelectedBoundaryUnit { get { return ((GCDUnits.FormattedUnit<UnitsNet.Units.VolumeUnit>)cboBoundaryUnits.SelectedItem).Unit; } }

        private void valDuration_ValueChanged(object sender, EventArgs e)
        {
            Analysis.Duration = UnitsNet.Duration.From((double)valDuration.Value, SelectedDurationUnit);
            UpdateCriticalDuration();
        }

        private void PercentCritical_ValueChanged(object sender, EventArgs e)
        {
            Analysis.Competency = valPercentCompetent.Value;
            UpdateCriticalDuration();
        }

        private void UpdateCriticalDuration()
        {
            decimal value = 0;
            if (valPercentCompetent.Value > 0)
                value = valDuration.Value * valPercentCompetent.Value;

            txtCriticalDuration.Text = string.Format("{0:F} {1}{2}", value, cboDuration.SelectedItem.ToString(), value != 1m ? "s" : "");
            UpdateChart();
            UpdateMinFluxDisplay();
        }

        private void UnitsChanged(object sender, EventArgs e)
        {
            string abbrVol = Volume.GetAbbreviation(Analysis.DisplayUnits_Volume);
            string abbrMass = Mass.GetAbbreviation(Analysis.DisplayUnits_Mass);
            string abbrDur = Duration.GetAbbreviation(Analysis.DisplayUnits_Duration);

            lblBoundaryVolume.Text = lblBoundaryVolume.Text.Replace(")", string.Format("{0})", abbrVol));
            UpdateMinFluxDisplay();

            // This will cause the analysis to recalculate the flux volume and flux mass
            // Analysis.DisplayUnits_Volume = ((FormattedVolumeUnit)cboBoundaryUnits.SelectedItem).VolumeUnit;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                if (string.Compare(col.Name, "colFuxVolume", true) == 0)
                {
                    col.HeaderText = string.Format("{0}({1}/{2})", col.HeaderText.Substring(0, col.HeaderText.IndexOf("(")), abbrVol, abbrDur);
                }
                else if (string.Compare(col.Name, "colFluxMass", true) == 0)
                {
                    col.HeaderText = string.Format("{0}({1}/{2})", col.HeaderText.Substring(0, col.HeaderText.IndexOf("(")), abbrMass, abbrDur);
                }
                else if (col.HeaderText.ToLower().Contains("vol"))
                {
                    col.HeaderText = string.Format("{0}({1})", col.HeaderText.Substring(0, col.HeaderText.IndexOf("(")), abbrVol);
                }
            }

            Analysis.Units.ResetBindings();
            UpdateChart();
            UpdateFormulae();
        }

        private void UpdateMinFluxDisplay()
        {
            txtMinFlux.Text = string.Format("{0:#,##0.00} {1}", Analysis.ReachInputFlux.As(Analysis.DisplayUnits_Volume), UnitsNet.Volume.GetAbbreviation(Analysis.DisplayUnits_Volume));
            // Need to set back color first for read only textboxes
            // https://stackoverflow.com/questions/20688408/how-do-you-change-the-text-color-of-a-readonly-textbox
            txtMinFlux.BackColor = txtMinFlux.BackColor;
            txtMinFlux.ForeColor = Analysis.ReachInputFlux < new Volume(0) ? Color.Red : Color.Black;

            // Distribute the reach flux over the critical duration
            txtMinFluxRate.Text = string.Format("{0:#,##0.00} {1}", Analysis.ReachFluxRate, Analysis.ReachFluxRateUnits);
        }

        private void UpdateChart()
        {
            ucClassChart.UpdateChart(Analysis.OutputFolder, Analysis.Units.Where(x => !x.IsTotal), Analysis.DisplayUnits_Volume, false, true);         
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = ProjectManager.Project.GetAbsoluteDir(txtPath.Text);
            if (dir.Exists)
                System.Diagnostics.Process.Start(dir.FullName);
        }

        private void grdData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            string colName = grdData.Columns[e.ColumnIndex].HeaderText.ToLower();

            if (e.Value is Volume)
            {
                e.Value = ((Volume)e.Value).As(Analysis.DisplayUnits_Volume);
            }

            if (grdData.Rows[e.RowIndex].DataBoundItem is GCDCore.Project.Morphological.MorphologicalUnit)
            {
                Color foreColor = Color.Black;
                if (((GCDCore.Project.Morphological.MorphologicalUnit)grdData.Rows[e.RowIndex].DataBoundItem).IsTotal)
                {
                    grdData.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(grdData.Font, FontStyle.Bold);
                    // Special total row colours
                    switch (grdData.Columns[e.ColumnIndex].Name)
                    {
                        case "volChange": foreColor = colDoD; break;
                        case "colVolDeposition": foreColor = colDep; break;
                        case "colVolErosion": foreColor = colErr; break;
                        case "colVolumeIn": foreColor = colVIn; break;
                        case "colVolumeOut": foreColor = colVOu; break;
                    }
                }
                else if (grdData.Columns[e.ColumnIndex] == colVolumeIn && (double)e.Value < 0)
                {
                    foreColor = Color.Red;
                }
                else if (grdData.Columns[e.ColumnIndex] == colVolumeOut && (double)e.Value < 0)
                {
                    foreColor = Color.Red;
                }

                e.CellStyle.ForeColor = foreColor;
            }
        }

        private void NumericUpDown_Enter(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Text.Length);
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = DialogResult.None;
                return;
            }

            // The name should be the only property that is not already synchronized with the analysis object
            Analysis.Name = txtName.Text;

            try
            {
                Analysis.SaveExcelSpreadsheet();
            }
            catch (Exception ex)
            {
                ex.Data["Path"] = Analysis.Spreadsheet.FullName;
                GCDException.HandleException(ex, "Error saving morphological spreadsheet");
                DialogResult = DialogResult.None;
                return;
            }

            ProjectManager.Project.Save();
        }

        private bool ValidateForm()
        {
            // Sanity check to avoid empty names
            txtName.Text = txtName.Text.Trim();

            if (!frmMorpProperties.ValidateName(txtName, Analysis.BS, Analysis))
                return false;

            List<System.Diagnostics.Process> proc = naru.os.FileUtil.WhoIsLocking(Analysis.Spreadsheet.FullName);
            if (proc.Count > 0)
            {
                MessageBox.Show("Unable to update the morphological analysis spreadsheet because it is in use by another application." +
                    " Close all applications using this file and then attempt to save again.", "File Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void exportTablularDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog frm = new SaveFileDialog();
            frm.Title = "Save Tabular Data To File";
            frm.Filter = "Comma Separated Value (CSV) Files (*.csv)|*.csv";

            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            try
            {

                // Header row
                List<string> values = new List<string>();
                foreach (DataGridViewColumn col in grdData.Columns)
                {
                    values.Add(col.HeaderText.ToString().Replace(",", ""));
                }
                sb.AppendLine(string.Join(",", values));

                // Data rows
                foreach (DataGridViewRow dgvr in grdData.Rows)
                {
                    values = new List<string>();
                    for (int i = 0; i < grdData.Columns.Count; i++)
                    {
                        values.Add(dgvr.Cells[i].FormattedValue.ToString().Replace(",", ""));
                    }

                    sb.AppendLine(string.Join(",", values));
                }
            }
            catch (Exception ex)
            {
                GCDException.HandleException(ex, "Error serializing data table");
            }

            try
            {
                using (StreamWriter file = new System.IO.StreamWriter(frm.FileName))
                {
                    file.WriteLine(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                GCDException.HandleException(ex, "Error Writing Tabular Data To CSV File");
            }

            if (File.Exists(frm.FileName))
            {
                try
                {
                    System.Diagnostics.Process.Start(frm.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("The tabular data file was created at {0} but an error occured attempting to open the file.\n\n{1}", frm.FileName, ex.Message), Properties.Resources.ApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void MinFlux_Changed(object sender, EventArgs e)
        {
            GCDCore.Project.Morphological.MorphologicalUnit unit = cboBoundaryUnit.SelectedItem as GCDCore.Project.Morphological.MorphologicalUnit;
            GCDCore.Project.Morphological.MorphologicalAnalysis.FluxDirection eDir = (GCDCore.Project.Morphological.MorphologicalAnalysis.FluxDirection)cboBoundaryType.SelectedItem;

            Analysis.ImposeBoundaryCondition(eDir, unit, Volume.From((double)valBoundaryFlux.Value, ((GCDUnits.FormattedUnit<UnitsNet.Units.VolumeUnit>)cboBoundaryUnits.SelectedItem).Unit));
            UpdateMinFluxDisplay();
            UpdateFormulae();

            //Analysis.Units.ResetBindings();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            // Disconnect event firing    
            cboBoundaryUnit.SelectedIndexChanged -= MinFlux_Changed;
            valBoundaryFlux.ValueChanged -= MinFlux_Changed;
            cboBoundaryType.SelectedIndexChanged -= MinFlux_Changed;

            Analysis.ImposeMinimumFlux();
            cboBoundaryUnit.SelectedIndex = Analysis.Units.IndexOf(Analysis.BoundaryFluxUnit);
            valBoundaryFlux.Value = (decimal)Analysis.BoundaryFlux.As(((GCDUnits.FormattedUnit<UnitsNet.Units.VolumeUnit>)cboBoundaryUnits.SelectedItem).Unit);
            cboBoundaryType.SelectedIndex = 1;

            UpdateMinFluxDisplay();
            UpdateFormulae();

            // Re-attach event firing
            cboBoundaryUnit.SelectedIndexChanged += MinFlux_Changed;
            valBoundaryFlux.ValueChanged += MinFlux_Changed;
            cboBoundaryType.SelectedIndexChanged += MinFlux_Changed;
        }

        private void UpdateFormulae()
        {
            string sFormat = "#,##0.00";
            GCDCore.Project.Morphological.MorphologicalUnit muTotal = Analysis.Units.First(x => x.IsTotal);

            SetFormulaTextbox(txtVDoD, muTotal.VolChange.As(Analysis.DisplayUnits_Volume).ToString(sFormat), colDoD);
            SetFormulaTextbox(txtVIn, Analysis.Units[0].VolIn.As(Analysis.DisplayUnits_Volume).ToString(sFormat), colVIn);
            SetFormulaTextbox(txtVOut, muTotal.VolOut.As(Analysis.DisplayUnits_Volume).ToString(sFormat), colVOu);
            SetFormulaTextbox(txtVD, muTotal.VolDeposition.As(Analysis.DisplayUnits_Volume).ToString(sFormat), colDep);
            SetFormulaTextbox(txtVE, muTotal.VolErosion.As(Analysis.DisplayUnits_Volume).ToString(sFormat), colErr);
        }

        private void SetFormulaTextbox(TextBox txt, string text, Color col)
        {
            txt.Text = text;
            txt.BorderStyle = BorderStyle.None;
            txt.ForeColor = col;
            txt.Font = new Font(txtVDoD.Font, FontStyle.Bold);
        }

        private void cmdOptions_Click(object sender, EventArgs e)
        {
            frmMorphologicalUnits frm = new frmMorphologicalUnits(Analysis.DisplayUnits_Volume, Analysis.DisplayUnits_Mass, Analysis.DisplayUnits_Duration);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Analysis.DisplayUnits_Volume = frm.VolumeUnit;
                Analysis.DisplayUnits_Mass = frm.MassUnit;
                Analysis.DisplayUnits_Duration = frm.DurationUnit;

                UnitsChanged(sender, e);
            }
        }

        /// <summary>
        /// Update the boundary flux with the new units. This shouldn't trigger a recalculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BoundaryUnitsChanged(object sender, EventArgs e)
        {
            valBoundaryFlux.ValueChanged -= MinFlux_Changed;

            UnitsNet.Units.VolumeUnit newVolUnit = ((GCDUnits.FormattedUnit<UnitsNet.Units.VolumeUnit>)cboBoundaryUnits.SelectedItem).Unit;
            valBoundaryFlux.Value = (decimal)Analysis.BoundaryFlux.As(newVolUnit);

            valBoundaryFlux.ValueChanged += MinFlux_Changed;
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            OnlineHelp.Show(Name);
        }
    }
}

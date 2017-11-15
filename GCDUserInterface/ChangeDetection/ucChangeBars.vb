﻿Imports GCDCore.ChangeDetection
Imports GCDCore.Visualization

Namespace ChangeDetection
    Public Class ucChangeBars

        Private m_chngStats As GCDConsoleLib.DoDStats
        Private m_Viewer As ElevationChangeBarViewer
        Private m_eUnits As UnitsNet.Units.LengthUnit

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()
            m_eUnits = UnitsNet.Units.LengthUnit.Undefined

            ' Add any initialization after the InitializeComponent() call.

        End Sub

        Private Sub ChangeBarsUC_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            m_Viewer = New ElevationChangeBarViewer(chtControl, m_eUnits)

            cboType.Items.Add(New naru.db.NamedObject(ElevationChangeBarViewer.BarTypes.Area, "Areal"))
            cboType.Items.Add(New naru.db.NamedObject(ElevationChangeBarViewer.BarTypes.Volume, "Volumetric"))
            cboType.Items.Add(New naru.db.NamedObject(ElevationChangeBarViewer.BarTypes.Vertical, "Vertical Averages"))

            ' Add these handlers here so that everything is initialized properly before they fire
            AddHandler rdoAbsolute.CheckedChanged, AddressOf RefreshBarsEvent
            AddHandler cboType.SelectedIndexChanged, AddressOf RefreshBarsEvent
            cboType.SelectedIndex = 0

        End Sub

        Public Sub Initialize(chngStats As GCDConsoleLib.DoDStats, eUnits As UnitsNet.Units.LengthUnit)
            m_chngStats = chngStats
            m_eUnits = eUnits
        End Sub

        Private Sub RefreshBarsEvent(sender As Object, e As EventArgs)

            RefreshBars(m_eUnits, GCDConsoleLib.Utility.Conversion.LengthUnit2AreaUnit(m_eUnits), GCDConsoleLib.Utility.Conversion.LengthUnit2VolumeUnit(m_eUnits))
        End Sub

        Private Sub RefreshBars(linearDisplayUnits As UnitsNet.Units.LengthUnit, areaDisplayUnits As UnitsNet.Units.AreaUnit, volumeDisplayUnits As UnitsNet.Units.VolumeUnit)

            Dim eType As ElevationChangeBarViewer.BarTypes = DirectCast(Convert.ToInt32(DirectCast(cboType.SelectedItem, naru.db.NamedObject).ID), ElevationChangeBarViewer.BarTypes)

            Select Case eType
                Case ElevationChangeBarViewer.BarTypes.Area
                    m_Viewer.Refresh(m_chngStats.AreaErosion_Thresholded,
                                     m_chngStats.AreaDeposition_Thresholded,
                                     m_eUnits, eType, rdoAbsolute.Checked)

                Case ElevationChangeBarViewer.BarTypes.Volume
                    m_Viewer.Refresh(m_chngStats.VolumeErosion_Thresholded,
                                     m_chngStats.VolumeDeposition_Thresholded,
                                     m_chngStats.NetVolumeOfDifference_Thresholded,
                                     m_chngStats.VolumeErosion_Error,
                                     m_chngStats.VolumeDeposition_Error,
                                     m_chngStats.NetVolumeOfDifference_Error,
                                     m_eUnits, eType, rdoAbsolute.Checked)

                Case ElevationChangeBarViewer.BarTypes.Vertical
                    m_Viewer.Refresh(m_chngStats.AverageDepthErosion_Thresholded,
                                     m_chngStats.AverageDepthDeposition_Thresholded,
                                     m_chngStats.AverageThicknessOfDifferenceADC_Thresholded,
                                     m_chngStats.AverageThicknessOfDifferenceADC_Error,
                                     m_chngStats.AverageDepthErosion_Error,
                                     m_chngStats.AverageNetThicknessOfDifferenceADC_Error,
                                     m_eUnits, eType, rdoAbsolute.Checked)

            End Select

        End Sub

    End Class

End Namespace
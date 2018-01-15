﻿using System;

namespace GCDAddIn.Help
{
    public class btnOnlineHelp : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            try
            {
                System.Diagnostics.Process.Start(GCDCore.Properties.Resources.HelpBaseURL);
            }
            catch (Exception ex)
            {
                naru.error.ExceptionUI.HandleException(ex);
            }

            ArcMap.Application.CurrentTool = null;
        }
    }
}

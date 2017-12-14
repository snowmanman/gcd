﻿using GCDConsoleLib;
using GCDConsoleLib.GCD;
using System.IO;
using GCDCore.Project;

namespace GCDCore.Engines
{
    public class ChangeDetectionEngineMinLoD : ChangeDetectionEngineBase
    {
        public decimal Threshold { get; internal set; }

        public ChangeDetectionEngineMinLoD(DEMSurvey NewDEM, DEMSurvey OldDEM, decimal fThreshold)
            : base(NewDEM, OldDEM)
        {
            Threshold = fThreshold;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawDoD"></param>
        /// <param name="thrDoDPath"></param>
        /// <returns></returns>
        /// <remarks>Let the base class build the pyramids for the thresholded raster</remarks>
        protected override Raster ThresholdRawDoD(Raster rawDoD, FileInfo thrDoDPath)
        {
            return RasterOperators.SetNull(rawDoD, RasterOperators.ThresholdOps.GreaterThanOrEqual, -Threshold, RasterOperators.ThresholdOps.LessThanOrEqual, Threshold, thrDoDPath);
        }

        protected override DoDStats CalculateChangeStats(Raster rawDoD, Raster thrDoD, UnitGroup units)
        {
            return RasterOperators.GetStatsMinLoD(rawDoD, thrDoD, Threshold, units);
        }

        protected override DoDBase GetDoDResult(string dodName, DoDStats changeStats, Raster rawDoD, Raster thrDoD, HistogramPair histograms, FileInfo summaryXML)
        {
            return new DoDMinLoD(dodName, rawDoD.GISFileInfo.Directory, NewDEM, OldDEM, rawDoD, thrDoD, histograms, summaryXML, Threshold, changeStats);
        }
    }
}
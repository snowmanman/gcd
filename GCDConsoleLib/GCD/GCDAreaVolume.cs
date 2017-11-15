﻿using System;
using UnitsNet;
using UnitsNet.Units;

namespace GCDConsoleLib.GCD
{
    public class GCDAreaVolume
    {
        public int Count { get; private set; }
        // _sum has an implied unit so we really don't want to expose it.
        // it is a float because the rasters get read as a float
        private float _sum { get; set; }

        /// <summary>
        /// Initialize the Count and Sum to 0
        /// </summary>
        /// <param name="cellArea"></param>
        /// <param name="vUnit"></param>
        public GCDAreaVolume()
        {
            Count = 0;
            _sum = 0;
        }

        /// <summary>
        /// Initialize the Count and Sum to Values
        /// </summary>
        /// <param name="count"></param>
        /// <param name="sum"></param>
        /// <param name="cellArea"></param>
        /// <param name="vUnit"></param>
        public GCDAreaVolume(int count, float sum)
        {
            Count = count;
            _sum = sum;
        }

        public GCDAreaVolume(Area ar, Volume vol, Area cellArea)
        {
            SetArea(ar, cellArea);
            SetVolume(vol, cellArea);
        }

        /// <summary>
        /// Add to the Sum and increment the counter
        /// </summary>
        /// <param name="val"></param>
        public void AddToSumAndIncrementCounter(float val) { _sum += val; Count++; }

        /// <summary>
        /// Increment the Counter
        /// </summary>
        /// <param name="num"></param>
        public void IncrementCount(int num) { Count++; }

        /// <summary>
        /// Add to the sum 
        /// </summary>
        /// <param name="val"></param>
        public void AddToSum(float val) { _sum += val; }

        /// <summary>
        /// Get the actual area value
        /// </summary>
        /// <param name="cellArea"></param>
        /// <returns></returns>
        public Area GetArea(Area cellArea) { return Count * cellArea; }
        public void SetArea(Area theArea, Area cellArea)
        {
            Count =(int)( theArea.SquareMeters / cellArea.SquareMeters);
        }

        /// <summary>
        /// Get the actual volume value
        /// </summary>
        /// <param name="cellArea"></param>
        /// <param name="vUnit"></param>
        /// <returns></returns>
        public Volume GetVolume(Area cellArea, LengthUnit vUnit) { return Volume.FromCubicMeters(Length.From(_sum, vUnit).Meters * cellArea.SquareMeters); }
        public void SetVolume(Volume vol, Area cellArea)
        {
            _sum = (float)(vol.CubicMeters / cellArea.SquareMeters);
        }
    }
}
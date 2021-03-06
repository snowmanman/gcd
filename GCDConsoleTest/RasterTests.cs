﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using GCDConsoleTest.Helpers;

namespace GCDConsoleLib.Tests
{

    [TestClass()]
    public class RasterTests
    {
        [TestMethod()]
        [TestCategory("Unit")]
        public void RasterInitTest()
        {
            Raster rTemplateRaster = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("Slopey980-950.tif")));
            Assert.IsFalse(rTemplateRaster.IsOpen);
            Assert.IsTrue(rTemplateRaster.Datatype.Equals(FakeRaster<float>.floatType));
            Assert.IsFalse(rTemplateRaster.IsOpen);
        }

        /// <summary>
        /// Temporary Raster Tests
        /// </summary>
        [TestMethod()]
        [TestCategory("Unit")]
        public void Raster_Temporary_Init_Test()
        {
            // First try with the using method
            Raster rOrig = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("Slopey980-950.tif")));

            using (Raster tmpRaster = new Raster(rOrig)) {
                Assert.IsTrue(tmpRaster.Extent.Equals(rOrig.Extent));
            }
            // Make sure the original file is still there
            Assert.IsTrue(rOrig.FileExists());


            // Now try explicitly cleaning it up
            Raster tmpRaster2 = new Raster(rOrig);
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void RasterInitLazyTest()
        {
            Raster rTemplateRaster = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("Slopey980-950.tif")));
            Assert.IsFalse(rTemplateRaster.IsOpen);
            Assert.IsFalse(rTemplateRaster.IsLoaded);

            // Now do somethign with the extent
            double nodataval = (double)rTemplateRaster.origNodataVal;
            Assert.IsFalse(rTemplateRaster.IsOpen);
            Assert.IsTrue(rTemplateRaster.IsLoaded);

            // Reset and try again
            rTemplateRaster = null;

            rTemplateRaster = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("Slopey980-950.tif")));
            Assert.IsFalse(rTemplateRaster.IsOpen);
            Assert.IsFalse(rTemplateRaster.IsLoaded);

            // Now do somethign with the extent
            ExtentRectangle extrect = rTemplateRaster.Extent;
            Assert.IsFalse(rTemplateRaster.IsOpen);
            Assert.IsTrue(rTemplateRaster.IsLoaded);

            // Reset and try again
            rTemplateRaster = null;

            rTemplateRaster = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("Slopey980-950.tif")));
            Assert.IsFalse(rTemplateRaster.IsOpen);
            Assert.IsFalse(rTemplateRaster.IsLoaded);

            // Now do somethign with the extent
            Projection proj = rTemplateRaster.Proj;
            Assert.IsFalse(rTemplateRaster.IsOpen);
            Assert.IsTrue(rTemplateRaster.IsLoaded);
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void BasicRasterDSCopyTest()
        {
            using (ITempDir tmp = TempDir.Create())
            {
                Raster rTemplaetRaster = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("SinWave950-980.tif")));
                rTemplaetRaster.Copy(new FileInfo(Path.Combine(tmp.Name, "CopyRasterTest.tif")));

                // Make sure we're good.
                Assert.IsTrue(File.Exists(Path.Combine(tmp.Name, "CopyRasterTest.tif")));
                Assert.IsTrue(File.Exists(Path.Combine(tmp.Name, "CopyRasterTest.tif.aux.xml")));
                Assert.IsTrue(File.Exists(Path.Combine(tmp.Name, "CopyRasterTest.tif.ovr")));
                rTemplaetRaster.UnloadDS();
                rTemplaetRaster = null;
            }
        }


        [TestMethod()]
        [TestCategory("Unit")]
        public void RasterDeleteTest()
        {
            using (ITempDir tmp = TempDir.Create())
            {
                FileInfo sSourceRater = new FileInfo(DirHelpers.GetTestRasterPath("const990.tif"));
                FileInfo sDeletePath = new FileInfo(Path.Combine(tmp.Name, "DeleteRasterTest.tif"));

                Raster rRaster = new Raster(sSourceRater);
                rRaster.Copy(sDeletePath);
                // Make sure our setup worked                
                Assert.IsTrue(sDeletePath.Exists);

                Raster rDeleteRaster = new Raster(sDeletePath);
                rDeleteRaster.Delete();

                // Make sure we're good.
                sDeletePath.Refresh();
                Assert.IsFalse(sDeletePath.Exists);
                Assert.IsFalse(File.Exists(Path.Combine(tmp.Name, "DeleteRasterTest.tif.aux.xml")));
                Assert.IsFalse(File.Exists(Path.Combine(tmp.Name, "DeleteRasterTest.tif.ovr")));
                rRaster.UnloadDS();
                rDeleteRaster.UnloadDS();
                rDeleteRaster = null;
                rRaster = null;
            }
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void RasterExtentExpandTest()
        {
            // This is kind of too simple to test.
        }


        [TestMethod()]
        [TestCategory("Unit")]
        public void ReadTest()
        {
            Raster rTempl = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("AngledSlopey950-980E.tif")));
            rTempl.Open();

            float[] fBuff = new float[1];
            rTempl.Read(0, 0, 1, 1, fBuff);
            Assert.AreEqual(fBuff[0], 964.85f);

            double[] dBuff = new double[1];
            rTempl.Read(0, 0, 1, 1, dBuff);
            Assert.AreEqual(dBuff[0], 964.8499755859375);

            int[] iBuff = new int[1];
            rTempl.Read(0, 0, 1, 1, iBuff);
            Assert.AreEqual(iBuff[0], 965);
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void WriteTest()
        {
            Raster rTempl = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("SquareValley950-980.tif")));
            using (ITempDir tmp = TempDir.Create())
            {
                Raster rOutput = new Raster(rTempl, new FileInfo(Path.Combine(tmp.Name, "ExtendedCopyRasterTestBuffer.tif")));
                rTempl.UnloadDS();
                rTempl = null;

                rOutput.Open(true);

                rOutput.Write(0, 0, 1, 1, new double[1] { 0.55 });
                rOutput.Write(0, 1, 1, 1, new float[1] { 30.135f });
                rOutput.Write(0, 2, 1, 1, new int[1] { 3 });
                rOutput.UnloadDS();
                rOutput = null;

                Raster rTest = new Raster(new FileInfo(Path.Combine(tmp.Name, "ExtendedCopyRasterTestBuffer.tif")));

                double[] dBuff = new double[1];
                rTest.Read(0, 0, 1, 1, dBuff);
                Assert.AreEqual(dBuff[0], 0.55, 0.000001);

                float[] fBuff = new float[1];
                rTest.Read(0, 1, 1, 1, fBuff);
                Assert.AreEqual(fBuff[0], 30.135f);

                int[] iBuff = new int[1];
                rTest.Read(0, 2, 1, 1, iBuff);
                Assert.AreEqual(iBuff[0], 3);

                rTest.UnloadDS();
                rTest = null;
            }
        }

        [TestMethod()]
        [TestCategory("Functional")]
        public void BuildPyramidsTest()
        {
            using (ITempDir tmp = TempDir.Create())
            {
                // Small rasters don't need pyramids 
                Raster rTempl = new Raster(new FileInfo(DirHelpers.GetTestRasterPath("SquareValley950-980.tif")));
                Raster rTemplateOutput = RasterOperators.ExtendedCopy(rTempl, new FileInfo(Path.Combine(tmp.Name, "SMALL_PyramidTest.tif")));

                rTemplateOutput.BuildPyramids("average");
                Assert.IsFalse(File.Exists(Path.Combine(tmp.Name, "SMALL_PyramidTest.tif.ovr")));


                // Big Rasters do need pyramids
                Raster rBigTempl = new Raster(new FileInfo(DirHelpers.GetTestRootPath(@"BudgetSeg\SulphurCreek\2005Dec_DEM\2005Dec_DEM.img")));
                ExtentRectangle newExtReal = rBigTempl.Extent.Buffer(1000);
                Raster rBigTemplateOutput = RasterOperators.ExtendedCopy(rBigTempl, new FileInfo(Path.Combine(tmp.Name, "BIG_PyramidTest.tif")), newExtReal);

                rBigTemplateOutput.BuildPyramids("average");
                Assert.IsTrue(File.Exists(Path.Combine(tmp.Name, "BIG_PyramidTest.tif.ovr")));
            }
        }
    }
}
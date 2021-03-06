---
title: Old Versions of GCD Software
weight: 5
---

<div class="callout alert">
  <h2>Old Versions</h2>
  <p>GCD has been through multiple iterations. The <a href="{{ site.baseurl }}/Download">current version is GCD 7</a>. However, all of the old versions can be downloaded below and still work (many with various version specific ESRI ArcGIS dependencies). We are no longer supporting any of these pre GCD 7 releases. </p>
  <p>
  Latest stable release of GCD 6: <a class="hollow button secondary" href=" http://releases.northarrowresearch.com/GCD/Desktop/2015_11_26_GCDAddIn_6_1_14.esriAddIn" ><i class="fa fa-download"></i>&nbsp;&nbsp;  GCD 6.1.14</a>
 </p>

</div>



# GCD 6

<a href="http://northarrowresearch.com"><img class="float-right" src="{{ site.baseurl }}/assets/images/logos/NA_Logo_150pxTall.png"></a> 
With GCD 6, we switched to ArcGIS Add-Ins, so we could be a little less version dependent and and facilitate users installing without administrative privileges. We also took major strides towards minimizing our depencies on ESRI's Arc Object libraries and ESRI geoprocessing to improve stability. 

GCD 6 exists in a few different flavors. The GCD 6 ArcGIS Add-In is a C# project coded in Visual Studio that makes calls to an underlying GCDCore library written in C++, as well as a [RasterMan Library](https://github.com/NorthArrowResearch/rasterman)  also written in C++. There is a command-line version of GCD that makes calls to the same C++ libraries. Given how finicky the Add-In source code is, we have not made that open source and instead just freeware.  However, the underlying libraries that all the computations and GCD functionality are based on are open source. GCD 6 was primarily programmed by [North Arrow Research](http://northarrowresearch.com), with some support from the [Wheaton ET-AL Lab](http://etal.joewheaton.org) and [ESSA Technologies](https://essa.com/explore-essa/tools/geomorphic-change-detection/).

<a class=" hollow button" href="https://sites.google.com/a/joewheaton.org/gcd/downloads"><i class="fa fa-download"></i>  Last Stable Release of GCD 6: GCD 6.1.14</a>  
<a class="hollow button" href="http://gcd6help.joewheaton.org/"><i class="fa fa-question-circle"></i>  GCD 6 Help </a>  

## RasterMan Library

<img class="float-left" src="{{ site.baseurl }}/assets/images/rasterman.png">  
The [RasterMan Library](https://github.com/NorthArrowResearch/rasterman) is used in GCD and other tools and is an open source library of common raster geoprocessing calls written in C++ and available in Win32, Win64, Ubuntu12.04 and OSX 10.10 builds. A GitHub repository of the source code can be found [here](https://github.com/NorthArrowResearch/rasterman). 

<a class="hollow button" href="https://github.com/NorthArrowResearch/rasterman"><i class="fa fa-github"></i>  RasterMan on GitHub </a>  

## GCD 6 Command Line Version

Windows 32 Bit Version of [`GCD 6 Console GCDConsole_6_1_6_x86.zip`](http://releases.northarrowresearch.com/GCD/Console/Win32/GCDConsole_6_1_6_x86.zip) - Simply type `GCD` at command line for a list of commands and syntax.

<a class=" button" href="http://releases.northarrowresearch.com/GCD/Console/Win32/GCDConsole_6_1_6_x86.zip"><i class="fa https://fontawesome.com/v4.7.0/"></i>  GCD 6 Command Line </a> 

NOTE: *Command line  ships with GCD 6 Add-In

------

# GCD 5

<a href="https://essa.com/explore-essa/tools/geomorphic-change-detection/"><img class="float-right" src="{{ site.baseurl }}/assets/images/logos/essa_logo_blank.png"></a>  
The GCD 5 was a complete redesign of the GUI, and it was refactored into C# by [ESSA Technologies](https://essa.com/explore-essa/tools/geomorphic-change-detection/). 
GCD 5 is an ArcGIS Plug-In and is therefore, unfortunately, version specific to ArcGIS ([GCD 5.0.35](http://gcd.joewheaton.org/downloads/release-notes/5035-10july2012) and earlier works with 10.0.x and [GCD 5.2.07](http://gcd.joewheaton.org/downloads/release-notes/520713jan2014) works with 10.1.x).  

<a class=" hollow button" href="http://gcd.joewheaton.org/downloads/release-notes/520713jan2014"><i class="fa fa-download"></i>  Last Stable Release of GCD 5: GCD 5.2.07</a>  
<a class="hollow button" href="http://gcd5help.joewheaton.org/"><i class="fa fa-question-circle"></i>  GCD 5 Help </a>  

------
# GCD 4

<img class="float-right" src="{{ site.baseurl }}/assets/images/logos/ICRRR-Logo_64.gif">  
The [GCD 4.0 Beta](https://sites.google.com/a/joewheaton.org/gcd/downloads/older-versions/gcd-4-0) was the first non-MatLAB release of the Geomorphic Change Detection software. GCD 4  was developed by [Chris Garrard](http://www.cgarrard.com/) of the [RSGIS Lab](http://www.gis.usu.edu/) at Utah State University and Joe Wheaton. Plugins to [ArcGIS 9.3](http://etal.usu.edu/GCD/GCD4/Arc9_Plugin/GCDArc9Setup.msi) and [ArcGIS 10.x](http://etal.usu.edu/GCD/GCD4/Arc10_Plugin/GCDArc10Setup.msi) as well as a Windows [Stand-alone](http://etal.usu.edu/GCD/GCD4/StandAlone/GCDWinSetup.msi) flavor were all produce. Funding for the software development was provided by the now defunct Intermountain Center for River Restoration and Rehabilitation (Note: ICRRR has been superceded by Utah State University's [Restoration Consoritum](http://restoration.usu.edu)).  

<a class="hollow button" href="https://sites.google.com/a/joewheaton.org/gcd/downloads/older-versions/gcd-4-0"><i class="fa fa-download"></i> GCD 4 - Downloads  </a>  
<a class="hollow  button" href="https://sites.google.com/a/joewheaton.org/gcd/downloads/older-versions/gcd-4-0/gcd-4-help"><i class="fa fa-question-circle"></i>  GCD 4 - Help Pages </a>  

------

# DoD 3 (Matlab) / GCD 3

For those of you Matlab hold outs out there, [Joe](http://joewheaton.org) originally coded the [GCD up in Matlab](https://github.com/joewheaton/DoD/). You can still access the source code here:

<a class="hollow button" href="https://github.com/joewheaton/DoD"><i class="fa fa-github"></i>  DoD 3 - Matlab Source Code on GitHub </a>  
<a class=" button" href="https://github.com/joewheaton/DoD/releases/tag/DoD_3.0"><i class="fa fa-download"></i>  DoD 3 - Program, Tutorials & Documentation </a>  

------
<div align="center">
	<a class="hollow button" href="{{ site.baseurl }}/Download"><i class="fa fa-chevron-circle-left"></i>  Back to GCD Downloads </a>  
	<a class="hollow button" href="{{ site.baseurl }}/"><img src="{{ site.baseurl}}/assets/images/icons/GCDAddIn.png">  Back to GCD Home </a>  
</div>
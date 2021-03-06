---
title: Budget Seggregation FAQ
---

Here is a little description of how bugdet seggregation works "under the hood". This is all internal workings that you don't need to know but it might help with the understanding:

### How does Rasterized budget seggregation work?

1. By passing a string field name (Like "Method") to the rasterization method a temporary rasterized version of the shapefile is created. The values in the raster correspond to our special `GCDFID` field. 
2. The raster is stored in a special `VectorRaster` object which contains an open dataset and a Dictionary to create a relationship between our `GCDFID` field and the value of the field we want to use as a mask
3. This `VectorRaster` object is passed to the Budget Seggregation app which iterates cell-by-cell and aggregates values into bins named after the string values of the fields specified in step 1.
4. The algorithm returns the dictionary of Stats objects.



### Why do I see `GCDFID` in my shapefile when I add it to a project?

The [ESRI Shapefile specification](https://www.esri.com/library/whitepapers/pdfs/shapefile.pdf) allows for inconsistent handling of FID fields. Combining that with limitations in the GDaL Rasterization algorithm, in order to do Rasterization of an ESRI shapefile using the FID field we need an internally consistent (to GCD) unique ID field to identify shapes in our workflow.

------
<div align="center">
	<a class="hollow button" href="{{ site.baseurl }}/Help"><i class="fa fa-chevron-circle-left"></i>  Back to GCD Help </a>  
	<a class="hollow button" href="{{ site.baseurl }}/"><img src="{{ site.baseurl}}/assets/images/icons/GCDAddIn.png">  Back to GCD Home </a>  
</div>
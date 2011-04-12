using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using Emgu.CV;
using Emgu.CV.Structure;
using Point = System.Drawing.Point;


namespace QAliber.ImageHandling
{
	/// <summary>
	/// Filters an image so only a range of hues will be included in the filtered image
	/// </summary>
	public class BoxDetectorFilter
	{
		private readonly Bitmap image;
		private readonly double minHue;
		private readonly double maxHue;


		/// <summary>
		/// Constructs the filter
		/// </summary>
		/// <param name="image">The image to be filtered</param>
		public BoxDetectorFilter(Bitmap image, double minHue, double maxHue)
		{
			this.image = image;
			this.minHue = minHue;
			this.maxHue = maxHue;
		}

		/// <summary>
		/// Do the actual filtering
		/// </summary>
		/// <returns>A filtered bitmap</returns>
		public List<PointF[]> Filter()
		{
			List<PointF[]> boxesFound = new List<PointF[]>();
			using (var origImage = new Image<Gray, Byte>(image))
			{

				Gray cannyThreshold = new Gray(minHue);
				Gray cannyThresholdLinking = new Gray(maxHue);


				using (var cannyEdges = origImage.Canny(cannyThreshold, cannyThresholdLinking))
				{

					var boxList = new List<MCvBox2D>();
					using (MemStorage storage = new MemStorage())
					{
						for (Contour<Point> contours = cannyEdges.FindContours();
							 contours != null;
							 contours = contours.HNext)
						{
							Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter*0.05, storage);

							if (contours.Area > 700) //only consider contours with area greater than 250
							{
								if (currentContour.Total >= 4) //The contour has 4 vertices.
								{
									boxList.Add(currentContour.GetMinAreaRect());
								}
							}
						}
					}

					foreach (var box in boxList)
					{
						boxesFound.Add(box.GetVertices());
					}


				}

			}
			return boxesFound;
		}
	}
}

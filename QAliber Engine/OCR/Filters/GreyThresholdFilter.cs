using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QAliber.ImageHandling
{
	/// <summary>
	/// Filters the gray levels og the image including only the ones between the min level and the max level
	/// </summary>
	public class GreyThresholdFilter : IFilter
	{
		private readonly Bitmap image;
		private readonly double minGray;
		private readonly double maxGray;

		/// <summary>
		/// Constructs this filter
		/// </summary>
		/// <param name="image">The image to filter</param>
		/// <param name="minGray">The minimum level of grey to include (0-255)</param>
		/// <param name="maxGray">The maximum level of grey to include (0-255)</param>
		public GreyThresholdFilter(Bitmap image, double minGray, double maxGray)
		{
			this.image = image;
			this.minGray = minGray;
			this.maxGray = maxGray;
		}

		/// <summary>
		/// Do the actual filtering
		/// </summary>
		/// <returns>A filtered bitmap</returns>
		public Bitmap Filter()
		{
			Bitmap result;
			using (var origImage = new Image<Bgr, Byte>(image))
			{
				using (var grayImage = origImage.Convert<Gray, Byte>())
				{
					var outputImage = grayImage.ThresholdBinary(new Gray(minGray), new Gray(maxGray)); 
					result = outputImage.ToBitmap();
				}
			}
			return result;
		}
	}
}

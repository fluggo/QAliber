using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QAliber.ImageHandling
{
	/// <summary>
	/// Filters an image so only a range of hues will be included in the filtered image
	/// </summary>
	public class HuePassFilter : IFilter
	{
		private readonly Bitmap image;
		private readonly double minHue;
		private readonly double maxHue;

		/// <summary>
		/// Constructs the filter
		/// </summary>
		/// <param name="image">The image to be filtered</param>
		/// <param name="minHue">The minimal hue (0-255)</param>
		/// <param name="maxHue">The maximal hue (0-255)</param>
		public HuePassFilter(Bitmap image, double minHue, double maxHue)
		{
			this.image = image;
			this.minHue = minHue;
			this.maxHue = maxHue;
		}

		/// <summary>
		/// Do the actual filtering
		/// </summary>
		/// <returns>A filtered bitmap</returns>
		public Bitmap Filter()
		{
			Bitmap result;
			using (var origImage = new Image<Hls, Byte>(image))
			{
				using (var outImage = new Image<Hls, Byte>(origImage.Width, origImage.Height))
				{
					for (int y = 0; y < origImage.Width; y++)
					{
						for (int x = 0; x < origImage.Height; x++)
						{
							if (origImage[x, y].Hue >= minHue && origImage[x, y].Hue <= maxHue)
							{
								outImage[x, y] = origImage[x, y];
							}
						}
					}
					result = outImage.ToBitmap();
				}
			}
			return result;
		}
	}
}

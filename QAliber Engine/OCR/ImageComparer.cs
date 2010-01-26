using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using QAliber.Logger;

namespace QAliber.ImageHandling
{
	/// <summary>
	/// Compare 2 bitmaps
	/// </summary>
	public class ImageComparer
	{
		/// <summary>
		/// Initializes a new instance of image comparision
		/// </summary>
		/// <param name="bmp1">The 1st image to compare</param>
		/// <param name="bmp2">The 2nd image to compare</param>
		/// <param name="tolerance">The tolerance (0-100) of different pixels percentage between the images</param>
		/// <param name="postDiff">Should the result be posted to QALiber log</param>
		public ImageComparer(Bitmap bmp1, Bitmap bmp2, int tolerance, bool postDiff)
		{
			this.tolerance = tolerance;
			this.bmp1 = bmp1;
			this.bmp2 = bmp2;
			this.postDiff = postDiff;
		}

		public ImageComparer(Bitmap bmp1, Bitmap bmp2, int tolerance)
			: this(bmp1, bmp2, tolerance, false) { }

		public ImageComparer(Bitmap bmp1, Bitmap bmp2)
			: this(bmp1, bmp2, 5, false) { }

		/// <summary>
		/// Do the actual comaprison
		/// </summary>
		/// <returns>True if the images are equal within the tolerance, otherwise false</returns>
		public bool Compare()
		{
			
			if (postDiff)
			{
				Log.Default.Image(bmp1, "First Image");
				Log.Default.Image(bmp2, "Second Image");
			}
			if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
			{
				if (postDiff)
					Log.Default.Error("Sizes are different", bmp1.Size + "\n" + bmp2.Size);
				return false;
			}
			else
			{
				double totalPixels = bmp1.Width * bmp1.Height;
				double diffPixels = 0;
				diffImage = new Bitmap(bmp1.Width, bmp1.Height);
				for (int i = 0; i < bmp1.Width; i++)
				{
					for (int j = 0; j < bmp1.Height; j++)
					{
						Color c1 = bmp1.GetPixel(i, j);
						Color c2 = bmp2.GetPixel(i, j);
						if (c1 != c2)
							diffPixels++;
						Color diffColor = Color.FromArgb(c2.ToArgb() - c1.ToArgb());
						diffImage.SetPixel(i, j, diffColor);
					}
				}
				if (diffPixels / totalPixels > (double)tolerance / 100.0)
				{
					if (postDiff)
					{
						Log.Default.Error("Images are not equal!");
						Log.Default.Image(diffImage, "Difference Image");
					}
					return false;
					
				}
			}
			return true;
		}

		private int tolerance;

		/// <summary>
		/// A percentage of allowed different pixels betwwwn the images - e.g - 5 means that 5% of the pixels can be different between the images
		/// </summary>
		public int Tolerance
		{
			get { return tolerance; }
			set { tolerance = value; }
		}

		private Bitmap diffImage;

		/// <summary>
		/// A bitmap that represents the difference between the images (substraction of pixels)
		/// </summary>
		public Bitmap DiffImage
		{
			get { return diffImage; }
			set { diffImage = value; }
		}

		private Bitmap bmp1;

		/// <summary>
		/// The 1st bitmap to compare
		/// </summary>
		public Bitmap Bitmap1
		{
			get { return bmp1; }
			set { bmp1 = value; }
		}

		private Bitmap bmp2;

		/// <summary>
		/// The 2nd bitmap to compare
		/// </summary>
		public Bitmap Bitmap2
		{
			get { return bmp2; }
			set { bmp2 = value; }
		}

		private bool postDiff;

		/// <summary>
		/// Gets or sets whether the difference will be posted to the log (if the images are equal no posting will be made
		/// </summary>
		public bool PostDifference
		{
			get { return postDiff; }
			set { postDiff = value; }
		}
	}
}

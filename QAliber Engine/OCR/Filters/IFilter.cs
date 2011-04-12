using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QAliber.ImageHandling
{
	interface IFilter
	{
		Bitmap Filter();
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace de.christianleberfinger.dotnet.pocketknife.drawing
{
    /// <summary>
    /// Region interface.
    /// </summary>
    public interface IRegion
    {
        /// <summary>
        /// The region's id.
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// Indicates whether a given point is in the current region.
        /// Also known as hit test.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <returns>A boolean value indicating whether the point is in the region.</returns>
        bool IsInRegion(PointF point);

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace de.christianleberfinger.dotnet.pocketknife.drawing
{
    /// <summary>
    ///  A region with the shape of a circle. Can be used for simple hit tests.
    /// </summary>
    public class CircularRegion : IRegion
    {
        string _ID;
        PointF _center;
        float _radius;
        float _squareRadius;

        /// <summary>
        /// Creates a new circular region.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public CircularRegion(string ID, PointF center, float radius)
        {
            this._ID = ID;
            this._center = center;
            Radius = radius;
        }

        /// <summary>
        /// The region's id.
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// The region's center.
        /// </summary>
        public PointF Center
        {
            get { return _center; }
            set { this._center = value; }
        }

        /// <summary>
        /// The region's radius.
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { 
                _radius = value;
                _squareRadius = value*value;
            }
        }

        /// <summary>
        /// Indicates whether a given point is in the current region.
        /// Also known as hit test.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <returns>A boolean value indicating whether the point is in the region.</returns>
        public bool IsInRegion(System.Drawing.PointF point)
        {
            return IsInRegion(point.X, point.Y);
        }

        /// <summary>
        /// Indicates whether a given point is in the current region.
        /// Also known as hit test.
        /// </summary>
        /// <param name="x">The point's x coordinate</param>
        /// <param name="y">The point's y coordinate</param>
        /// <returns></returns>
        public bool IsInRegion(float x, float y)
        {
            // nothing fits into an infinitely small region.
            if (_radius == 0)
                return false;

            // get the point's distance to the region's center
            float dx = Center.X - x;
            float dy = Center.Y - y;

            double squareDistance = dx * dx + dy * dy;

            return squareDistance <= _squareRadius;
        }

        /// <summary>
        /// Gets a string representation of this circular region.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("CircularRegion '{0}' with radius '{1}'", ID, Radius);
        }
    }
}

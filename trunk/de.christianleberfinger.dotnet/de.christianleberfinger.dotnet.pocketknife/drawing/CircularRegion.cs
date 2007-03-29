using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace de.christianleberfinger.dotnet.pocketknife.drawing
{
    public class CircularRegion : IRegion
    {
        string _ID;
        PointF _center;
        float _radius;
        float _squareRadius;

        public CircularRegion(PointF center, float radius)
        {
            this._center = center;
            this._radius = radius;
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public PointF Center
        {
            get { return _center; }
            set { this._center = value; }
        }

        public float Radius
        {
            get { return _radius; }
            set { 
                _radius = value;
                _squareRadius = value*value;
            }
        }

        public bool IsInRegion(System.Drawing.PointF point)
        {
            return IsInRegion(point.X, point.Y);
        }

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
    }
}

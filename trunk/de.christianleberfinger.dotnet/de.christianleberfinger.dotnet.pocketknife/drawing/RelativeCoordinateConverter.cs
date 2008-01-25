/*
 * 
 * Copyright (c) 2007 Christian Leberfinger
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN 
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace de.christianleberfinger.dotnet.pocketknife.drawing
{
    /// <summary>
    /// Generates relative coordinates from absolute ones.
    /// You can reduce the amount of events by setting pixelthreshold.
    /// It sums up the incoming coordinates and calls the move-event only after the 
    /// summed up coordinates exceed the PixelThreshold.
    /// </summary>
    public class RelativeCoordinateConverter
    {
        int _minDistance = 1;
        int _maxDistance = int.MaxValue;

        /// <summary>
        /// Defines a maximum distance to the last added coordinate.
        /// If this threshold is exceeded, the added coordinate is being ignored.
        /// </summary>
        public int MaxDistance
        {
            get { return _maxDistance; }
            set { _maxDistance = value; }
        }

        /// <summary>
        /// Threshold for reducing the event count.
        /// The event 'OnCoordinateMove' is only fired when the summed up 
        /// coordinates exceed the MinDistance.
        /// </summary>
        public int MinDistance
        {
            get { return _minDistance; }
            set { _minDistance = value; }
        }

        bool _isFirst = true;
        Point _lastCoordinate;
        Point _relativeBuffer;

        /// <summary>
        /// Handler for points
        /// </summary>
        /// <param name="newValue"></param>
        public delegate void CoordinateHandler(Point p);

        /// <summary>
        /// Is fired when a coordinate was added and the threshold was reached.
        /// </summary>
        public event CoordinateHandler OnCoordinateMove;

        /// <summary>
        /// Adds an absolute coordinate and sums it up until the threshold is reached.
        /// Then the event 'OnCoordinateMove' is fired.
        /// </summary>
        /// <param name="newValue">The absolute coordinate to add.</param>
        public void addCoordinate(Point p)
        {
            if (_isFirst)
            {
                _isFirst = false;
                _lastCoordinate = p;

                // there's no distance 'between' a single point
                return;
            }

            Size delta = new Size(p.X - _lastCoordinate.X, p.Y - _lastCoordinate.Y);

            // Verify MaxPixelThreshold
            // If this distance is exceeded, the added coordinate is being ignored.
            if (delta.Width * delta.Width + delta.Height * delta.Height > MaxDistance * MaxDistance)
            {
                _isFirst = true;
                return;
            }

            _lastCoordinate = p;
            _relativeBuffer += delta;
            
            if (_relativeBuffer.X * _relativeBuffer.X + _relativeBuffer.Y + _relativeBuffer.Y > _minDistance * _minDistance)
            {
                CoordinateHandler temp = OnCoordinateMove;
                if (temp != null)
                {
                    temp(_relativeBuffer);
                }

                _isFirst = true;
                _relativeBuffer = new Point(0, 0);
            }
        }
    }
}

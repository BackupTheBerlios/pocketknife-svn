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
    /// Does a hittest for a given list of regions. This allows you
    /// to check, in which region(s) a given point is located.
    /// HitTest knows which region were entered 
    /// </summary>
    public class HitTest
    {
        /// <summary>
        /// Represents a method that acts after a region was entered.
        /// </summary>
        /// <param name="region">The region that was entered.</param>
        public delegate void RegionEnterHandler(IRegion region);

        /// <summary>
        /// Represents a method that acts after a region was left.
        /// </summary>
        /// <param name="region">The region that was left.</param>
        public delegate void RegionLeaveHandler(IRegion region);

        /// <summary>
        /// Occurs when a region is entered.
        /// </summary>
        public event RegionEnterHandler OnRegionEnter;

        /// <summary>
        /// Occurs when a region is left.
        /// </summary>
        public event RegionLeaveHandler OnRegionLeave;

        private List<IRegion> _regions = new List<IRegion>();
        private List<IRegion> _enteredRegions = new List<IRegion>();

        /// <summary>
        /// List of Regions
        /// </summary>
        public List<IRegion> Regions
        {
            get { return _regions; }
        }

        /// <summary>
        /// Tests, if a point is inside one (or more) of the defined regions.
        /// </summary>
        /// <param name="point">A point.</param>
        public void hitTest(PointF point)
        {
            foreach (IRegion region in _regions)
            {
                if(region.IsInRegion(point))
                {
                    if(!_enteredRegions.Contains(region))
                    {
                        _enteredRegions.Add(region);
                        
                        RegionEnterHandler temp = OnRegionEnter;
                        if (temp != null)
                            temp(region);
                    }
                }
                else
                {
                    if(_enteredRegions.Contains(region))
                    {
                        _enteredRegions.Remove(region);

                        RegionLeaveHandler temp = OnRegionLeave;
                        if(temp != null)
                            temp(region);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a list of regions, that the last submitted point is located in.
        /// </summary>
        public List<IRegion> EnteredRegions
        {
            get { return _enteredRegions; }
        }

        /// <summary>
        /// Clears all information about entered regions.
        /// </summary>
        public void reset()
        {
            _enteredRegions.Clear();
        }
    }
}

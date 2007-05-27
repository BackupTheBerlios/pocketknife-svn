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


    }
}

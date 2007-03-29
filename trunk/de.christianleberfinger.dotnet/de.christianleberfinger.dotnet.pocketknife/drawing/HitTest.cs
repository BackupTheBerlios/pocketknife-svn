using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace de.christianleberfinger.dotnet.pocketknife.drawing
{
    public class HitTest
    {
        public delegate void RegionEnterHandler(IRegion region);
        public delegate void RegionLeaveHandler(IRegion region);
        public event RegionEnterHandler OnRegionEnter;
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

    }
}

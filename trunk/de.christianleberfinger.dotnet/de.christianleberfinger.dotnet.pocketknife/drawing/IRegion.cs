using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace de.christianleberfinger.dotnet.pocketknife.drawing
{
    public interface IRegion
    {
        string ID { get; set; }

        bool IsInRegion(PointF point);



    }
}

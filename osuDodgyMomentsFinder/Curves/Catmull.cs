using osuDodgyMomentsFinder;
using System.Collections.Generic;

namespace ReplayViewer.Curves
{
    public class Catmull : Curve
    {
        public Catmull() : base(BMAPI.v1.SliderType.CSpline)
        {
        }

        protected override bool Linear => true;

        protected override Vector2 Interpolate(float t)
        {
            // let's hope nobody tries to do a replay of an old map that actually has catmulls
            return Vector2.Zero;
        }
    }
}

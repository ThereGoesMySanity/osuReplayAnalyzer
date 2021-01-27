using osuDodgyMomentsFinder;
using System;
using System.Collections.Generic;

namespace ReplayViewer.Curves
{
    public class Circle : Curve
    {
        public Circle() : base(BMAPI.v1.SliderType.PSpline)
        {
        }

        protected override bool Linear => this.Points.Count != 3;

        protected override Vector2 Interpolate(float t)
        {
            if(this.Points.Count == 3)
            {
                // essentially we are just drawing a circle between two angles
                Vector2 center = this.CircleCenter(this.Points[0], this.Points[1], this.Points[2]);
                float radius = this.Distance(this.Points[0], center);
                // arctangent gives us the angles around the circle that the point is at
                float start = this.Atan2(this.Points[0] - center);
                float end = this.Atan2(this.Points[2] - center);
                float twopi = (float)(2 * Math.PI);
                // determine which direction the circle should be drawn
                // we want it so that the curve passes throught all the points
                if(this.IsClockwise(this.Points[0], this.Points[1], this.Points[2]))
                {
                    while(end < start)
                    {
                        end += twopi;
                    }
                }
                else
                {
                    while(start < end)
                    {
                        start += twopi;
                    }
                }
                t = start + (end - start) * t;
                // t is now the angle around the circle to draw
                return new Vector2((float)(Math.Cos(t) * radius), (float)(Math.Sin(t) * radius)) + center;
            }
            else
            {
                return Vector2.Zero;
            }
        }

        private Vector2 CircleCenter(Vector2 A, Vector2 B, Vector2 C)
        {
            // finds the point of a circle from three points on it's edges
            Vector2 a = new Vector2((A.X + B.X) / 2, (A.Y + B.Y) / 2);
            Vector2 u = new Vector2((A.Y - B.Y), (B.X - A.X));
            Vector2 b = new Vector2((B.X + C.X) / 2, (B.Y + C.Y) / 2);
            Vector2 v = new Vector2((B.Y - C.Y), (C.X - B.X));
            Vector2 d = new Vector2(a.X - b.X, a.Y - b.Y);
            double vu = v.X * u.Y - v.Y * u.X;
            double g = (d.X * u.Y - d.Y * u.X) / vu;
            return new Vector2(b.X + g * v.X, b.Y + g * v.Y);
        }

        private bool IsClockwise(Vector2 a, Vector2 b, Vector2 c)
        {
            // this is a cross product / shoelace formula math thing
            // just google it, it's what I did
            return a.X * b.Y - b.X * a.Y + b.X * c.Y - c.X * b.Y + c.X * a.Y - a.X * c.Y > 0;
        }
    }
}

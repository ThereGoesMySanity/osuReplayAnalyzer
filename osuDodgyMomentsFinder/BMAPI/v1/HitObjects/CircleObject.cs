﻿using System;

namespace BMAPI.v1.HitObjects
{
    public class CircleObject
    {
        public CircleObject()
        {
        }
        public CircleObject(CircleObject baseInstance)
        {
            //Copy from baseInstance
            Location = baseInstance.Location;
            Radius = baseInstance.Radius;
            StartTime = baseInstance.StartTime;
            Type = baseInstance.Type;
            Effect = baseInstance.Effect;
        }

        public Point2 Location = new Point2(0, 0);
        public double Radius = 80;
        public float StartTime
        {
            get; set;
        }
        public HitObjectType Type
        {
            get; set;
        }
        public int StackHeight { get; internal set; } = 0;
        public Point2 StackOffset(float circleSize) => new Point2(StackHeight * ((1.0f - 0.7f * (circleSize - 5) / 5) / 2) * -6.4f);
        public virtual float EndTime
        {
            get => StartTime;
            set => throw new System.NotImplementedException();
        }

        public EffectType Effect = EffectType.None;

        public virtual bool ContainsPoint(Point2 Point)
        {
            return Math.Sqrt(Math.Pow((double)Point.X - (double)Location.X, 2) + Math.Pow((double)Point.Y - (double)Location.Y, 2)) <= Radius;
        }

        public override string ToString() 
        {
            string res = "";
            if ((Type.HasFlag(HitObjectType.Circle)))
            {
                res += "Circle";
            }
            if ((Type.HasFlag(HitObjectType.Slider)))
            {
                res += "Slider";
            }
            res += " at ";
            res += StartTime + "ms";
            return res;
        }
    }
}

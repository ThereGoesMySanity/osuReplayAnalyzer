using System;

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
            BaseLocation = baseInstance.BaseLocation;
            StartTime = baseInstance.StartTime;
            Type = baseInstance.Type;
            Effect = baseInstance.Effect;
            Beatmap = baseInstance.Beatmap;
        }

        public Point2 BaseLocation = new Point2(0, 0);
        public Point2 Location => BaseLocation + StackOffset;
        public float Radius => 64f * (1.0f - 0.7f * (Beatmap.CircleSize - 5) / 5) / 2;
        public float StartTime
        {
            get; set;
        }
        public HitObjectType Type
        {
            get; set;
        }
        public Beatmap Beatmap;
        public int StackHeight { get; internal set; } = 0;
        private float _stackOffset => StackHeight * Radius * -0.1f;
        public Point2 StackOffset => new Point2(_stackOffset, _stackOffset * (Beatmap.hardRock? -1 : 1));
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

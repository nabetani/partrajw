using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partraj
{
    static class Extensions
    {
        internal static PointF Add(this PointF a, PointF b)
        {
            return new PointF(a.X + b.X, a.Y + b.Y);
        }
        internal static PointF Sub(this PointF a, PointF b)
        {
            return new PointF(a.X - b.X, a.Y - b.Y);
        }
        internal static PointF Negative(this PointF p)
        {
            return new PointF(-p.X, -p.Y);
        }
        internal static float Abs(this PointF p)
        {
            return (float)Math.Sqrt(p.Abs2());
        }
        internal static float Abs2(this PointF p)
        {
            return p.X * p.X + p.Y * p.Y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;


namespace Partraj
{
    internal class Particle
    {
        private PointF pos;
        private PointF velo;
        private readonly Particle prev;
        private readonly Color color;

        public Particle(PointF pos, PointF velo, Particle prev)
        {
            this.pos = pos;
            this.velo = velo;
            this.prev = prev;
            this.color = prev == null ? CreateColor(velo) : prev.Color;
        }

        public Particle(PointF pos, PointF velo, Particle prev, Color col)
        {
            this.pos = pos;
            this.velo = velo;
            this.prev = prev;
            this.color = col;
        }

        static private Color CreateColor(PointF v)
        {
            double dir = Math.Atan2(v.Y, v.X);
            if (double.IsNaN(dir))
            {
                dir = 0;
            }
            int e(int x)
            {
                double phase = (dir / (Math.PI * 2) * 3 + x) % 3.0;
                if (2<phase)
                {
                    return 0;
                }
                return (int)((Math.Cos( phase/2 * Math.PI*2)+1)*(255.0/2));
            };
            int red = e(0);
            int green = e(1);
            int blue = e(2);
            return Color.FromArgb(red, green, blue);
        }

        internal Particle Prev { get { return prev; } }

        internal PointF Position { get { return pos; } }

        public Color Color { get { return color; } }

        internal Particle NextParticle()
        {
            return new Particle(this.pos.Add(this.velo), this.velo, this);
        }

        internal List<Particle> Division()
        {
            double arg = Math.Atan2(velo.Y, velo.X) + Math.PI/2;
            double v0 = 1e-3;
            PointF vD = new PointF((float)(v0 * Math.Cos(arg)), (float)(v0 * Math.Sin(arg)));
            PointF vA= this.velo.Add(vD);
            PointF vB = this.velo.Add(vD.Negative());
            
            return new List<Particle>
            {
                new Particle(this.pos, vA, this, CreateColor(vA)),
                new Particle(this.pos, vB, this, CreateColor(vB))
            };
        }

        internal void AddVelo(PointF a)
        {
            this.velo.X += a.X;
            this.velo.Y += a.Y;
        }
    }
}
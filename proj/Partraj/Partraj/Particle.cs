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
        private readonly RgbType rgb;

        public Particle(PointF pos, PointF velo, Particle prev)
        {
            this.pos = pos;
            this.velo = velo;
            this.prev = prev;
            this.rgb = prev == null ? CreateColor(velo) : prev.Rgb;
        }

        private RgbType Rgb { get { return this.rgb; } }
        private Particle(PointF pos, PointF velo, Particle prev, RgbType rgb)
        {
            this.pos = pos;
            this.velo = velo;
            this.prev = prev;
            this.rgb = rgb;
        }

        static private RgbType CreateColor(PointF v)
        {
            double dir = Math.Atan2(v.Y, v.X) * 360;
            if (double.IsNaN(dir))
            {
                dir = 0;
            }
            double e(int x)
            {
                double phase = (dir / (Math.PI * 2) * 3 + x) % 3.0;
                if (2 < phase)
                {
                    return 0;
                }
                return (Math.Cos(phase / 2 * Math.PI * 2) + 1) * 0.5;
            };
            return new RgbType(e(0), e(1), e(2));
        }

        internal Particle Prev { get { return prev; } }

        internal PointF Position { get { return pos; } }

        public Color GetColor(double weight)
        {
            return this.rgb.GetColor(weight);
        }

        internal Particle NextParticle()
        {
            return new Particle(this.pos.Add(this.velo), this.velo, this);
        }

        internal List<Particle> Division()
        {
            double arg = Math.Atan2(velo.Y, velo.X) + Math.PI / 2;
            double v0 = 1e-3;
            PointF vD = new PointF((float)(v0 * Math.Cos(arg)), (float)(v0 * Math.Sin(arg)));
            PointF vA = this.velo.Add(vD);
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

        private class RgbType
        {
            double r, g, b;

            public RgbType(double r, double g, double b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            internal Color GetColor(double weight)
            {
                double w = weight * weight * weight * weight;
                w *= w;
                return Color.FromArgb(
                    (int)((r * w + (1 - w)) * 255),
                    (int)((g * w + (1 - w)) * 255),
                    (int)((b * w + (1 - w)) * 255)
                );
            }
        }
    }
}
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

        public Particle(PointF pos, PointF velo, Particle prev)
        {
            this.pos = pos;
            this.velo = velo;
            this.prev = prev;
            this.color = prev == null ? 0 : prev.color;
        }

        private RgbType Rgb { get { return Particle.CreateColor(color); } }
        internal double color;
        private Particle(PointF pos, PointF velo, Particle prev, double color)
        {
            this.pos = pos;
            this.velo = velo;
            this.prev = prev;
            this.color = color;
        }

        static private RgbType CreateColor(double dir)
        {
            double e(int x)
            {
                double phase0 = (6 + (dir / (Math.PI * 2) * 300 + x) % 6.0) % 6.0;
                double phase = phase0 % 3;
                if (phase < 1)
                {
                    return phase;
                }
                if (phase < 2)
                {
                    return 2 - phase;
                }
                return phase0<3 ? 0 : 1;
            };
            return new RgbType(e(0), e(1), e(2));
        }

        internal Particle Prev { get { return prev; } }

        internal PointF Position { get { return pos; } }

        public Color GetColor(double weight)
        {
            return this.Rgb.GetColor(weight);
        }

        internal Particle NextParticle()
        {
            return new Particle(this.pos.Add(this.velo), this.velo, this);
        }

        internal List<Particle> Division(double colSplit)
        {
            double arg = Math.Atan2(velo.Y, velo.X) + Math.PI / 2;
            double v0 = Constants.divisionV0 * (1 + (Program.Rng.NextDouble() - Program.Rng.NextDouble()) * 0.1);
            PointF vD = new PointF((float)(v0 * Math.Cos(arg)), (float)(v0 * Math.Sin(arg)));
            PointF vA = this.velo.Add(vD);
            PointF vB = this.velo.Add(vD.Negative());

            double colDelta = Math.Pow(Math.PI/10, colSplit);
            return new List<Particle>
            {
                new Particle(this.pos, vA, this, this.color+colDelta),
                new Particle(this.pos, vB, this, this.color-colDelta)
            };
        }

        internal void SpeedUp(float v)
        {
            this.velo.X *= v;
            this.velo.Y *= v;
        }

        internal void AddVelo(PointF a)
        {
            this.velo.X += a.X;
            this.velo.Y += a.Y;
        }

        private class RgbType
        {
            private readonly double r;
            private readonly double g;
            private readonly double b;

            public RgbType(double r, double g, double b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            internal Color GetColor(double weight)
            {
                double w = Math.Pow(weight, Constants.colorPower);
                return Color.FromArgb(
                    (int)((r * w) * 255),
                    (int)((g * w) * 255),
                    (int)((b * w) * 255)
                );
            }
        }
    }
}
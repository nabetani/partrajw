using System;
using System.Collections.Generic;
using System.Drawing;


namespace Partraj
{
    internal class Particle
    {
        private PointF pos;
        private PointF velo;
        private Particle prev;

        public Particle(PointF pos, PointF velo, Particle prev)
        {
            this.pos = pos;
            this.velo = velo;
            this.prev = prev;
        }

        internal Particle Prev { get { return prev; } }

        internal PointF Position { get { return pos; } }

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
                new Particle(this.pos, vA, this),
                new Particle(this.pos, vB, this)
            };
        }
    }
}
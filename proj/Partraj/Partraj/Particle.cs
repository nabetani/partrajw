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
            PointF v = new PointF(this.velo.Y, -this.velo.X);
            return new List<Particle>
            {
                new Particle(this.pos, v, this),
                new Particle(this.pos, v.Negative(), this)
            };
        }
    }
}
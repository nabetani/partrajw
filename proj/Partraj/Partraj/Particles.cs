using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;

namespace Partraj
{
    internal class Particles
    {
        List<List<Particle>> pas;
        public Particles()
        {
            pas = new List<List<Particle>>();
            pas.Add(new List<Particle>());
            Particle pa = new Particle(new PointF(0, 0), new PointF(0, 1e-3f), null);
            pas[0].Add(pa);
        }

        internal void Evolute(int t)
        {
            var lastPas = pas.Last();
            var nextPas = new List<Particle>();
            foreach(Particle last in lastPas)
            {
                Particle pa = last.NextParticle();
                nextPas.Add(pa);
            }
            pas.Add(nextPas);
            Console.WriteLine(nextPas.Count());
        }

        internal IEnumerable<Particle> ParticlesAt(int t)
        {
            return pas[t];
        }
    }
}
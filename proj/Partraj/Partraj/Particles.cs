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
            Particle pa = new Particle(new PointF(0, 0), new PointF(0, 1e-10f), null);
            pas[0].Add(pa);
        }

        internal void Evolute(int t)
        {
            var lastPas = pas.Last();
            var nextPas = new List<Particle>();
            bool division = t % 50 == 0;
            foreach(Particle last in lastPas)
            {
                if (division)
                {
                    nextPas.AddRange(last.Division());
                }
                else
                {
                    Particle pa = last.NextParticle();
                    nextPas.Add(pa);
                }
            }
            pas.Add(nextPas);
            foreach( Particle me in nextPas)
            {
                PointF a = new PointF(0, 0);
                foreach (Particle o in nextPas)
                {
                    if ( o.Position == me.Position)
                    {
                        continue;
                    }
                    PointF vec = o.Position.Sub(me.Position);
                    float dist = vec.Abs();
                    double dir = Math.Atan2(vec.Y, vec.X);
                    if (double.IsNaN(dir))
                    {
                        continue;
                    }
                    double abs0 = 1e-5 / (0.001 + dist);
                    double abs = dist < 0.3 ? -abs0 : abs0;
                    a.X += (float)(abs * Math.Cos(dir));
                    a.Y += (float)(abs * Math.Sin(dir));
                }
                me.AddVelo(a);
                me.SpeedUp((float)(1 + 1e-4));
            }
            Console.WriteLine(nextPas.Count());
        }

        internal IEnumerable<Particle> ParticlesAt(int t)
        {
            return pas[t];
        }
    }
}
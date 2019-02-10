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
            foreach (Particle last in lastPas)
            {
                //bool division = Program.Rng.Next(Constants.divisionInterval) == 0;
                bool division = t % Constants.divisionInterval == 0;
                //bool division = false;
                if (division)
                {
                    nextPas.AddRange(last.Division((double)t/Constants.maxTime));
                }
                else
                {
                    Particle pa = last.NextParticle();
                    nextPas.Add(pa);
                }
            }
            pas.Add(nextPas);
            foreach (Particle me in nextPas)
            {
                PointF a = new PointF(0, 0);
                foreach (Particle o in nextPas)
                {
                    if (o.Position == me.Position)
                    {
                        continue;
                    }
                    PointF vec = o.Position.Sub(me.Position);
                    float dist = vec.Abs();
                    double dir = Math.Atan2(vec.Y, vec.X) + Constants.rotForce;
                    if (double.IsNaN(dir))
                    {
                        continue;
                    }
                    double abs0 = Constants.forceBase * 1 / (0.1 + dist / Constants.distBase);
                    double abs1 = Constants.forceBase * 0.16 / Math.Pow(0.1 + dist / Constants.distBase, 2);
                    double abs = abs0 - abs1;
                    a.X += (float)(abs * Math.Cos(dir));
                    a.Y += (float)(abs * Math.Sin(dir));
                }
                me.AddVelo(a);
                me.SpeedUp(Constants.accelaration);
            }
            Console.WriteLine("pas:{0}, t:{1}/{2}", nextPas.Count(), t, Constants.maxTime);
        }

        internal IEnumerable<Particle> ParticlesAt(int t)
        {
            return pas[t];
        }
    }
}
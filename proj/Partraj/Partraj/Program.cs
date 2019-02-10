using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace Partraj
{
    class Program
    {
        static readonly Random rng = new Random(Constants.randomSeed);
        internal static Random Rng { get { return rng; } }

        static void Main(string[] args)
        {
            (new Program()).Run();
        }

        private void Run()
        {
            int w = 1024 * 4;
            int h = w;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.Black);
                Draw(g, w, h);
            }
            bmp.Save("../../../../../hoge.png");
        }

        private void Draw(Graphics g, int w, int h)
        {
            Particles pas = new Particles();
            const int maxTime = Constants.maxTime;
            for (int t = 0; t < maxTime; ++t)
            {
                pas.Evolute(t);
            }
            DrawParticles(pas, maxTime, g, w, h);
        }

        private void DrawParticles(Particles pas, int maxTime, Graphics g, int w, int h)
        {
            g.TranslateTransform(w / 2, h / 2);
            float z = Math.Min(w, h) / Constants.imageRange;
            g.ScaleTransform(z, z);
            if (Constants.highQuality)
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
            }
            Pen pen = new Pen(Color.Green);
            pen.StartCap = pen.EndCap = LineCap.Round;
            for (int t = 1; t < maxTime; ++t)
            {
                Console.WriteLine("{0}/{1}", t, maxTime);
                double d = t * 1.0 / (maxTime - 1);
                double colWeight = d;
                pen.Width = (float)((1 - d) * Constants.penWidth);
                foreach (Particle pa in pas.ParticlesAt(t))
                {
                    PointF cur = pa.Position;
                    PointF prev = pa.Prev.Position;
                    pen.Color = pa.GetColor(colWeight);
                    g.DrawLine(pen, cur, prev);
                }
            }
        }
    }
}

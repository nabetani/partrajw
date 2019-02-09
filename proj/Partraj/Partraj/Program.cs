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
        static void Main(string[] args)
        {
            (new Program()).Run();
        }

        private void Run()
        {
            int w = 1024;
            int h = 1024;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                Draw(g, w, h);
            }
            bmp.Save("../../../../../hoge.png");
        }

        private void Draw(Graphics g, int w, int h)
        {
            Particles pas = new Particles();
            const int maxTime = 200;
            for( int t=0; t<maxTime; ++t)
            {
                pas.Evolute(t);
            }
            DrawParticles(pas, maxTime, g, w, h);
        }

        private void DrawParticles(Particles pas, int maxTime, Graphics g, int w, int h)
        {
            g.TranslateTransform(w / 2, h / 2);
            float z = Math.Min(w, h)/2;
            g.ScaleTransform(z, z);
            g.CompositingQuality = CompositingQuality.HighQuality;
            Pen pen = new Pen(Color.Green, 1e-2f);
            for ( int t=1; t<maxTime; ++t)
            {
                foreach( Particle pa in pas.ParticlesAt(t))
                {
                    PointF cur = pa.Position;
                    PointF prev = pa.Prev.Position;
                    g.DrawLine(pen, cur, prev);
                }
            }
        }
    }
}

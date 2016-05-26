using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Vertex
    {
        public string Name;
        public int x;
        public int y;

        public bool isUsed;
    }

    public class Edge
    {
        public Vertex ver1;
        public Vertex ver2;

        public Edge(Vertex v1, Vertex v2)
        {
            ver1 = v1;
            ver2 = v2;

            v1.isUsed = true;
            v2.isUsed = true;

            ver1.y += 20;
            ver2.y += 20;
        }
    }

    class GraphOrlov
    {
        public List<Vertex> Vertexes = new List<Vertex>();
        public List<Edge> Edges = new List<Edge>();
        private int verCount;
        const int step = 80;

        public GraphOrlov(int verCount)
        {
            this.verCount = verCount;

            for (int i = 0; i < verCount; ++i)
            {
                var new_ver = new Vertex();
                new_ver.Name = "X" + (Vertexes.Count + 1);

                if (Vertexes.Count == 0)
                {
                    new_ver.x = step;
                    new_ver.y = step/2;
                }
                else
                {
                    new_ver.x = Vertexes.Last().x + step / 2;
                    new_ver.y =  step/2;
                }

                Vertexes.Add(new_ver);
            }
        }

        public void Render(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            int R = 15;
            var Pen = new Pen(Color.Black, 2);

            foreach (Vertex vertex in Vertexes.FindAll(v=>!v.isUsed))
            {
                g.DrawEllipse(Pen, new Rectangle(vertex.x - R, vertex.y - R, R*2, R*2));

                g.DrawString(vertex.Name, SystemFonts.DefaultFont, Brushes.Black, new PointF(vertex.x - R/2, vertex.y - R/2));
            }

            foreach (var edge in Edges)
            {
                g.DrawEllipse(Pen, new Rectangle(edge.ver1.x - R, edge.ver1.y - R, R*2, R*2));
                g.DrawString(edge.ver1.Name, SystemFonts.DefaultFont, Brushes.Black, new PointF(edge.ver1.x - R / 2, edge.ver1.y - R / 2));

                g.DrawEllipse(Pen, new Rectangle(edge.ver2.x - R, edge.ver2.y - R, R * 2, R * 2));
                g.DrawString(edge.ver2.Name, SystemFonts.DefaultFont, Brushes.Black, new PointF(edge.ver2.x - R / 2, edge.ver2.y - R / 2));

                Pen.EndCap = LineCap.ArrowAnchor;
                g.DrawLine(Pen, edge.ver1.x, edge.ver1.y, edge.ver2.x, edge.ver2.y);
                

            }
        }
    }
}

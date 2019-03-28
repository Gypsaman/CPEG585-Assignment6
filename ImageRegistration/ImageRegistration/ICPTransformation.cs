using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Linq;

using Mapack;

namespace ImageRegistration
{
        public class Transformation
        {
            public double A { get; set; }
            public double B { get; set; }
            public double T1 { get; set; }
            public double T2 { get; set; }

            public Transformation(double A, double B, double T1, double T2)
            {
                this.A = A;
                this.B = B;
                this.T1 = T1;
                this.T2 = T2;
            }
            public Transformation( List<Point> Shp1,  List<Point> Shp2)
            {
                Matrix a = new Matrix(4, 4);
                Matrix b = new Matrix(4,1);

                for(int i = 0; i < Shp2.Count; i++)
                {
                    Point p = Shp2[i];
                    a[0, 0] += 2 * p.X * p.X + 2 * p.Y * p.Y;
                    a[0, 1] = 0;
                    a[0, 2] += p.X;
                    a[0, 3] += p.Y;
                    a[1, 0] = 0;
                    a[1, 1] += 2 * p.X * p.X + 2 * p.Y * p.Y;
                    a[1, 2] += 2 * p.Y;
                    a[1, 3] += -2 * p.X;
                    a[2, 0] += 2 * p.X;
                    a[2, 1] += 2 * p.Y;
                    a[2, 2] += 2;
                    a[2, 3] = 0;
                    a[3, 0] += 2 * p.Y;
                    a[3, 1] += -2 * p.X;
                    a[3, 2] += 0;
                    a[3, 3] += 2;

                    b[0, 0] += 2 * (p.X * Shp1[i].X + p.Y * Shp1[i].Y);
                    b[1, 0] += 2 * (Shp1[i].X*p.Y- p.X*Shp1[i].Y);
                    b[2, 0] += 2 * Shp1[i].X;
                    b[3, 0] += 2 * Shp1[i].Y;
                }

                Matrix Res = a.Inverse * b;
                A = Res[0, 0];
                B = Res[1, 0];
                T1 = Res[2, 0];
                T2 = Res[3, 0];

            }

            public double Cost( List<Point> P1,  List<Point> P2)
            {
                if(P1.Count != P2.Count)
                    throw new ArgumentException("Shapes are not same size");
                double cost = 0;
                List<Point> p2 = Apply(P2);
                for (int i = 0; i < P1.Count; i++)
                {
                    cost += (Math.Pow(P1[i].X - p2[i].X,2) + Math.Pow(P1[i].Y - p2[i].Y,2));
                }

                return cost;
            }

            public  List<Point> Apply( List<Point> shp)
            {
                List<Point> points = new  List<Point>();
                foreach (Point pt in shp)
                {
                    double xprime = A * pt.X + B * pt.Y + T1;
                    double yprime = -B * pt.X + A * pt.Y + T2;
                    points.Add(new Point(xprime,yprime));
                }

                return points;
            }
    }
}

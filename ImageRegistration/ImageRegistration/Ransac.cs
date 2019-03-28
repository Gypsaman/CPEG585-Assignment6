using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Documents;

namespace ImageRegistration
{
    static class Ransac { 

        public static bool GetRansac(List<Point> shp1,List<Point> shp2, int MinDataPoints, int Iterations, double Threshold,
            int CloseValues)
        { 

            int iterations = 0;
            while (iterations < Iterations)
            {
                List<Point> modelPts1 = new List<Point>();
                List<Point> modelPts2 = new List<Point>();
                List<Point> orig1 = new List<Point>(shp1);
                List<Point> orig2 = new List<Point>(shp2);
                List<int> ptsIndex = RandomPoints(shp1.Count, MinDataPoints);
                foreach (var i in ptsIndex)
                {
                    modelPts1.Add(shp1[i]);
                    modelPts2.Add(shp2[i]);
                }

                Transformation maybemodel = new Transformation(modelPts1,modelPts2);
                
                for(int i =0;i < shp1.Count;i++)
                {
                    if (ptsIndex.Contains(i))
                        continue;
                    double cost = maybemodel.Cost(shp1.Where(x=>x == shp1[i]).ToList(), shp2.Where(y=>y==shp2[i]).ToList());
                    if (cost < Threshold)
                    {
                        modelPts1.Add(shp1[i]);
                        modelPts2.Add(shp2[i]);
                    }

                    if (modelPts1.Count >= CloseValues)
                    {
                        shp1.Clear();
                        shp2.Clear();
                        foreach (var pt in orig1)
                        {
                            if(modelPts1.Contains(pt))
                            shp1.Add(pt);
                        }

                        foreach (var pt in orig2)
                        {
                            if(modelPts2.Contains(pt))
                                shp2.Add(pt);
                        }
                        
                        return true;
                    }
                }
            }

            return false;
        }

        private static List<int> RandomPoints(int total, int n)
        {
            List<int> pts = new List<int>();
            Random r = new Random();
            int indx;
            while (n-- >= 0)
            {
                do
                {
                    indx = r.Next(0, total - 1);
                } while (pts.Contains(indx));
                pts.Add(indx);
            }

            return pts;


        }
    }

    static class BruteForce
    {
        public static void RemoveOutliers(List<Point> shape1, List<Point> shape2 )
        {
            if (shape1.Count != shape2.Count) throw new ArgumentException("Shapes do not have same size");

            for (int i = 0; i < shape1.Count; i++)
            {
                List<Point> shape1m = new List<Point>(shape1);
                List<Point> shape2m = new List<Point>(shape2);
                Transformation T = new Transformation(shape1m,shape2m);
                double oldcost = T.Cost(shape1m, shape2m);
                Point pt1 = shape1m[i];
                shape1m.Remove(pt1);
                Point pt2 = shape2m[i];
                shape2m.Remove(pt2);

                T = new Transformation(shape1m, shape2m);
                double newcost = T.Cost(shape1m, shape2m);
                if (newcost * 1.05 < oldcost )
                {
                    shape1.Remove(pt1);
                    shape2.Remove(pt2);
                }
            }
            

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Point> shape1;
        private List<Point> shape2;
        private List<Point> shape2T;
        private Transformation T;
        public MainWindow()
        {
            InitializeComponent();

        }

        private Polygon DrawImage(List<Point> shape, Brush brush)
        {
            Polygon p = new Polygon();
            p.Stroke = brush;
            p.StrokeThickness = 1;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Center;
          
            foreach(var pt in shape)
                p.Points.Add(pt);
            return p;
        }

        private void InitShape_Click(object sender, RoutedEventArgs e)
        {
            shape1 =  new List<Point>()
            {
                new Point(20, 30),
                new Point(120, 50),
                new Point(160, 80),
                new Point(180, 300),
                new Point(100, 220),
                new Point(50, 280),
                new Point(20, 140)
            };
            T = new Transformation(1.05,0.05,15,22);
            shape2 = T.Apply(shape1);
            shape2[2] = new Point(shape2[2].X + 10, shape2[2].Y + 3);
            shape1.Add(new Point(200,230));
            shape2.Add(new Point(270,160));
            Images1.Children.Add(DrawImage(shape1, Brushes.Blue));
            Images1.Children.Add(DrawImage(shape2, Brushes.Red));

        }

        private void ApplyTransf_Click(object sender, RoutedEventArgs e)
        {
            Transformation T1 = new Transformation(shape1,shape2);
            MessageBox.Show($"Cost =  {T1.Cost(shape1, shape2)}");
            shape2T = T1.Apply(shape2);

            Images2.Children.Add(DrawImage(shape1, Brushes.Blue));
            Images2.Children.Add(DrawImage(shape2T, Brushes.Red));

        }


        private void OutlierRemove_OnClick_Click(object sender, RoutedEventArgs e)
        {
            if (isRansac.IsChecked == true)
            {
                if (Ransac.GetRansac(shape1, shape2, 3, 10, 500, 6) == false)
                {
                    MessageBox.Show("Could not remove outliers with Ransac");
                }
            }
            else
            {
                BruteForce.RemoveOutliers(shape1, shape2);
            }

            T = new Transformation(shape1,shape2);
            shape2T = T.Apply(shape2);
            Images3.Children.Add(DrawImage(shape1, Brushes.Blue));
            Images3.Children.Add(DrawImage(shape2T, Brushes.Red));
        }
    }
}

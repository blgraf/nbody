using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nbody
{
    public partial class NBodyMain : Window
    {
        public NBodyMain()
        {
            InitializeComponent();
        }


        private async void RunSimulation(object sender, RoutedEventArgs e)
        {
            //int steps = 1_000;
            int steps = 120_000;
            //int steps = 50_000_000;
            var system = new NBodySystem();

            Debug.WriteLine("{0:f9}", system.Energy());
            Debug.WriteLine(AreEqualDouble(-0.169075164, system.Energy()));

            for (int i = 0; i < steps; i++)
            {
                if (i % 500 == 0)
                    Debug.WriteLine(i);
                await system.Advance(0.01);
                Draw(system.bodies);
            }

            Debug.WriteLine("{0:f9}", system.Energy());
            Debug.WriteLine(AreEqualDouble(-0.169087605, system.Energy()));

        }

        private void Draw(Body[] bodies)
        {
            //BodyCanvas.Children.Add(elt);
            var xOffset = BodyCanvas.RenderSize.Width / 2;
            var yOffset = BodyCanvas.RenderSize.Width / 2;
            var multiplier = 20;

            foreach (Body body in bodies)
            {
                var e = new Ellipse();
                var size = Math.Round(body.mass / 5) > 0 ? body.mass / 5 : 1;
                e.Width = size + 2;
                e.Height = size + 2;
                e.Fill = GetBrushByBody(body);
                e.Opacity = 0.7;
                body.point = e;
                if (body.line == null)
                {
                    var l = new Polyline();
                    var asize = Math.Round(body.mass / 5) > 0 ? body.mass / 5 : 1;
                    l.Width = asize;
                    l.Height = asize;
                    l.Fill = GetBrushByBody(body);
                    body.line = l;
                }
                var point = body.point;
                var line = body.line;

                Canvas.SetLeft(point, (body.x * multiplier + xOffset - point.Width/2));
                Canvas.SetTop(point, (body.y * multiplier + yOffset - point.Height / 2));
                
                BodyCanvas.Children.Add(point);

                
                body.trace.AddLast(point);

                if (body.trace.Count > 1000)
                {
                    BodyCanvas.Children.Remove(body.trace.First());
                    body.trace.RemoveFirst();
                }
            }
        }

        private Brush GetBrushByBody(Body body)
        {
            if (body.name.Equals("Sun"))
                return Brushes.Yellow;
            else if (body.name.Equals("Jupiter"))
                return Brushes.SandyBrown;
            else if (body.name.Equals("Saturn"))
                return Brushes.Gray;
            else if (body.name.Equals("Uranus"))
                return Brushes.LightBlue;
            else if (body.name.Equals("Neptune"))
                return Brushes.RoyalBlue;
            else
                return Brushes.Black;
        }

        private static bool AreEqualDouble(double expected, double actual) => Math.Abs(actual - expected) <= Math.Abs(expected * 0.00000001);

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                RunSimulation(sender, e);
            else if (e.Key == Key.Escape)
                Environment.Exit(0);
        }
    }
}

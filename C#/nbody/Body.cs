using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace nbody
{
    internal class Body
    {
        internal double x { get; set; }
        internal double y { get; set; }
        internal double z { get; set; }

        internal double vx { get; set; }
        internal double vy { get; set; }
        internal double vz { get; set; }

        internal double mass { get; set; }

        internal string name { get; set; }
        
        internal LinkedList<Ellipse> trace { get; set; }
        internal Ellipse point { get; set; }
        internal Polyline line { get; set; }

        internal Body()
        {
            trace = new LinkedList<Ellipse>();
        }

        internal Body(double x, double y, double z, double vx, double vy, double vz, double mass, string name)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.vx = vx;
            this.vy = vy;
            this.vz = vz;
            this.mass = mass;
            this.name = name;

            trace = new LinkedList<Ellipse>();
        }


        #region DefaultBodies

        internal static readonly double Pi = 3.141592653589793;
        internal static readonly double Solarmass = 4 * Pi * Pi;
        internal static readonly double DaysPeryear = 365.24;

        // Sun
        internal static Body Sun()
        {
            return new Body(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Solarmass, "Sun");
        }

        // Jupiter
        internal static Body Jupiter()
        {
            return new Body
            {
                x = 4.84143144246472090e+00,
                y = -1.16032004402742839e+00,
                z = -1.03622044471123109e-01,
                vx = 1.66007664274403694e-03 * DaysPeryear,
                vy = 7.69901118419740425e-03 * DaysPeryear,
                vz = -6.90460016972063023e-05 * DaysPeryear,
                mass = 9.54791938424326609e-04 * Solarmass,
                name = "Jupiter",
            };
        }

        // Saturn
        internal static Body Saturn()
        {
            return new Body
            {
                x = 8.34336671824457987e+00,
                y = 4.12479856412430479e+00,
                z = -4.03523417114321381e-01,
                vx = -2.76742510726862411e-03 * DaysPeryear,
                vy = 4.99852801234917238e-03 * DaysPeryear,
                vz = 2.30417297573763929e-05 * DaysPeryear,
                mass = 2.85885980666130812e-04 * Solarmass,
                name = "Saturn",
            };
        }

        // Uranus
        internal static Body Uranus()
        {
            return new Body
            {
                x = 1.28943695621391310e+01,
                y = -1.51111514016986312e+01,
                z = -2.23307578892655734e-01,
                vx = 2.96460137564761618e-03 * DaysPeryear,
                vy = 2.37847173959480950e-03 * DaysPeryear,
                vz = -2.96589568540237556e-05 * DaysPeryear,
                mass = 4.36624404335156298e-05 * Solarmass,
                name = "Uranus",
            };
        }

        // Neptune
        internal static Body Neptune()
        {
            return new Body
            {
                x = 1.53796971148509165e+01,
                y = -2.59193146099879641e+01,
                z = 1.79258772950371181e-01,
                vx = 2.68067772490389322e-03 * DaysPeryear,
                vy = 1.62824170038242295e-03 * DaysPeryear,
                vz = -9.51592254519715870e-05 * DaysPeryear,
                mass = 5.15138902046611451e-05 * Solarmass,
                name = "Neptune",
            };
        }

        #endregion


    }
}

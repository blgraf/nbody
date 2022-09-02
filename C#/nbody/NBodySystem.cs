using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nbody
{
    internal class NBodySystem
    {
        internal Body[] bodies { get; }
        private Pair[] pairs;

        internal NBodySystem()
        {
            bodies = new Body[]
            {
                Body.Sun(),
                Body.Jupiter(),
                Body.Saturn(),
                Body.Uranus(),
                Body.Neptune()
            };

            pairs = new Pair[bodies.Length * (bodies.Length - 1) / 2];
            int p = 0;
            for (int i = 0; i < bodies.Length-1; i++)
                for (int j = i+1; j < bodies.Length; j++)
                    pairs[p++] = new Pair() { BodyA = bodies[i], BodyB = bodies[j] };

            double fx = 0.0, fy = 0.0, fz = 0.0;
            foreach (var b in bodies)
            {
                fx += b.vx * b.mass;
                fy += b.vy * b.mass;
                fz += b.vz * b.mass;
            }
            var sol = bodies[0];
            sol.vx = -fx / Body.Solarmass; 
            sol.vy = -fy / Body.Solarmass; 
            sol.vz = -fz / Body.Solarmass;
        }

        internal async Task Advance(double dt)
        {
            await Task.Run(() =>
            {
                //Parallel Foreach
                foreach (var p in pairs)
                {
                    var bodyA = p.BodyA;
                    var bodyB = p.BodyB;

                    var dx = bodyA.x - bodyB.x;
                    var dy = bodyA.y - bodyB.y;
                    var dz = bodyA.z - bodyB.z;

                    var d2 = dx * dx + dy * dy + dz * dz;
                    var mag = dt / (d2 * Math.Sqrt(d2));

                    bodyA.vx -= dx * bodyB.mass * mag;
                    bodyA.vy -= dy * bodyB.mass * mag;
                    bodyA.vz -= dz * bodyB.mass * mag;

                    bodyB.vx += dx * bodyA.mass * mag;
                    bodyB.vy += dy * bodyA.mass * mag;
                    bodyB.vz += dz * bodyA.mass * mag;
                }
                foreach (var b in bodies)
                {
                    b.x += dt * b.vx;
                    b.y += dt * b.vy;
                    b.z += dt * b.vz;
                }
                Thread.Sleep(3);
            });
        }

        internal double Energy()
        {
            double e = 0.0;

            for (int i = 0; i < bodies.Length; i++)
            {
                var bodyA = bodies[i];
                e += 0.5 * bodyA.mass * (bodyA.vx * bodyA.vx + bodyA.vy * bodyA.vy + bodyA.vz * bodyA.vz);
                for (int j = i+1; j < bodies.Length; j++)
                {
                    var bodyB  = bodies[j];
                    double dx = bodyA.x - bodyB.x;
                    double dy = bodyA.y - bodyB.y;
                    double dz = bodyA.z - bodyB.z;

                    e -= (bodyA.mass * bodyB.mass) / Math.Sqrt(dx * dx + dy * dy + dz * dz);
                }
            }

            return e;
        }





    }
}

﻿using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    internal class ArchimedeanSpiral : IPointGenerator
    {
        private const double TwoPi = 2 * Math.PI;

        private readonly double distance;
        private readonly double delta;
        private double azimuth;
        private double radius;

        internal ArchimedeanSpiral(double distance = 1, double delta = Math.PI / 180)
        {
            this.distance = distance;
            this.delta = delta;
        }

        public PointF GetNextPoint()
        {
            var point = CreatePointFromPolar(radius, azimuth);
            azimuth += delta;
            radius = distance * azimuth / TwoPi;
            return point;
        }

        private static PointF CreatePointFromPolar(double r, double phi) =>
            new PointF((float)(r * Math.Cos(phi)), (float)(r * Math.Sin(phi)));
    }
}

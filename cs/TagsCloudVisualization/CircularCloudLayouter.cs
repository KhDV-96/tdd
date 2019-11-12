﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly Point center;
        private readonly IPointGenerator sequence;
        private readonly List<Rectangle> rectangles;

        internal IEnumerable<Rectangle> Rectangles => rectangles;

        public CircularCloudLayouter(Point center) :
            this(center, new ArchimedeanSpiral())
        { }

        public CircularCloudLayouter(Point center, IPointGenerator sequence)
        {
            this.center = center;
            this.sequence = sequence;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var shift = new Size(
                center.X - rectangleSize.Width / 2,
                center.Y - rectangleSize.Height / 2
            );
            while (true)
            {
                var location = Point.Truncate(sequence.GetNextPoint()) + shift;
                var rectangle = new Rectangle(location, rectangleSize);
                if (IntersectsWithOthers(rectangle))
                {
                    continue;
                }
                rectangles.Add(rectangle);
                return rectangle;
            }
        }

        private bool IntersectsWithOthers(Rectangle rectangle) =>
            rectangles.Any(rectangle.IntersectsWith);
    }
}

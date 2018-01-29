using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tanki5
{
    /// <summary>
    /// Структура, описывающая границы формы.
    /// </summary>
    public class Border
    {
        private Segment right, left, upper, bottom;

        public Border(int width, int height)
        {
            left = new Segment(new Coordinate(0, 0, 0), new Coordinate(0, height, 0));
            right = new Segment(new Coordinate(width, 0, 0), new Coordinate(width, height, 0));
            upper = new Segment(new Coordinate(0, 0, 0), new Coordinate(width, 0, 0));
            bottom = new Segment(new Coordinate(0, height, 0), new Coordinate(width, height, 0));

        }

        public void draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen p = new Pen(Color.Violet);

            g.DrawLine(p, left.start.x, left.start.y, left.end.x, left.end.y);
            g.DrawLine(p, right.start.x, right.start.y, right.end.x, right.end.y);
            g.DrawLine(p, upper.start.x, upper.start.y, upper.end.x, upper.end.y);
            g.DrawLine(p, bottom.start.x, bottom.start.y, bottom.end.x, bottom.end.y);
        }

        public bool intersectsWith(List<Segment> tankSegments)
        {
            Tanki5.Segment[] segments = new Segment[] { right, left, upper, bottom };

            foreach (var i in tankSegments)
            {
                foreach (var j in segments)
                {
                    if (Segment.intersect(i, j))
                    {
                        if ((j.start.x == 0.0 || j.end.x == 0) &&
                            (j.start.y == 0.0 || j.end.y == 0) &&
                            (j.start.x == right.start.x || j.end.x == right.end.x) &&
                            (j.start.y == bottom.start.y || j.end.y == bottom.end.y))
                        {
                            continue;
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}

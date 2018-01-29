using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tanki5
{
    /// <summary>
    /// Класс пуля.
    /// </summary>
    class Bullet
    {
        Coordinate coordinate, tempCoordinate;

        int velocity;
        int bulletOwnerIdx;
        Pen pen;

        internal struct LineDimensions
        {
            public const int x1 = 0;
            public const int y1 = 22;
            public const int x2 = 0;
            public const int y2 = 7;
        }

        public Bullet(Coordinate coordinate, int bulletOwnerIdx, Pen pen)
        {
            this.coordinate = coordinate;
            this.velocity = 10;
            this.bulletOwnerIdx = bulletOwnerIdx;
            this.pen = pen;
        }

        public void move(float time)
        {
            tempCoordinate.x = coordinate.x - (float)velocity * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) * time;
            tempCoordinate.y = coordinate.y + (float)velocity * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) * time;
            tempCoordinate.angle = coordinate.angle;
        }

        public void applyCoordinates()
        {
            coordinate = tempCoordinate;
        }

        public int getBulletOwnerIdx()
        {
            return bulletOwnerIdx;
        }

        public List<Segment> getBarrelSegment()
        {
            List<Segment> tempSegList = new List<Segment>();
            tempSegList.Add(new Segment(
                new Coordinate(
                    (float)LineDimensions.x1 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)LineDimensions.y1 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)LineDimensions.x1 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + (float)LineDimensions.y1 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    tempCoordinate.angle),
                new Coordinate(
                    (float)LineDimensions.x2 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)LineDimensions.y2 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)LineDimensions.x2 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + (float)LineDimensions.y2 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    tempCoordinate.angle)));

            return tempSegList;
        }

        public void draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.TranslateTransform(coordinate.x, coordinate.y);
            g.RotateTransform(coordinate.angle);

            g.DrawLine(pen, LineDimensions.x1, LineDimensions.y1, LineDimensions.x2, LineDimensions.y2);

            g.RotateTransform(-coordinate.angle);
            g.TranslateTransform(-coordinate.x, -coordinate.y);

        }
    }
}

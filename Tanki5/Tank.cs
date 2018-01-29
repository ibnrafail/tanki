using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tanki5
{
    class Tank
    {
        internal struct RectDimensions
        {
            public const int x = -10;
            public const int y = -15;
            public const int width = 20;
            public const int height = 30;
        }

        internal struct EllipseDimensions
        {
            public const int x = -5;
            public const int y = -7;
            public const int width = 10;
            public const int height = 14;
        }

        internal struct LineDimensions
        {
            public const int x1 = 0;
            public const int y1 = 22;
            public const int x2 = 0;
            public const int y2 = 7;
        }

        private Coordinate coordinate; /** координата танка */
        private Coordinate tempCoordinate; /** временная координата танка */

        private float velocity = 0; /** скорость танка */
        private float angularVelocity = 0; /** угловая скорость */
        private bool isForward = true; /** флаг направления движения */
        
        public bool IsForward
        {
            get { return isForward; }
            set { isForward = value; }
        }

        private Pen pen;
        public int life = 10;

        public Tank(Pen pen, float x, float y)
        {
            this.pen = pen;
            this.coordinate.x = x;
            this.coordinate.y = y;
            this.coordinate.angle = 0;
            this.tempCoordinate = this.coordinate;
        }

        /// <summary>
        /// Установить угловую скорость.
        /// </summary>
        /// <param name="angularVelocity"></param>
        public void setAngularVelocity(float angularVelocity)
        {
            this.angularVelocity = angularVelocity;
        }

        /// <summary>
        /// Установить скорость.
        /// </summary>
        /// <param name="velocity"></param>
        public void setVelocity(float velocity)
        {
            this.velocity = velocity;
        }

        public Coordinate getCoordinate()
        {
            return this.coordinate;
        }

        /// <summary>
        /// Измененить временную координату середины танка.
        /// </summary>
        /// <param name="time"></param>
        public void move(float time)
        {
            if (isForward)
            {
                tempCoordinate.x = coordinate.x - velocity * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) * time;
                tempCoordinate.y = coordinate.y + velocity * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) * time;
                
            }
            else
            {
                tempCoordinate.x = coordinate.x + velocity * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) * time;
                tempCoordinate.y = coordinate.y - velocity * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) * time;
            }
            tempCoordinate.angle = coordinate.angle + angularVelocity * time;
            //System.Console.WriteLine(tempCoordinate.angle);
        }

        /// <summary>
        /// Зафиксировать временную координату, как основную
        /// </summary>
        public void applyCoordinates()
        {
            coordinate = tempCoordinate;
        }

        /// <summary>
        /// Возвращает концевую координату дула.
        /// </summary>
        /// <returns></returns>
        public Coordinate getBarrelEndCoordinate()
        {
            return new Coordinate(
                    (float)LineDimensions.x1 * (float)Math.Cos(coordinate.angle / 180 * Math.PI) - (float)LineDimensions.y1 * (float)Math.Sin(coordinate.angle / 180 * Math.PI) + coordinate.x,
                    (float)LineDimensions.x1 * (float)Math.Sin(coordinate.angle / 180 * Math.PI) + (float)LineDimensions.y1 * (float)Math.Cos(coordinate.angle / 180 * Math.PI) + coordinate.y,
                    coordinate.angle);
        }

        /// <summary>
        /// Получить массив отрезков сторон и дула танка. 
        /// </summary>
        /// <returns></returns>
        public List<Segment> getTempSegmentsList()
        {
            List<Segment> tempSegmentsList = new List<Segment>();
            tempSegmentsList.Add(new Segment(
                    new Coordinate(
                        (float)RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                        (float)RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                        0),
                    new Coordinate(
                        (float)-RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                        (float)-RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                        0)));

            tempSegmentsList.Add(new Segment(
                new Coordinate(
                    (float)RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0),
                new Coordinate(
                    (float)-RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)-RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0)));

            tempSegmentsList.Add(new Segment(
                new Coordinate(
                    (float)RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0),
                new Coordinate(
                    (float)RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0)));

            tempSegmentsList.Add(new Segment(
                new Coordinate(
                    (float)-RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)-RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0),
                new Coordinate(
                    (float)-RectDimensions.x * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + (float)RectDimensions.y * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)-RectDimensions.x * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) - (float)RectDimensions.y * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0)));

            tempSegmentsList.Add(new Segment(
                new Coordinate(
                    (float)LineDimensions.x1 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)LineDimensions.y1 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)LineDimensions.x1 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + (float)LineDimensions.y1 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0),
                new Coordinate(
                    (float)LineDimensions.x2 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) - (float)LineDimensions.y2 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.x,
                    (float)LineDimensions.x2 * (float)Math.Cos(tempCoordinate.angle / 180 * Math.PI) + (float)LineDimensions.y2 * (float)Math.Sin(tempCoordinate.angle / 180 * Math.PI) + tempCoordinate.y,
                    0)));

            return tempSegmentsList;
        }

        /// <summary>
        /// Отобразить танк на форме.
        /// 1. Для отображения танка сначала производится установка начала координат в координату танка.
        /// 2. Затем совершается поворот системы координат на угол <c>coordinate.angle</c>.
        /// 3. После этого рисуются фигуры из которых состоит танк.
        /// 4. Система координат возвращается в исходное положение.
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TranslateTransform(coordinate.x, coordinate.y);
            g.RotateTransform(coordinate.angle);

            g.DrawRectangle(pen, RectDimensions.x, RectDimensions.y, RectDimensions.width, RectDimensions.height);
            g.DrawEllipse(pen, EllipseDimensions.x, EllipseDimensions.y, EllipseDimensions.width, EllipseDimensions.height);
            g.DrawLine(pen, LineDimensions.x1, LineDimensions.y1, LineDimensions.x2, LineDimensions.y2);


            g.RotateTransform(-coordinate.angle);
            g.TranslateTransform(-coordinate.x, -coordinate.y);
            
        }
    }
}

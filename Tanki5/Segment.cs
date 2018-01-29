using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanki5
{
    /// <summary>
    /// Структура, описывающая отрезок.
    /// </summary>
    public struct Segment
    {
        public Coordinate start;
        public Coordinate end;

        public Segment(Coordinate start, Coordinate end)
        {
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// Проверка на пересечение двух отрезков.
        /// </summary>
        /// <param name="A">отрезок A</param>
        /// <param name="B">отрезок B</param>
        /// <returns><c>true</c> если отрезки пересекаются</returns>
        /// <returns><c>false</c> если отрезки не пересекаются</returns>
        static public bool intersect(Segment A, Segment B)
        {
            Coordinate dir1 = A.end - A.start;
            Coordinate dir2 = B.end - B.start;

            float a1 = -dir1.y;
            float b1 = +dir1.x;
            float d1 = -(a1 * A.start.x + b1 * A.start.y);

            float a2 = -dir2.y;
            float b2 = +dir2.x;
            float d2 = -(a2 * B.start.x + b2 * B.start.y);

            float seg1_line2_start = a2 * A.start.x + b2 * A.start.y + d2;
            float seg1_line2_end = a2 * A.end.x + b2 * A.end.y + d2;

            float seg2_line1_start = a1 * B.start.x + b1 * B.start.y + d1;
            float seg2_line1_end = a1 * B.end.x + b1 * B.end.y + d1;

            if (seg1_line2_start * seg1_line2_end > 0 || seg2_line1_start * seg2_line1_end > 0)
                return false;

            return true;
        }
    }
}

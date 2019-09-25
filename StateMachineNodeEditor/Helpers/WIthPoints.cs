using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace StateMachineNodeEditor.Helpers
{
    /// <summary>
    /// Вспомогательный класс, для работы с Point
    /// </summary>
   public class WithPoints
    {
        /// <summary>
        /// Сложение координат
        /// </summary>
        /// <param name="point1">Координата 1</param>
        /// <param name="point2">Координата 2</param>
        /// <returns>Сумма координат</returns>
        public static Point Addition(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }
    }
}

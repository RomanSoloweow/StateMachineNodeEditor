using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows;

namespace StateMachineNodeEditor.Helpers
{
    /// <summary>
    /// Класс точки
    /// </summary>
    public class MyPoint: ReactiveObject
    {
        /// <summary>
        /// Координата X
        /// </summary>
        [Reactive] public double X { get; set; }

        /// <summary>
        /// Координата Y
        /// </summary>
        [Reactive] public double Y { get; set; }

        public MyPoint(double x = 0,  double y = 0)
        {
            X = x;
            Y = y;
        }


        /// <summary>
        /// Конвертировать из MyPoint в Point
        /// </summary>
        /// <returns>Точка Point</returns>
        public Point ToPoint()
        {
            return MyPoint.MyPointToPoint(this);
        }

        /// <summary>
        /// Получить значения из Point
        /// </summary>
        /// <param name="point">Точка Point</param>
        /// <returns>Текущая точка содежащая координаты Point</returns>
        public MyPoint FromPoint(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
            return this;
        }

        #region Static Methods

        /// <summary>
        /// Конвертировать из MyPoint в Point
        /// </summary>
        /// <param name="point">Точка MyPoint</param>
        /// <returns>Точка Point</returns>
        public static Point MyPointToPoint(MyPoint point)
        {            
            return (point != null)?new Point(point.X, point.Y): new Point();
        }

        /// <summary>
        /// Конвертировать из Point в MyPoint
        /// </summary>
        /// <param name="point">ТОчка Point</param>
        /// <returns>Точка MyPoint</returns>
        public static MyPoint MyPointFromPoint(Point point)
        {
            return (point != null) ? new MyPoint(point.X, point.Y) : new MyPoint();
        }

        /// <summary>
        ///  Сложение координат двух точек
        /// </summary>
        /// <param name="point1">Точка 1</param>
        /// <param name="point2">Точка 2</param>
        /// <returns>Новая координата с результатом сложения</returns>
        public static MyPoint operator + (MyPoint point1, MyPoint point2)
        {
            return new MyPoint(point1.X + point2.X, point2.X + point2.Y);
        }

        /// <summary>
        /// Вычитание координат двух точек
        /// </summary>
        /// <param name="point1">Точка 1</param>
        /// <param name="point2">Точка 2</param>
        /// <returns>Точка содержащая разность двух точек</returns>
        public static MyPoint operator - (MyPoint point1, MyPoint point2)
        {
            return new MyPoint(point1.X - point2.X, point2.X - point2.Y);
        }



        #endregion Static Methods

    }
}

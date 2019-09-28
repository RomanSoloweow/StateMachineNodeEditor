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
        /// Точка с координатами X и Y
        /// </summary>
        [Reactive] public Point Value { get; set; }

        /// <summary>
        /// Координата X
        /// </summary>
        public double X
        {
            get { return Value.X; }
        }

        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y
        {
            get { return Value.Y; }
        }

        public MyPoint(double x = 0,  double y = 0)
        {
            Set(x, y);
        }

        public MyPoint(Point point)
        {
            FromPoint(point);
        }

        /// <summary>
        /// Точка находится в начале координат (0,0)
        /// </summary>
        public bool IsClear
        {
            get
            {
                return (this.X == 0) && (this.Y == 0);
            }
        }

        /// <summary>
        /// Занулить координаты точки
        /// </summary>
        public void Clear()
        {
            Value = new Point();
        }

        /// <summary>
        /// Сложение координат двух точек
        /// </summary>
        /// <param name="point">Вторая точка MyPoint</param>
        /// <returns>MyPoint с результатом суммы</returns>
        public MyPoint Add(MyPoint point)
        {
            Value = new Point(point.X, point.Y);
            return this;
        }

        /// <summary>
        /// Установить координаты текущей точки
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>MyPoint с новыми координатами</returns>
        public MyPoint Set(double x = 0, double y = 0)
        {
            Value = new Point(x, y);
            return this;
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
            Value = new Point(point.X, point.Y);
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
        /// Сложение координат двух точек
        /// </summary>
        /// <param name="point1">Точка 1</param>
        /// <param name="point2">Точка 2</param>
        /// <returns>Новая координата с результатом сложения</returns>
        public static MyPoint operator + (MyPoint point1, MyPoint point2)
        {
            return new MyPoint(point1.X + point2.X, point1.Y + point2.Y);
        }

        /// <summary>
        /// Вычитание координат двух точек
        /// </summary>
        /// <param name="point1">Точка 1</param>
        /// <param name="point2">Точка 2</param>
        /// <returns>Точка содержащая разность двух точек</returns>
        public static MyPoint operator - (MyPoint point1, MyPoint point2)
        {
            return new MyPoint(point1.X - point2.X, point1.Y - point2.Y);
        }

        /// <summary>
        /// Прибавить число к обеим координатам точки
        /// </summary>
        /// <param name="point1">Точка, к которой прибавляем</param>
        /// <param name="number">Число, которое прибавляем</param>
        /// <returns>Новая точка</returns>
        public static MyPoint operator +(MyPoint point1, int number)
        {
            return new MyPoint(point1.X + number, point1.Y + number);
        }

        /// <summary>
        /// Отнять число от обеих координат точки
        /// </summary>
        /// <param name="point1">Точка, из которой вычитаем</param>
        /// <param name="number">Число, которое вычитаем</param>
        /// <returns>Новая точка</returns>
        public static MyPoint operator -(MyPoint point1, int number)
        {
            return new MyPoint(point1.X - number, point1.Y - number);
        }

        #endregion Static Methods

    }
}

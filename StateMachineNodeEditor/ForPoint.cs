using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
namespace StateMachineNodeEditor
{
   static public class ForPoint
    {
        public static Point Addition(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point Division(Point point1, Point point2)
        {
            return new Point(point1.X / point2.X, point1.Y / point2.Y);
        }
        public static Point Division(Point point1, int number)
        {
            return new Point(point1.X / number, point1.Y / number);
        }
        public static Point Division(Point point1, double number)
        {
            return new Point(point1.X / number, point1.Y / number);
        }
        public static Point Subtraction(Point point1, Point point2)
        {
            return new Point(point1.X - point2.X, point1.Y - point2.Y);
        }
        public static Point Subtraction(Point point1, int number)
        {
            return new Point(point1.X - number, point1.Y - number);
        }
        public static Point Subtraction(Point point1, double number)
        {
            return new Point(point1.X -number, point1.Y -number);
        }
        public static Point Equality(Point point1, Point point2)
        {
            return new Point(point2.X, point2.Y);
        }

        public static Point GetValueAsPoint(TranslateTransform translate)
        {
          return new Point(translate.X, translate.Y);
        }
        public static void Addition(TranslateTransform translate, Point point)
        {
            translate.X += point.X;
            translate.Y += point.Y;
        }
        public static void Equality(TranslateTransform translate, Point point)
        {
            translate.X = point.X;
            translate.Y = point.Y;
        }
        public static void EqualityScale(ScaleTransform scale, Point point)
        {
            scale.ScaleX = point.X;
            scale.ScaleY = point.Y;
        }
        public static void EqualityScale(ScaleTransform scale, double number)
        {
            scale.ScaleX = number;
            scale.ScaleY = number;
        }
        public static void AdditionToCenter(RotateTransform rotate, Point point)
        {
            rotate.CenterX += point.X;
            rotate.CenterY += point.Y;
        }
        public static void EqualityCenter(RotateTransform rotate, Point point)
        {
            rotate.CenterX = point.X;
            rotate.CenterY = point.Y;
        }


        public static Point DivisionOnScale(Point point1, ScaleTransform scale)
        {
            return new Point(point1.X / scale.ScaleX, point1.Y / scale.ScaleY);
        }
    }
}

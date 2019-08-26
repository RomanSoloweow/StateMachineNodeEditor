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
        
        public static Point Multiplication(Point point1, int number)
        {
            return new Point(point1.X * number, point1.Y * number);
        }
        public static Point Multiplication(Point point1, double number)
        {
            return new Point(point1.X * number, point1.Y * number);
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

        public static Point GetPoint1(FrameworkElement element, TranslateTransform translate)
        {
            return ForPoint.GetValueAsPoint(translate);
        }
        public static Point GetPoint2(FrameworkElement element, TranslateTransform translate)
        {
            Point point1 = GetPoint1(element, translate);
            return new Point(point1.X + element.ActualWidth, point1.Y + element.ActualHeight);
        }
        public static Point GetPoint1WithAngle(FrameworkElement element, Transforms transforms)
        {
            if ((transforms.rotate.Angle >= 0) && (transforms.rotate.Angle < 90))
            {
                return ForPoint.GetPoint1(element, transforms.translate);
            }
            else if ((transforms.rotate.Angle >= 90) && (transforms.rotate.Angle < 180))
            {
                Point point1 = ForPoint.GetPoint1(element, transforms.translate);
                return new Point(point1.X - element.ActualHeight, point1.Y );
            }
            else if ((transforms.rotate.Angle >= 180) && (transforms.rotate.Angle < 270))
            {
                Point point1 = ForPoint.GetPoint1(element, transforms.translate);
                return new Point(point1.X - element.ActualWidth, point1.Y - element.ActualHeight);
            }
            else
            {
                Point point1 = ForPoint.GetPoint1(element, transforms.translate);
                return new Point(point1.X, point1.Y - element.ActualWidth);
            }
        }
        public static Point GetPoint2WithAngle(FrameworkElement element, Transforms transforms)
        {
            if ((transforms.rotate.Angle >= 0) && (transforms.rotate.Angle < 90))
            {
                return ForPoint.GetPoint2(element, transforms.translate);
            }
            else if ((transforms.rotate.Angle >= 90) && (transforms.rotate.Angle < 180))
            {
                Point point1 = ForPoint.GetPoint1(element, transforms.translate);
                return new Point(point1.X, point1.Y + element.ActualWidth);
            }
            else if ((transforms.rotate.Angle >= 180) && (transforms.rotate.Angle < 270))
            {
                return ForPoint.GetPoint1(element, transforms.translate);
            }
            else
            {
                Point point2 = ForPoint.GetPoint1(element, transforms.translate);
                return new Point(point2.X+element.ActualHeight, point2.Y );
            }
        }
    }
}

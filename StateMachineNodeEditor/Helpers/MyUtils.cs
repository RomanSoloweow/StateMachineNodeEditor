using System.Windows;
using System.Windows.Media;
using System;
using System.Collections.Generic;

namespace StateMachineNodeEditor.Helpers
{
    public static class MyUtils
    {
        public static T FindParent<T>(DependencyObject currentObject) where T : DependencyObject
        {
            DependencyObject foundObject = currentObject;
            do
            {
                foundObject = VisualTreeHelper.GetParent(foundObject);
                if (foundObject == null)
                  return default(T);
            } while (!(foundObject is T));

            return (T)foundObject;
        }

        public static bool Intersect(MyPoint a1, MyPoint b1, MyPoint a2, MyPoint b2)
        {
            bool par1 = a1.X > b2.X; //второй перед первым
            bool par2 = a2.X > b1.X; //первый перед вторым
            bool par3 = a1.Y > b2.Y; //первый под вторым
            bool par4 = a2.Y > b1.Y; //второй под первым
            //если хоть одно условие выполняется - прямоугольники не пересекаются
            return !(par1 || par2 || par3 || par4);
        }

        public static MyPoint GetStartPointDiagonal(MyPoint a1, MyPoint b1)
        {
            return new MyPoint(Math.Min(a1.X, b1.X), Math.Min(a1.Y, b1.Y));
        }
        public static MyPoint GetEndPointDiagonal(MyPoint a1, MyPoint b1)
        {
            return new MyPoint(Math.Max(a1.X, b1.X), Math.Max(a1.Y, b1.Y));
        }

        //пока что не ясно
        private static Point[] bezierCoeffs(MyPoint bezierStartPoint, MyPoint bezierPoint1, MyPoint bezierPoint2, MyPoint bezierEndPoint)
        {
            Point[] coeffs = new Point[4];

            coeffs[0].X = -bezierStartPoint.X + 3 * bezierPoint1.X + -3 * bezierPoint2.X + bezierEndPoint.X;
            coeffs[1].X = 3 * bezierStartPoint.X - 6 * bezierPoint1.X + 3 * bezierPoint2.X;
            coeffs[2].X = -3 * bezierStartPoint.X + 3 * bezierPoint1.X;
            coeffs[3].X = bezierStartPoint.X;

            coeffs[0].Y = -bezierStartPoint.Y + 3 * bezierPoint1.Y + -3 * bezierPoint2.Y + bezierEndPoint.Y;
            coeffs[1].Y = 3 * bezierStartPoint.Y - 6 * bezierPoint1.Y + 3 * bezierPoint2.Y;
            coeffs[2].Y = -3 * bezierStartPoint.Y + 3 * bezierPoint1.Y;
            coeffs[3].Y = bezierStartPoint.Y;

            return coeffs;
        }
        //Поиск корней
        private static double[] cubicRoots(double a, double b, double c, double d)
        {
            double A = b / a;
            double B = c / a;
            double C = d / a;

            double Q, R, D, S, T, Im;

            Q = (3 * B - Math.Pow(A, 2)) / 9;
            R = (9 * A * B - 27 * C - 2 * Math.Pow(A, 3)) / 54;
            D = Math.Pow(Q, 3) + Math.Pow(R, 2);

            double[] t = new double[3];

            if (D >= 0)
            {
                double sqrtD = Math.Sqrt(D);//Это выражение используется несколько раз. Небольшая оптимизация
                double _AD3 = -A / 3;//Это выражение используется несколько раз. Небольшая оптимизация
                double RAsqrtD = R + sqrtD;//Это выражение используется несколько раз. Небольшая оптимизация
                double RSsqrtD = R - sqrtD;//Это выражение используется несколько раз. Небольшая оптимизация
                double D13 = (1 / 3);//Это выражение используется несколько раз. Небольшая оптимизация
                S = Math.Sign(RAsqrtD) * Math.Pow(Math.Abs(RAsqrtD), D13);
                T = Math.Sign(RSsqrtD) * Math.Pow(Math.Abs(RSsqrtD), D13);

                double SST = (S + T);//Это выражение используется несколько раз. Небольшая оптимизация

                t[0] = _AD3 + SST;
                t[1] = _AD3 - SST / 2;
                t[2] = t[1];
                Im = Math.Abs(Math.Sqrt(3) * (S - T) / 2);

                if (Im != 0)
                {
                    t[1] = -1;
                    t[2] = -1;
                }
            }
            else
            {
                double th = Math.Acos(R / Math.Sqrt(-Math.Pow(Q, 3)));
                double sqrt_QM2 = 2 * Math.Sqrt(-Q);//Это выражение используется несколько раз. Небольшая оптимизация
                double AD3 = A / 3; //Это выражение используется несколько раз. Небольшая оптимизация

                double thD3 = (th / 3); //Это выражение используется несколько раз. Небольшая оптимизация
                double PIM2D3 = (Math.PI * 2) / 3; //Это выражение используется несколько раз. Небольшая оптимизация

                t[0] = sqrt_QM2 * Math.Cos(thD3) - AD3;
                t[1] = sqrt_QM2 * Math.Cos(thD3 + PIM2D3) - AD3;
                t[2] = sqrt_QM2 * Math.Cos(thD3 + 2 * PIM2D3) - AD3;
            }

            for (var i = 0; i < 3; i++)
                if (t[i] < 0 || t[i] > 1.0) 
                           t[i] = -1;

            Func<double[], double[]> sortSpecial = array =>
            {
                bool flip;
                double temp;
                do
                {
                    flip = false;
                    for (var i = 0; i < array.Length - 1; i++)
                    {
                        if ((array[i + 1] >= 0 && array[i] > array[i + 1]) || (array[i] < 0 && array[i + 1] >= 0))
                        {
                            flip = true;
                            temp = array[i];
                            array[i] = array[i + 1];
                            array[i + 1] = temp;

                        }
                    }
                } while (flip);

                return array;
            };
            return sortSpecial(t);
        }
        //основная функция
        public static bool ComputeIntersections(MyPoint bezierStartPoint, MyPoint bezierPoint1, MyPoint bezierPoint2, MyPoint bezierEndPoint, MyPoint lineStartPoint, MyPoint lineEndPoint)
        {
            double A = lineEndPoint.Y - lineStartPoint.Y;// A = y2 - y1
            double B = lineStartPoint.X - lineEndPoint.X;// B = x1 - x2
            double C = -lineStartPoint.X * A - lineStartPoint.Y * B;//C=x1*(y1-y2)+y1*(x2-x1) = x1*(-A)+y1*(-B)=-x1*(A)-y1*(B)

            var coeffs = bezierCoeffs(bezierStartPoint, bezierPoint1, bezierPoint2, bezierEndPoint);

            double[] P = new double[4];

            P[0] = A * coeffs[0].X + B * coeffs[0].Y;// t^3
            P[1] = A * coeffs[1].X + B * coeffs[1].Y;// t^2
            P[2] = A * coeffs[2].X + B * coeffs[2].Y;// t
            P[3] = A * coeffs[3].X + B * coeffs[3].Y + C;

            var r = cubicRoots(P[0], P[1], P[2], P[3]);

            List<MyPoint> X = new List<MyPoint>();
            double t;
            MyPoint p;
            double s;
            double tMt;
            double tMtMt; 
            for (int i = 0; i < 3; i++)
            {
                t = r[i];
                tMt = t * t; //Это выражение используется несколько раз. Небольшая оптимизация
                tMtMt = tMt * t; //Это выражение используется несколько раз. Небольшая оптимизация
                p = new MyPoint
                (
                   coeffs[0].X * tMtMt + coeffs[1].X * tMt + coeffs[2].X * t + coeffs[3].X,
                   coeffs[0].Y * tMtMt + coeffs[1].Y * tMt + coeffs[2].Y * t + coeffs[3].Y
               );
              
                if ((lineEndPoint.X - lineStartPoint.X) != 0)
                    s = (p.X - lineStartPoint.X) / (lineEndPoint.X - lineStartPoint.X);
                else
                    s = (p.Y - lineStartPoint.Y) / (lineEndPoint.Y - lineStartPoint.Y);

                if (t < 0 || t > 1.0 || s < 0 || s > 1.0)
                {
                    continue;
                }

                X.Add(p);
            }

            return X.Count>0;
        }
    }
}

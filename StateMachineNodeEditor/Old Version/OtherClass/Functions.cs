using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StateMachineNodeEditor
{
    public static class Functions
    {
        public static bool Intersect(Point a1, Point b1, Point a2, Point b2)
        {
            bool par1 = a1.X > b2.X; //второй перед первым
            bool par2 = a2.X > b1.X; //первый перед вторым
            bool par3 = a1.Y > b2.Y; //первый под вторым
            bool par4 = a2.Y > b1.Y; //второй под первым
            //если хоть одно условие выполняется - прямоугольники пересекаются
            return !(par1 || par2 || par3 || par4);
        }   
    }
}

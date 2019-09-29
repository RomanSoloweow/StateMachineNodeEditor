using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor.Helpers
{
    public static class Utils
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
            //если хоть одно условие выполняется - прямоугольники пересекаются
            return !(par1 || par2 || par3 || par4);
        }
    }
}

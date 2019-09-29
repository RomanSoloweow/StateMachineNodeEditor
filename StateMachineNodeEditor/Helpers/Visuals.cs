using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor.Helpers
{
    public static class Visuals
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
    }
}

using System;
using System.Windows;
using ReactiveUI;

namespace StateMachineNodeEditor.Helpers
{
    public class ConverterPointToMyPoint: IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            bool fromTypeIsPoint = (fromType == typeof(Point));
            bool fromTypeIsMyPoint = (fromType == typeof(MyPoint));
            bool toTypeIsPoint= (toType == typeof(Point));
            bool toTypeIsMyPoint= (toType == typeof(MyPoint));
            if (!(fromTypeIsPoint|| fromTypeIsMyPoint)|| !(toTypeIsPoint || toTypeIsMyPoint))
                return 0;

            return 100;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            if(toType == typeof(Point))
            {
                result = MyPoint.MyPointToPoint((MyPoint)from);
            }
            else if (toType == typeof(MyPoint))
            {
                result = MyPoint.MyPointFromPoint((Point)from);
            }
            else
            {
                result = null;
                return false;

            }

            return true;

        }
    }
}

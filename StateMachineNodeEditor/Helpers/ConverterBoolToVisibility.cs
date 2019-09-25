﻿using System;
using System.Windows;
using ReactiveUI;

namespace StateMachineNodeEditor.Helpers
{
    public class ConverterBoolToVisibility:  IBindingTypeConverter
    {
        //public Visibility TrueValue { get; set; }
        //public Visibility FalseValue { get; set; }
        //public Visibility NullValue { get; set; }
        //public ConverterBoolToVisibility()
        //{
        //    // set defaults
        //    TrueValue = Visibility.Visible;
        //    FalseValue = Visibility.Hidden;
        //    NullValue = Visibility.Collapsed;
        //}

        //public object Convert(object value, Type targetType,
        //    object parameter, CultureInfo culture)
        //{

        //    if (value == null)
        //        return NullValue;

        //    if (!(value is bool?))
        //        return null;

        //    return (bool)value ? TrueValue : FalseValue;
        //}

        //public object ConvertBack(object value, Type targetType,
        //    object parameter, CultureInfo culture)
        //{
        //    if (Equals(value, TrueValue))
        //        return true;
        //    if (Equals(value, FalseValue))
        //        return false;
        //    return null;
        //}

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            bool fromTypeIsNullBool = (fromType == typeof(bool?));
            bool fromTypeIsBool = (fromType == typeof(bool));
            bool toTypeIsVisibility = (toType == typeof(Visibility));
            if(!(fromTypeIsNullBool || fromTypeIsBool) ||!toTypeIsVisibility)
                return 0;

            return 100;            
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            result = null;
            if (toType != typeof(Visibility))
                return false;

            if(from==null)
            {
                result = Visibility.Collapsed;
                return true;
            }

            bool valueIsCorrect;
            valueIsCorrect =  bool.TryParse(from.ToString(),out bool value);
            if(valueIsCorrect)
            {
                result = value ? Visibility.Visible : Visibility.Hidden;
            }       
            return valueIsCorrect;

        }
    }
}
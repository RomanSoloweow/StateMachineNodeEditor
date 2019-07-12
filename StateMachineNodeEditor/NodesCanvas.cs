﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace StateMachineNodeEditor
{
    public class NodesCanvas : Grid, ManagedElement
    {
        public Managers Manager { get; set; }
        public NodesCanvas()
        {
            Manager = new Managers(this);
            AddStyles();
            this.ClipToBounds = true;
        }
        public UIElement parent;
        public NodesCanvas(UIElement _parent):this()
        {
            parent = _parent;
            this.Background = Brushes.Red;
            this.Children.Add(new Node("State1"));
            this.Children.Add(new Node("State2"));
        }
        public void AddStyles()
        {
            #region Style for class Text
            Styles TextStyle = new Styles();
            TextStyle.AddSetter(Text.BorderBrushProperty, null);
            TextStyle.AddSetter(Text.BackgroundProperty, null);
            TextStyle.AddSetter(Text.TextWrappingProperty, TextWrapping.NoWrap);
            TextStyle.AddSetter(Text.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            TextStyle.AddSetter(Text.VerticalAlignmentProperty, VerticalAlignment.Center);
            TextStyle.AddSetter(Text.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            TextStyle.AddSetter(Text.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            TextStyle.AddSetter(Text.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            TextStyle.AddSetter(Text.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            TextStyle.AddSetter(Text.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            TextStyle.AddSetter(Text.MaxLengthProperty, 100);
            TextStyle.AddSetter(Text.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            TextStyle.AddSetter(Text.CaretBrushProperty, Brushes.DarkGray);
            TextStyle.AddSetter(Text.ForegroundProperty, Brushes.White);
            TextStyle.TargetType = typeof(Text);
            Application.Current.Resources.Add(typeof(Text), TextStyle);
            #endregion
            #region Style for class Node
            Styles NodeStyle = new Styles();
            #region Name
            NodeStyle.AddSetter(Node.BorderBrushProperty, null);
            NodeStyle.AddSetter(Node.BackgroundProperty, null);
            NodeStyle.AddSetter(Node.TextWrappingProperty, TextWrapping.NoWrap);
            NodeStyle.AddSetter(Node.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            NodeStyle.AddSetter(Node.VerticalAlignmentProperty, VerticalAlignment.Center);
            NodeStyle.AddSetter(Node.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            NodeStyle.AddSetter(Node.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            NodeStyle.AddSetter(Node.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Node.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            NodeStyle.AddSetter(Node.BorderThicknessProperty, new Thickness(0, 0, 0, 0));
            NodeStyle.AddSetter(Node.MaxLengthProperty, 100);
            NodeStyle.AddSetter(Node.MinWidthProperty, (double)60);
            NodeStyle.AddSetter(Node.SelectionBrushProperty, new SolidColorBrush(Color.FromRgb(0, 120, 215)));
            NodeStyle.AddSetter(Node.ContextMenuProperty, null);
            NodeStyle.AddSetter(Node.DataContextProperty, null);
            NodeStyle.AddSetter(Node.CaretBrushProperty, Brushes.DarkGray);
            NodeStyle.AddSetter(Node.ForegroundProperty, Brushes.White);
            #endregion
            NodeStyle.AddSetter(Node.BorderProperty, new Thickness(10, 2, 10, 2));
            NodeStyle.AddSetter(Node.InOutTextCultureProperty, new System.Globalization.CultureInfo("en-US"));
            NodeStyle.AddSetter(Node.InOutSpaceProperty, (double)10);
            #region Body
            NodeStyle.AddSetter(Node.BodyRadiusProperty, (double)5);
            NodeStyle.AddSetter(Node.BodyBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(45, 45, 48)));
            NodeStyle.AddSetter(Node.BodyPenProperty, new Pen());
            #endregion
            #region Header
            NodeStyle.AddSetter(Node.HeaderRadiusProperty, (double)5);
            NodeStyle.AddSetter(Node.HeaderBrushProperty, (Brush)new SolidColorBrush(Color.FromRgb(18, 61, 106)));
            NodeStyle.AddSetter(Node.HeaderPenProperty, new Pen());
            #endregion          
            #region Input
            #region Figure
            NodeStyle.AddSetter(Node.InputVisibleProperty, true);
            NodeStyle.AddSetter(Node.InputSizeProperty, new Size(10, 10));
            NodeStyle.AddSetter(Node.InputBrushProperty, new SolidColorBrush(Color.FromRgb(92, 83, 83)));
            NodeStyle.AddSetter(Node.InputPenProperty, new Pen());
            #endregion
            #region Text
            NodeStyle.AddSetter(Node.InputTextProperty, "Input");
            NodeStyle.AddSetter(Node.InputTextBrushProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            #endregion
            #endregion
            #region Output
            #region Figure
            NodeStyle.AddSetter(Node.OutputVisibleProperty, true);
            NodeStyle.AddSetter(Node.OutputSizeProperty, new Size(10, 10));
            NodeStyle.AddSetter(Node.OutputBrushProperty, new SolidColorBrush(Color.FromRgb(92, 83, 83)));
            NodeStyle.AddSetter(Node.OutputPenProperty, new Pen());
            #endregion
            #region Text
            NodeStyle.AddSetter(Node.OutputTextProperty, "Output");
            NodeStyle.AddSetter(Node.OutputTextBrushProperty, new SolidColorBrush(Color.FromRgb(255, 255, 255)));
            #endregion
            #endregion
            NodeStyle.TargetType = typeof(Node);
            Application.Current.Resources.Add(typeof(Node), NodeStyle);
            #endregion
        }
    }
}

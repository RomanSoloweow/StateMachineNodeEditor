using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
namespace StateMachineNodeEditor
{
    public class Transforms
    {
        public TransformGroup _transformGroup { get;  set; } = new TransformGroup();
        public TranslateTransform translate { get;  set; } = new System.Windows.Media.TranslateTransform();
        public RotateTransform rotate { get;  set; } = new RotateTransform();
        public ScaleTransform scale { get; set; } = new ScaleTransform();
        public SkewTransform skew { get;  set; } = new SkewTransform();
        public MatrixTransform matrix { get;  set; } = new MatrixTransform();
        public Point Origin
        {
            get { return parent.RenderTransformOrigin; }

            set { parent.RenderTransformOrigin = value; }
        }
        public FrameworkElement parent;
        public Transforms(FrameworkElement _parent)
        {
            _transformGroup.Children.Add(translate);
            _transformGroup.Children.Add(rotate);
            _transformGroup.Children.Add(scale);
            _transformGroup.Children.Add(skew);
            _transformGroup.Children.Add(matrix);
            parent = _parent;
            parent.RenderTransform = _transformGroup;
        }
    }
}

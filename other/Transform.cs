using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
namespace StateMachineNodeEditor
{
    public class Transform
    {
        private TransformGroup _transformGroup = new TransformGroup();
        public TranslateTransform translate = new TranslateTransform();
        public RotateTransform rotate = new RotateTransform();
        public ScaleTransform scale = new ScaleTransform();
        public SkewTransform skew= new SkewTransform();
        public MatrixTransform matrix= new MatrixTransform();
        public UIElement parent;
        public Point Origin
        {
            get
            {
                return parent.RenderTransformOrigin;
            }

            set
            {
                parent.RenderTransformOrigin = value;
            }
        }
    public Transform(UIElement _parent)
        {
            parent = _parent;
            _transformGroup.Children.Add(translate);
            _transformGroup.Children.Add(rotate);
            _transformGroup.Children.Add(scale);
            _transformGroup.Children.Add(skew);
            _transformGroup.Children.Add(matrix);
            parent.RenderTransform = _transformGroup;
        }
        
    }
}
